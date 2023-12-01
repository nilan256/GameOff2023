using Irisheep.Runtime;
using UnityEngine;

namespace Game.Combat.HitBehaviours
{

    public class DisappearOnHit : HitBehaviour
    {

        public LayerMask HitLayer;

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (!HitLayer.Contains(collider)) return;
            projectile.Disappear();
        }

    }

}