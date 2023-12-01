using Game.Combat;
using Irisheep.Runtime;
using System.Linq;
using Game.Combat.HitBehaviours;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Projectile/Penetrating")]
    public class PenetratingProjectile : SkillData
    {

        public override bool CanGain(Player player)
        {
            return !player.OwnSkills.Exists(skill=>skill is BouncyProjectile);
        }

        public override void OnGained(Player player)
        {
            var count = player.OwnSkills.Count(skill=>skill is PenetratingProjectile);
            var maximumHitCount = player.gameObject.GetOrAddComponent<MaximumHitCount>();
            maximumHitCount.Maximum = count;
        }

    }

}