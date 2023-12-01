using HutongGames.PlayMaker;

namespace Game.FSM
{

    [ActionCategory("GameState")]
    public abstract class GameState : GameAction
    {
        public FsmEvent finishEvent;

        protected void FinishState()
        {
            if (finishEvent != null)
            {
                Fsm.Event(finishEvent);
            }
            Finish();
        }

    }

}