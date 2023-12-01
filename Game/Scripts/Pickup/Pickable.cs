using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Pickup
{

    public abstract class Pickable : GameBehaviour
    {
        [MinValue(0)]
        public float MoveSpeed = 10;
        public float ReachDistance = 5;
        protected bool picking;

        public void OnTriggerEnter2D(Collider2D other)
        {
            OnPickup(other);
        }

        protected virtual void OnEnable()
        {
            GameplayController.Current.ActivePickableCount += 1;
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            GameplayController.Current.ActivePickableCount -= 1;
        }

        public void OnPickup(Collider2D other)
        {
            if (picking) return;
            picking = true;
            StartCoroutine(AnimationCoroutine(other.transform));
        }

        protected abstract void OnPicked();

        private IEnumerator AnimationCoroutine(Transform target)
        {
            var sqrReachDist = ReachDistance * ReachDistance;
            var tr = transform;
            var speed = MoveSpeed;
            
            while (true)
            {
                if (!target) break;
                var isReach = Vector3.SqrMagnitude(target.position - tr.position) < sqrReachDist;
                if (isReach) break;
                tr.position = Vector3.MoveTowards(tr.position, target.position, speed * Time.deltaTime);

                yield return null;
            }

            OnPickAnimationEnd();
        }

        private void OnPickAnimationEnd()
        {
            picking = false;
            OnPicked();
            gameObject.SetActive(false);
        }

    }

}