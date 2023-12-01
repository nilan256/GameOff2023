using Irisheep.Runtime;
using UnityEngine;

namespace Game.Combat
{

    public class CatchBoomerang : HitBehaviour
    {

        public LayerMask PlayerLayer;
        public float ReduceSeconds;

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (projectile is Boomerang boomerang)
            {
                if (boomerang.IsBacking && PlayerLayer.Contains(collider))
                {
                    Owner.CurrentCooldown -= Mathf.Min(Owner.CurrentCooldown, ReduceSeconds);
                    projectile.Disappear();
                }
            }
            
        }

    }

}