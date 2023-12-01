using UnityEngine;

namespace Game
{

    public class GameBehaviour : MonoBehaviour
    {

        public GameplayController Gameplay => GameplayController.Current;
        protected Player Player => Gameplay.Player;

        protected virtual void OnDisable()
        {
            if (!GameManager.IsApplicationQuitting)
            {
                OnDisableNotApplicationQuitting();
            }
        }

        protected virtual void OnDestroy()
        {
            if (!GameManager.IsApplicationQuitting)
            {
                OnDestroyNotApplicationQuitting();
            }
        }

        protected virtual void OnDisableNotApplicationQuitting() { }

        protected virtual void OnDestroyNotApplicationQuitting() { }

    }

}