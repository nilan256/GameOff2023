using Game.Combat;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Attack/Independent")]
    public class AddIndependentAttackSkill : SkillData
    {

        public PlayerAttack Prefab;

        public override bool CanGain(Player player)
        {
            return true;
        }

        public override void OnGained(Player player)
        {
            player.AddIndependentAttack(Prefab);
        }

    }

}