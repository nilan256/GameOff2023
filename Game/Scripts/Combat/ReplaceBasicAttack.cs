namespace Game.Combat
{

    public abstract class ReplaceBasicAttack : PlayerAttack
    {

        public override bool CanAttack()
        {
            return true;
        }

        public abstract bool ShouldReplaceBasicAttack(PlayerAttack basicAttack);

    }

}