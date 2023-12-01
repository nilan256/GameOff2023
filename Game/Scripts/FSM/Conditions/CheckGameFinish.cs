namespace Game.FSM.Conditions
{

    public class CheckGameFinish : ConditionAction
    {

        protected override bool Test()
        {
            return Gameplay.IsWin || Player.CurrentHealth <= 0;
        }

    }

}