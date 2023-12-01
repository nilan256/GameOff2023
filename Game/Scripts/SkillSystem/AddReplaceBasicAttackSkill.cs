using Game.Combat;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Attack/Replace Basic")]
    public class AddReplaceBasicAttackSkill : SkillData
    {

        public ReplaceBasicAttack Prefab;

        public override bool CanGain(Player player)
        {
            return true;
        }

        public override void OnGained(Player player)
        {
            player.AddReplaceBasicAttack(Prefab);
        }

    }

}