using Doozy.Runtime.Soundy;
using Doozy.Runtime.Soundy.Ids;
using Game.Events;
using Irisheep.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{

    [DisallowMultipleComponent]
    public class Projectile : GameBehaviour, IGameEventListener<ClearProjectilesEvent>
    {

        #region Serialzied Fields

        [TitleGroup(InspectorGroup.References)][Required]
        public ProjectileMovement Movement;
        
        [TitleGroup(InspectorGroup.References)]
        public HitBox HitBox;
        
        [TitleGroup(InspectorGroup.Parameters)][MinValue(0)]
        public float MaximumLifetime = 60;
        
        [TitleGroup(InspectorGroup.Parameters)][MinValue(0)]
        public float HitInterval = 0.1f;
        
        [TitleGroup(InspectorGroup.Parameters)][MinValue(0)]
        public float Speed;
        
        [FoldoutGroup(InspectorGroup.Events, Order = 1000)]
        public UnityEvent<Projectile, Collider2D> HitEvent = new UnityEvent<Projectile, Collider2D>();
        [FoldoutGroup(InspectorGroup.Events)]
        public UnityEvent<Projectile> DisappearEvent = new UnityEvent<Projectile>();

        #endregion

        #region Fields And Properties

        protected float lastHitTime;
        protected float lifetime;
        public int HitCount { get; set; }

        #endregion

        #region Unity Events

        private void Start()
        {
            if (HitBox) HitBox.HitEvent.AddListener(OnHit);
        }

        private void OnEnable()
        {
            lifetime = 0f;
            this.StartListening<ClearProjectilesEvent>();
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            HitCount = 0;
            lastHitTime = 0;
            HitEvent.RemoveAllListeners();
            this.StopListening<ClearProjectilesEvent>();
        }

        protected virtual void Update()
        {
            UpdateLifetime();
        }

        #endregion

        #region Public Methods

        public virtual void SetMoveDirection(Vector2 direction)
        {
            Movement.MoveDirection = direction;
        }

        public virtual void Fire()
        {
            Movement.MoveSpeed = Speed;
        }

        public void Disappear()
        {
            if (!gameObject.activeSelf) return;
            DisappearEvent.Invoke(this);
            gameObject.SetActive(false);
        }

        #endregion

        #region Protected Methods

        private void UpdateLifetime()
        {
            lifetime += Time.deltaTime;
            if (MaximumLifetime > 0 && lifetime > MaximumLifetime)
            {
                Disappear();
            }
        }

        private void OnHit(Collider2D other)
        {
            if (Time.time - lastHitTime < HitInterval)
            {
                return;
            }
            HitCount += 1;
            lastHitTime = Time.time;
            HitEvent.Invoke(this, other);
        }

        #endregion

        public void OnEvent(ClearProjectilesEvent evt)
        {
            Disappear();
        }

    }

}