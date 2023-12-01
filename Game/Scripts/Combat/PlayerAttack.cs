using System.Collections.Generic;
using Irisheep.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{

    [DisallowMultipleComponent]
    public class PlayerAttack : GameBehaviour
    {

        [TitleGroup(InspectorGroup.References)]
        [Required]
        public Projectile Prefab;

        [TitleGroup(InspectorGroup.Parameters)]
        public LayerMask EnemyLayer;

        [TitleGroup(InspectorGroup.Parameters)]
        public LayerMask ObstacleLayer;

        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float CooldownSeconds = 1;

        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        [Unit(Units.Degree)]
        public float ScatteringDegrees = 5;

        [TitleGroup(InspectorGroup.Events)]
        public UnityEvent Attacked = new UnityEvent();

        public TargetFinder TargetFinder { get; set; }
        
        public List<HitBehaviour> HitBehaviours { get; } = new List<HitBehaviour>();

        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        [ReadOnly]
        public float CurrentCooldown { get; set; }

        [FoldoutGroup(InspectorGroup.RuntimeInfo)]
        [ShowInInspector]
        [ReadOnly]
        public int AttackCount { get; protected set; }

        protected override void OnDestroyNotApplicationQuitting()
        {
            GameSpawner.DestroyPoolOf(Prefab);
        }

        protected virtual void Update()
        {
            CurrentCooldown -= Time.deltaTime;
        }

        public virtual bool CanAttack()
        {
            if (CurrentCooldown > 0) return false;
            if (TargetFinder && !TargetFinder.HasTarget()) return false;
            return true;
        }

        public virtual void Attack()
        {
            CurrentCooldown = GetCooldown();
            AttackCount += 1;

            var aim = TargetFinder.GetTargetDirection();
            for (int i = 0; i < Player.ProjectileAmount; i++)
            {
                var projectile = GameSpawner.Spawn(Prefab);
                InitializeProjectile(projectile, aim, i, Player.ProjectileAmount);
            }
            
            Attacked.Invoke();
        }

        protected virtual void InitializeProjectile(Projectile projectile, Vector2 aim, int index, int total)
        {
            var offset = index - ((float)total / 2 - 0.5f);
            var dir = aim.RotateDegrees(ScatteringDegrees * offset);

            projectile.transform.position = transform.position;
            projectile.SetMoveDirection(dir);
            projectile.Fire();
            projectile.HitEvent.AddListener(OnProjectileHit);
        }

        private void OnProjectileHit(Projectile projectile, Collider2D other)
        {
            foreach (var hitBehaviour in HitBehaviours)
            {
                if (hitBehaviour)
                {
                    hitBehaviour.OnHit(projectile, other);
                }
            }
        }

        protected virtual float GetCooldown()
        {
            return CooldownSeconds / Player.AttackSpeed;
        }

        [Button("Test Attack")]
        private void TestAttack(Vector2 aim = default, int amount = 1)
        {
            if (aim == default) aim = Random.insideUnitCircle;
            for (int i = 0; i < amount; i++)
            {
                var projectile = GameSpawner.Spawn(Prefab);
                projectile.transform.position = transform.position;
                projectile.SetMoveDirection(aim);
                projectile.Fire();
                projectile.HitEvent.AddListener(OnProjectileHit);
            }
        }

    }

}