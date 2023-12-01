using System.Linq;
using Game.Combat;
using Game.Combat.HitBehaviours;
using Irisheep.Runtime;
using UnityEngine;

namespace Game.SkillSystem
{

    [CreateAssetMenu(menuName = "Game/Skill/Projectile/Bouncy")]
    public class BouncyProjectile : SkillData
    {

        public override bool CanGain(Player player)
        {
            return !player.OwnSkills.Exists(skill=>skill is PenetratingProjectile);
        }

        public override void OnGained(Player player)
        {
            player.gameObject.GetOrAddComponent<BounceOnHit>();
            
            var count = player.OwnSkills.Count(skill=>skill is BouncyProjectile);
            var maximumHitCount = player.gameObject.GetOrAddComponent<MaximumHitCount>();
            maximumHitCount.Maximum = count;
        }

    }

}