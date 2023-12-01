using Irisheep.Runtime.Singleton;
using UnityEngine;

namespace Game
{

    [DontDestroyOnLoad]
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {

        private bool isPausing;
        private float lastTimeScale;
        
        public void SetPause(bool pause)
        {
            if (pause)
            {
                if (isPausing) return;
                lastTimeScale = Time.timeScale;
                Time.timeScale = 0;
                isPausing = true;
            }
            else
            {
                if (!isPausing) return;
                Time.timeScale = lastTimeScale;
                isPausing = false;
            }
        }

    }

}