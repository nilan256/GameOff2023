using System;
using UnityEngine;

namespace Game.Combat
{

    [DisallowMultipleComponent]
    public abstract class TargetFinder : ProjectileBehaviour
    {

        public abstract bool HasTarget();

        public abstract Vector2 GetTargetDirection();

        private void OnEnable()
        {
            Owner.TargetFinder = this;
        }

        protected override void OnDisableNotApplicationQuitting()
        {
            if (Owner.TargetFinder) Owner.TargetFinder = null;
        }

    }

}