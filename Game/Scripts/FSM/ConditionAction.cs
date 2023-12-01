using HutongGames.PlayMaker;

namespace Game.FSM
{

    public abstract class ConditionAction : GameAction
    {

        public FsmEvent TrueEvent;
        public FsmEvent FalseEvent;
        public bool EveryFrame;

        protected abstract bool Test();

        public override void OnEnter()
        {
            if (EveryFrame) return;
            if (Test())
            {
                Fsm.Event(TrueEvent);
            }
            else
            {
                Fsm.Event(FalseEvent);
            }
            Finish();
        }

        public override void OnUpdate()
        {
            if (!EveryFrame) return;
            if (Fsm.IsSwitchingState) return;
            
            if (Test())
            {
                Fsm.Event(TrueEvent);
            }
            else
            {
                Fsm.Event(FalseEvent);
            }
        }

    }

}