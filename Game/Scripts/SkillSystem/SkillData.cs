using Game.GameAsset;
using UnityEngine;

namespace Game.SkillSystem
{

    public abstract class SkillData : AssetBase
    {

        public string DisplayName;
        [Multiline]
        public string DisplayDescription;
        public Sprite Icon;
        public float Weight = 1f;

        public abstract bool CanGain(Player player);

        public abstract void OnGained(Player player);

    }

}