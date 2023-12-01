namespace Game.FSM.Conditions
{

    public class CheckEvolution : ConditionAction
    {

        protected override bool Test()
        {
            if (!Player) return false;
            return Evaluator.GetEvolutions(Player.PlayerLevel) > Player.Evolutions;
        }

    }

}