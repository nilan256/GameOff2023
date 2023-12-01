using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownMovement : MonoBehaviour
    {

        [TitleGroup(InspectorGroup.Parameters)]
        public Vector2 MoveDirection;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float MoveSpeed;

        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float AccelerationTime;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [MinValue(0)]
        public float DecelerationTime;
        
        protected Rigidbody2D rb;
        
        public Vector2 PositionLastFrame { get; protected set; }
        
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        protected virtual void FixedUpdate()
        {
            var currentSpeed = rb.velocity.magnitude;

            var targetVelocity = MoveDirection.normalized * MoveSpeed;
            if (currentSpeed < MoveSpeed && AccelerationTime > 0)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime / AccelerationTime);
            }
            else if (currentSpeed > MoveSpeed && DecelerationTime > 0)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime / DecelerationTime);
            }
            else
            {
                rb.velocity = targetVelocity;
            }
            
            PositionLastFrame = rb.position;
        }

    }

}