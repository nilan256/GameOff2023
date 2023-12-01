using Game.Combat;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Attack/Basic")]
    public class ChangeBasicAttackSkill : SkillData
    {

        public PlayerAttack Prefab;

        public override bool CanGain(Player player)
        {
            return !player.OwnSkills.Exists(skill=>skill == this);
        }

        public override void OnGained(Player player)
        {
            player.ChangeBasicAttack(Prefab);
        }

    }

}