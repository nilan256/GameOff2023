namespace Game.FSM.Conditions
{

    public class CheckSkillBonus : ConditionAction
    {

        protected override bool Test()
        {
            return Gameplay.PendingSkillBonus > 0;
        }

    }

}