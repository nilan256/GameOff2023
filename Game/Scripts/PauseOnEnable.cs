using System;
using UnityEngine;

namespace Game
{

    public class PauseOnEnable : MonoBehaviour
    {

        private void OnEnable()
        {
            if (GameManager.HasInstance)
            {
                GameManager.Current.SetPause(true);
            }
        }

        private void OnDisable()
        {
            if (!GameManager.IsApplicationQuitting && GameManager.HasInstance)
            {
                GameManager.Current.SetPause(false);
            }
        }

    }

}