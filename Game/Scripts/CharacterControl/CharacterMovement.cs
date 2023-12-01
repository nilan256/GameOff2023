using System;
using Game.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.CharacterControl
{

    [RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
    public class CharacterMovement : TopDownMovement
    {
        
        [TitleGroup(InspectorGroup.Parameters)]
        public HorizontalFlipMethod HorizontalFlip;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [ShowIf(nameof(HorizontalFlip), HorizontalFlipMethod.TransformScale)]
        public Transform FlipTransform;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [ShowIf(nameof(HorizontalFlip), HorizontalFlipMethod.SpriteRenderer)]
        public SpriteRenderer FlipSpriteRenderer;
        
        [TitleGroup(InspectorGroup.Parameters)]
        [HideIf(nameof(HorizontalFlip), HorizontalFlipMethod.None)]
        public HorizontalDirection DefaultFaceDirection = HorizontalDirection.Right;
            
        public bool IsMoving
        {
            get
            {
                return MoveDirection != Vector2.zero;
            }
        }

        private void Update()
        {
            UpdateFlip();
        }

        private void UpdateFlip()
        {
            if (HorizontalFlip == HorizontalFlipMethod.None) return;
            if (MoveDirection == Vector2.zero) return;
            var needFlip = false;
            if (DefaultFaceDirection == HorizontalDirection.Right)
            {
                needFlip = MoveDirection.x < 0;
            }
            else
            {
                needFlip = MoveDirection.x > 0;
            }
            switch (HorizontalFlip)
            {
            case HorizontalFlipMethod.TransformScale:
                var tr = FlipTransform ? FlipTransform : transform;
                tr.localScale = needFlip ? new Vector3(-1, 1, 1) : Vector3.one;
                break;
            case HorizontalFlipMethod.SpriteRenderer:
                if (FlipSpriteRenderer) FlipSpriteRenderer.flipX = needFlip;
                break;
            }
        }

        public void Knockback(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }

    }

}