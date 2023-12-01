namespace Game.FSM.Conditions
{

    public class CheckLevelUp : ConditionAction
    {

        protected override bool Test()
        {
            if (!Player) return false;
            return Player.Xp >= Player.XpRequired;
        }

    }

}