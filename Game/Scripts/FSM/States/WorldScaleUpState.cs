using System.Collections;

namespace Game.FSM.States
{

    public class WorldScaleUpState : CoroutineState
    {

        public WorldScaleAnimation Animation;

        protected override IEnumerator Run()
        {
            Gameplay.WorldScale += 1;
            yield return Animation.Play();
        }

    }

}