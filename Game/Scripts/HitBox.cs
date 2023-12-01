using Irisheep.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{

    public class HitBox : MonoBehaviour
    {

        public enum HitType
        {

            Once,
            Continuous,

        }

        public LayerMask HitLayer;
        public HitType Type = HitType.Once;
        public UnityEvent<Collider2D> HitEvent = new UnityEvent<Collider2D>();

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (Type != HitType.Once) return;
            if (!HitLayer.Contains(other)) return;
            HitEvent.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (Type != HitType.Continuous) return;
            if (!HitLayer.Contains(other)) return;
            HitEvent.Invoke(other);
        }

    }

}