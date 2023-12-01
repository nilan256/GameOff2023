using System.Collections;
using UnityEngine;

namespace Game.FSM
{

    public abstract class CoroutineState : GameState
    {

        public bool ShouldPauseGame;

        protected bool isCoroutineCompleted;
        private Coroutine coroutine;

        public override void OnEnter()
        {
            base.OnEnter();
            if (ShouldPauseGame)
            {
                GameManager.Current.SetPause(true);
            }
            isCoroutineCompleted = false;
            coroutine = StartCoroutine(InternalCoroutine());
        }

        public override void OnExit()
        {
            if (GameplayController.IsApplicationQuitting) return;
            if (ShouldPauseGame)
            {
                GameManager.Current.SetPause(false);
            }
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            OnCoroutineStop(isCoroutineCompleted);
            base.OnExit();
        }

        protected abstract IEnumerator Run();

        protected virtual void OnCoroutineStop(bool isCompleted) { }

        private IEnumerator InternalCoroutine()
        {
            yield return Run();
            coroutine = null;
            isCoroutineCompleted = true;
            FinishState();
        }
        

    }

}