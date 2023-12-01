namespace Game.FSM
{

    public abstract class SimpleState : GameState
    {

        public override void OnEnter()
        {
            base.OnEnter();
            Run();
            FinishState();
        }

        protected abstract void Run();

    }

}