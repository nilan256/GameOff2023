using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat.HitBehaviours
{

    [AddComponentMenu("Game/Attack/On Hit/Maximum Hit Count")]
    public class MaximumHitCount : HitBehaviour
    {

        [MinValue(1)]
        public int Maximum = 1;

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (projectile.HitCount >= Maximum)
            {
                projectile.Disappear();
            }
        }

    }

}