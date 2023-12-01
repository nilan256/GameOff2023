using System.Collections.Generic;
using Irisheep.Runtime;

namespace Game.SkillSystem
{

    public class SkillPool
    {

        private List<SkillData> skills = new List<SkillData>();
        
        public SkillData Fallback { get; set; }

        public SkillPool(IEnumerable<SkillData> skills)
        {
            this.skills.AddRange(skills);
            this.skills.Shuffle();
        }

        public SkillData Get(Player player)
        {
            var list = skills.FindAll(skill=>skill.CanGain(player));
            var skill = list.PickRandomWeight(skill=>skill.Weight);
            if (skill) return skill;
            return Fallback;
        }

    }

}