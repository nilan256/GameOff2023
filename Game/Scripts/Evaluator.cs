using Game.GameAsset;
using UnityEngine;

namespace Game
{

    public static class Evaluator
    {

        private static GameplaySetting Setting => AssetManager.Current.Database.GameplaySetting;
        
        public static int GetXpRequired(int level)
        {
            return 100 + level * 10;
        }

        public static int GetEvolutions(int characterLevel)
        {
            return Mathf.Min(characterLevel / 5 + 1, Setting.MaximumEvolutions);
        }

        public static int GetWorldScaleLevel(int characterLevel)
        {
            return Mathf.Min(characterLevel / 15 + 1, Setting.MaximumWorldScaleLevel);
        }

        public static bool IsUniqueSkillBonusLevel(int characterLevel)
        {
            return characterLevel <= Setting.MaximumFinitePlayerLevel && characterLevel % 15 == 0;
        }
        

    }

}