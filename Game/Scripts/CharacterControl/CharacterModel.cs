using System;
using UnityEngine;

namespace Game.CharacterControl
{

    public class CharacterModel : MonoBehaviour
    {

        public Animator Animator;
        public float Width;
        public float Height;
        private static readonly int paramNameWalking = Animator.StringToHash("Walking");

        private void Reset()
        {
            Animator = GetComponent<Animator>();
            if (TryGetComponent<SpriteRenderer>(out var sp))
            {
                Width = sp.sprite.rect.width;
                Height = sp.sprite.rect.height;
            }
        }

        public virtual void SetWalking(bool value)
        {
            Animator.SetBool(paramNameWalking, value);
        }

    }

}