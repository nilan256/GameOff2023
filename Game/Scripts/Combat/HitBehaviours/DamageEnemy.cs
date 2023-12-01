using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat.HitBehaviours
{

    [AddComponentMenu("Game/Attack/On Hit/Damage Enemy")]
    public class DamageEnemy : HitBehaviour
    {
        
        [MinValue(0)]
        [Unit(Units.PercentMultiplier, Units.Percent)]
        public float DamageMultiplier = 1;

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy)) return;
            float damage = Gameplay.Player.GenerateDamage();
            damage *= DamageMultiplier;
            enemy.Hurt(damage);
        }

    }

}