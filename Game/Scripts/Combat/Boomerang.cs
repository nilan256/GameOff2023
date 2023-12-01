using Irisheep.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Combat
{

    public class Boomerang : Projectile
    {
        [TitleGroup(InspectorGroup.Parameters)][MinValue(0)]
        public float MaxDistance;
        [TitleGroup(InspectorGroup.Parameters)]
        public Vector2 OffsetRange;

        public bool IsBacking => currentTime > totalTimeOfPath / 2;

        private Vector2 initialPosition;
        private Vector2 destinationPosition;
        private Vector2 lastPoint;
        private float totalTimeOfPath;
        private float currentTime;
        private float curveStrength;

        public override void SetMoveDirection(Vector2 direction)
        {
            initialPosition = transform.position;
            curveStrength = MaxDistance * 2; // if right, strength must be positive
            
            var destDir = -Vector2.Perpendicular(direction.normalized); // default to right side
            if (Random.value < 0.5)
            {
                destDir = -destDir; // flip to left side
                curveStrength *= -1; // if left side, strength must be negative
            }
            var offset = Random.Range(OffsetRange.x, OffsetRange.y);
            destinationPosition = initialPosition + destDir * offset;
            
            // approximate length calculated by treating the path as a triangle
            var w = Vector2.Distance(initialPosition, destinationPosition);
            var length = Mathf.Sqrt(Mathf.Pow(w / 2, 2) + Mathf.Pow(MaxDistance, 2)) * 2;
            
            totalTimeOfPath = length / Speed;
            lastPoint = transform.position;
        }

        public override void Fire()
        {
            currentTime = 0;
        }

        protected override void Update()
        {
            base.Update();
            if (currentTime < totalTimeOfPath)
            {
                var t = currentTime / totalTimeOfPath;
                
                var point = MathUtil.QuadraticBezier(initialPosition, destinationPosition, curveStrength, t);
                Movement.MoveDirection = point - lastPoint;
                lastPoint = point;
                
                var speedMultiplier = 1 - (MathUtil.SmoothStep(0, 0.5f, t) - MathUtil.SmoothStep(0.5f, 1, t));
                Movement.MoveSpeed = Speed * speedMultiplier;
                
                currentTime += Time.deltaTime;
            }
            else
            {
                Movement.MoveSpeed = Speed;
            }
        }

    }

}