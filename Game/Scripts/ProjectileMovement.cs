using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileMovement : TopDownMovement
    {

        [TitleGroup(InspectorGroup.Parameters)]
        public bool FaceMoveDirection;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [ShowIf(nameof(FaceMoveDirection))]
        public Vector2 DefaultForwardDirection = Vector2.right;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (FaceMoveDirection)
            {
                var rot = Vector2.SignedAngle(Vector2.up, MoveDirection);
                rot -= Vector2.SignedAngle(Vector2.up, DefaultForwardDirection);
                rb.MoveRotation(rot);
            }
        }

    }

}