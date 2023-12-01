using UnityEngine;

namespace Game.Combat
{

    public abstract class HitBehaviour : ProjectileBehaviour
    {

        public abstract void OnHit(Projectile projectile, Collider2D collider);

        protected virtual void OnEnable()
        {
            Owner.HitBehaviours.Add(this);
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            Owner.HitBehaviours.Remove(this);
        }

    }

}