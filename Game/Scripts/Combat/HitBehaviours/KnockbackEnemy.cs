using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat.HitBehaviours
{

    [AddComponentMenu("Game/Attack/On Hit/Knockback Enemy")]
    public class KnockbackEnemy : HitBehaviour
    {
        [MinValue(0)]
        [Unit(Units.PercentMultiplier, Units.Percent)]
        public float KnockbackMultiplier = 1;

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy)) return;
            var power = Gameplay.Player.KnockbackPower * KnockbackMultiplier;
            var dir = (enemy.transform.position - Gameplay.Player.transform.position).normalized;
            var force = dir * power;
            
            enemy.Knockback(force);
        }

    }

}