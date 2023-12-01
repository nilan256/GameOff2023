using UnityEngine;

namespace Game.Combat.HitBehaviours
{

    [AddComponentMenu("Game/Attack/OnHit/Bounce")]
    public class BounceOnHit : HitBehaviour
    {

        public override void OnHit(Projectile projectile, Collider2D collider)
        {
            if (!collider.TryGetComponent<Enemy>(out var enemy)) return;
            var origin = projectile.transform.position;
            var dir = enemy.transform.position - origin;
            var length = dir.magnitude;
            var hit = Physics2D.Raycast(origin, dir, length);
            var inDir = (Vector2)origin - projectile.Movement.PositionLastFrame;
            var outDir = Vector2.Reflect(inDir, hit.normal);
            projectile.Movement.MoveDirection = outDir;
        }

    }

}