using System.Collections;
using Game.CharacterControl;
using Game.Events;
using Irisheep.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{

    public class Enemy : GameBehaviour, IGameEventListener<InstantKillAllEvent>
    {

        #region Statics

        public static int AliveEnemyCount { get; private set; }

        #endregion
        
        #region Serialized Fields
        
        [TitleGroup(InspectorGroup.References), Required]
        public HitBox TouchDamageBox;

        [TitleGroup(InspectorGroup.References), Required]
        public CharacterMovement Movement;
        
        [TitleGroup(InspectorGroup.References), Required]
        public Collider2D BodyCollider;

        [TitleGroup(InspectorGroup.References)]
        public Animator Animator;

        [TitleGroup(InspectorGroup.Animation)]
        public string DeadAnimationTrigger;
        
        [TitleGroup(InspectorGroup.Parameters), MinValue(1)]
        public float MaximumHealth = 100;
        
        [TitleGroup(InspectorGroup.Parameters), MinValue(0)]
        public float AttackPower = 10;
        
        [TitleGroup(InspectorGroup.Parameters), MinValue(0)]
        public float AttackCooldown = 1;

        [TitleGroup(InspectorGroup.Parameters), Range(0, 1)]
        public float KnockbackResistance;
        
        [TitleGroup(InspectorGroup.Parameters), MinValue(0)]
        public float MoveSpeed = 100;

        [TitleGroup(InspectorGroup.Parameters), MinValue(0)]
        public int XpValue;

        #endregion

        #region Fields And Properties

        private bool isDeadAnimationPlaying;
        private float timeLastAttack;
        
        [FoldoutGroup(InspectorGroup.RuntimeInfo), ReadOnly]
        public float CurrentHealth { get; private set; }

        [FoldoutGroup(InspectorGroup.RuntimeInfo), ReadOnly]
        public bool IsAlive { get; private set; }

        public Vector2 Position => (Vector2)transform.position + BodyCollider.offset;

        #endregion
        
        #region Unity Events

        private void Start()
        {
            if (!BodyCollider) BodyCollider = GetComponent<Collider2D>();
            TouchDamageBox.HitEvent.AddListener(OnTouchPlayer);
        }

        private void OnEnable()
        {
            CurrentHealth = MaximumHealth;
            IsAlive = true;
            AliveEnemyCount += 1;
            this.StartListening<InstantKillAllEvent>();
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            AliveEnemyCount -= 1;
            this.StopListening<InstantKillAllEvent>();
        }

        private void Update()
        {
            ProcessMovement();
        }

        #endregion

        #region Public Methods

        public void Hurt(float damage)
        {
            if (!IsAlive) return;
            damage = Mathf.Min(damage, CurrentHealth);
            if (damage == 0) return;
            CurrentHealth -= damage;
            EnemyHealthChangedEvent.Send(this, -damage);

            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }

        public void Knockback(Vector2 force)
        {
            if (!IsAlive) return;
            force *= 1 - KnockbackResistance;
            Movement.Knockback(force);
        }

        public void Kill()
        {
            IsAlive = false;
            StartCoroutine(GoDie());
        }

        #endregion
        
        #region Nonpublic Methods

        private void ProcessMovement()
        {
            Movement.MoveDirection = Vector2.zero;
            Movement.MoveSpeed = 0;
            if (IsAlive)
            {
                var player = GetPlayer();
                if (player)
                {
                    Movement.MoveDirection = player.transform.position - transform.position;
                    Movement.MoveSpeed = MoveSpeed;
                }
            }
        }

        private void OnTouchPlayer(Collider2D playerCollider)
        {
            if (Time.time - timeLastAttack < AttackCooldown) return;
            timeLastAttack = Time.time;
            var player = playerCollider.GetComponentInParent<Player>();
            if (player)
            {
                player.Hurt(AttackPower);
            }
        }

        private Player GetPlayer()
        {
            if (GameplayController.HasInstance && GameplayController.Current.Player)
            {
                return GameplayController.Current.Player;
            }

            return null;
        }

        private IEnumerator GoDie()
        {
            if (Animator && !string.IsNullOrEmpty(DeadAnimationTrigger))
            {
                Animator.SetTrigger(DeadAnimationTrigger);
                var state = Animator.GetCurrentAnimatorStateInfo(0);
                while (state.normalizedTime < 1) yield return null;
            }
            
            GameSpawner.SpawnXp(XpValue, transform.position);
            EnemyDeadEvent.Send(this);
            Gameplay.TotalKills += 1;
            gameObject.SetActive(false);
        }
        
        #endregion

        public void OnEvent(InstantKillAllEvent evt)
        {
            Kill();
        }

    }

}