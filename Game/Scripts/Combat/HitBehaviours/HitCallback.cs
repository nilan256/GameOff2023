using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat.HitBehaviours
{

    public class HitCallback : HitBehaviour
    {

        public UnityEvent Callback = new UnityEvent();

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            Callback.Invoke();
        }

    }

}