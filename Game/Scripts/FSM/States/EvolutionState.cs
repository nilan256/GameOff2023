using System.Collections;

namespace Game.FSM.States
{

    public class EvolutionState : CoroutineState
    {

        protected override IEnumerator Run()
        {
            yield return Player.Evolve();
        }

    }

}