namespace Game.FSM.Conditions
{

    public class CheckWorldScaleLevelUp : ConditionAction
    {

        protected override bool Test()
        {
            if (!Player) return false;
            return Evaluator.GetWorldScaleLevel(Player.PlayerLevel) > Gameplay.WorldScale;
        }

    }

}