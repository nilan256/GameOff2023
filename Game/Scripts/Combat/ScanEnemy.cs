using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat
{

    public class ScanEnemy : TargetFinder
    {

        public LayerMask EnemyLayer;
        public Vector2 VisionSize;

        [ShowInInspector, ReadOnly]
        private Enemy nearest;
        private int version;
        private RaycastHit2D[] buffer = new RaycastHit2D[10];

        public override bool HasTarget()
        {
            return FindNearest();
        }

        public override Vector2 GetTargetDirection()
        {
            var target = FindNearest();
            if (target)
            {
                return target.Position - (Vector2)transform.position;
            }
            return Random.insideUnitCircle;
        }

        public Enemy FindNearest()
        {
            RescanIfNeed();
            return nearest;
        }

        private void RescanIfNeed()
        {
            if (version == Time.frameCount) return;
            Rescan();
            version = Time.frameCount;
        }
        
        private void Rescan()
        {
            nearest = null;
            
            var position = transform.position;
            var count = Physics2D.BoxCastNonAlloc
            (
                position, 
                VisionSize, 
                0, 
                Vector2.zero, 
                buffer, 
                0,
                EnemyLayer.value
            );
            var minSqrDistance = float.MaxValue;
            for (int i = 0; i < count; i++)
            {
                var hit = buffer[i];
                if (!hit.transform.TryGetComponent<Enemy>(out var enemy))
                {
                    continue;
                }
                var dir = hit.transform.position - position;
                var sqrMagnitude = dir.sqrMagnitude;
                if (sqrMagnitude < minSqrDistance)
                {
                    nearest = enemy;
                    minSqrDistance = sqrMagnitude;
                }
            }
        }

    }

}