using System.Collections.Generic;
using Game.CharacterControl;
using Game.Combat;
using Game.Events;
using Game.SkillSystem;
using QFSW.QC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{

    [CommandPrefix("player.")]
    public class Player : GameBehaviour, IGameEventListener<GainXpEvent>
    {

        #region Serialized Fields

        [TitleGroup(InspectorGroup.References)]
        [Required]
        public CharacterMovement Movement;

        [TitleGroup(InspectorGroup.References)]
        [Required]
        public CircleCollider2D PickupRangeCollider;

        [FormerlySerializedAs("EvolvableModel")]
        [TitleGroup(InspectorGroup.References)]
        [Required]
        public ModelController ModelController;

        [TitleGroup(InspectorGroup.References)]
        [AssetsOnly]
        public PlayerAttack BasicAttackPrefab;

        [Command("hp-max")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(1)]
        public float MaximumHealth = 100;

        [Command("hp-regen")]
        [TitleGroup(InspectorGroup.Parameters)]
        public float HealthRegen = 1;

        [Command("hp-regen-cycle")]
        [TitleGroup(InspectorGroup.Parameters)]
        public float HealthRegenCycle = 5f;

        [Command("attack-power")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float AttackPower = 10;

        [Command("attack-speed")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float AttackSpeed = 1;

        [Command("knockback-power")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float KnockbackPower = 5000;

        [Command("crit-chance")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        [MaxValue(1)]
        [Unit(Units.PercentMultiplier, Units.Percent)]
        public float CritChance = 0.05f;

        [Command("crit-power")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(1)]
        [Unit(Units.PercentMultiplier, Units.Percent)]
        public float CritPower = 2;

        [Command("damage-resistance")]
        [TitleGroup(InspectorGroup.Parameters)]
        public float DamageResistance;

        [Command("move-speed")]
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float MoveSpeed = 100;

        [Command("pickup-range")]
        [TitleGroup(InspectorGroup.Parameters)]
        public float PickupRange = 60;

        [Command("projectile-amount")]
        [TitleGroup(InspectorGroup.Parameters)]
        public int ProjectileAmount = 1;

        [Command("xp-multiplier")]
        [TitleGroup(InspectorGroup.Parameters)]
        public float XpMultiplier = 1;

        [TitleGroup(InspectorGroup.Events)]
        public UnityEvent XpGained;

        #endregion

        #region Fields And Properties

        private float timeLastHpRegen;

        [Command("level")]
        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        public int PlayerLevel { get; set; }

        [Command("xp")]
        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        public int Xp { get; set; }

        [Command("xp-required")]
        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        public int XpRequired { get; set; }

        [Command("evolutions")]
        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        public int Evolutions { get; set; }

        [Command("hp")]
        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        public float CurrentHealth { get; set; }

        public List<SkillData> OwnSkills { get; } = new List<SkillData>();

        public PlayerAttack BasicAttack { get; private set; }

        public List<ReplaceBasicAttack> ReplaceBasicAttacks { get; } = new List<ReplaceBasicAttack>();

        public List<PlayerAttack> IndependentAttacks { get; } = new List<PlayerAttack>();

        #endregion

        #region Unity Events

        private void Start()
        {
            InitializeProperties();
            if (BasicAttackPrefab)
            {
                BasicAttack = Instantiate(BasicAttackPrefab, transform);
            }
        }

        private void OnEnable()
        {
            StartEventListening();
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            StopEventListening();
        }

        private void Update()
        {
            ProcessMovement();
            ProcessPickupRange();
            ProcessHealthRegen();
            ProcessAnimation();
            ProcessAttack();
        }

        #endregion

        #region Public Methods

        public void Hurt(float damage)
        {
            damage = Mathf.Min(damage, CurrentHealth);
            if (damage == 0) return;
            damage *= 1 - DamageResistance;
            CurrentHealth -= damage;
            PlayerHealthChangedEvent.Send(this, -damage);
        }

        public void Heal(float value)
        {
            value = Mathf.Min(MaximumHealth - CurrentHealth, value);
            if (value == 0) return;
            CurrentHealth += value;
            PlayerHealthChangedEvent.Send(this, value);
        }

        public CoroutineHandler Evolve()
        {
            Evolutions += 1;
            ModelController.ChangeModel(Evolutions);

            return CoroutineHandler.Empty;
        }

        public float GenerateDamage()
        {
            var critDamage = AttackPower * CritChance * CritPower;
            critDamage = Mathf.Max(critDamage, 0);
            return AttackPower + critDamage;
        }

        public void AddSkill(SkillData skill)
        {
            OwnSkills.Add(skill);
            skill.OnGained(this);
        }

        public void ChangeBasicAttack(PlayerAttack prefab)
        {
            if (BasicAttack)
            {
                Destroy(BasicAttack.gameObject);
            }
            BasicAttack = Instantiate(prefab, transform);
        }

        public void AddReplaceBasicAttack(ReplaceBasicAttack prefab)
        {
            var replaceBasicAttack = Instantiate(prefab, transform);
            ReplaceBasicAttacks.Add(replaceBasicAttack);
        }

        public void AddIndependentAttack(PlayerAttack prefab)
        {
            var attack = Instantiate(prefab, transform);
            IndependentAttacks.Add(attack);
        }

        #endregion

        #region Nonpublic Methods

        private void InitializeProperties()
        {
            PlayerLevel = 1;
            Xp = 0;
            XpRequired = Evaluator.GetXpRequired(1);
            Evolutions = 1;
            CurrentHealth = MaximumHealth;
        }

        private void StartEventListening()
        {
            this.StartListening<GainXpEvent>();
        }

        private void StopEventListening()
        {
            this.StopListening<GainXpEvent>();
        }

        private void ProcessMovement()
        {
            Movement.MoveSpeed = MoveSpeed;
        }

        private void ProcessPickupRange()
        {
            PickupRangeCollider.radius = PickupRange;
        }

        private void ProcessHealthRegen()
        {
            if (Time.time - timeLastHpRegen > HealthRegenCycle)
            {
                timeLastHpRegen = Time.time;

                Heal(HealthRegen);
            }
        }

        private void ProcessAnimation()
        {
            ModelController.CurrentModel.SetWalking(Movement.IsMoving);
        }

        private void ProcessAttack()
        {
            PlayerAttack basicAttack = BasicAttack;
            foreach (var replaceBasicAttack in ReplaceBasicAttacks)
            {
                if (replaceBasicAttack.CanAttack() && replaceBasicAttack.ShouldReplaceBasicAttack(BasicAttack))
                {
                    basicAttack = replaceBasicAttack;
                    break;
                }
            }

            if (basicAttack && basicAttack.CanAttack())
            {
                basicAttack.Attack();
            }

            foreach (var independentAttack in IndependentAttacks)
            {
                if (independentAttack && independentAttack.CanAttack())
                {
                    independentAttack.Attack();
                }
            }
        }

        #endregion

        #region Game Events

        public void OnEvent(GainXpEvent evt)
        {
            Xp += Mathf.CeilToInt(evt.Value * XpMultiplier);
            XpGained.Invoke();
        }

        #endregion

    }

}