using Sirenix.OdinInspector;

namespace Game.Combat
{

    public class CountBaseReplaceBasicAttack : ReplaceBasicAttack
    {

        [MinValue(1)]
        public int ReplaceAt = 3;

        public override bool ShouldReplaceBasicAttack(PlayerAttack basicAttack)
        {
            return basicAttack.AttackCount % ReplaceAt == 0;
        }

    }

}