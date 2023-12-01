using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameAsset
{

    [Serializable]
    public class WaveInfo
    {

        [Required]
        public EnemyAsset Enemy;

        [MinValue(1)]
        public int SpawnAmount = 5;
        
        public List<PlayerLevelSpawnWeightPair> WeightSettings = new List<PlayerLevelSpawnWeightPair>();

        [Tooltip("used if no weight setting matched")]
        [MinValue(0)]
        public float DefaultWeight;

        public float GetWeight(int playerLevel)
        {
            foreach (var pair in WeightSettings)
            {
                if (playerLevel >= pair.PlayerLevelRange.x && playerLevel <= pair.PlayerLevelRange.y)
                {
                    return pair.SpawnWeight;
                }
            }

            return DefaultWeight;
        }

        [InlineProperty]
        [Serializable]
        public class PlayerLevelSpawnWeightPair
        {

            [LabelText("lv")]
            [HorizontalGroup]
            public Vector2Int PlayerLevelRange;

            [LabelText("weight")]
            [HorizontalGroup]
            [MinValue(0)]
            public float SpawnWeight;

        }

    }

}