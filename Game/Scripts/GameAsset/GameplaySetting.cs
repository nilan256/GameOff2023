using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Data/GameplaySetting")]
    public class GameplaySetting : ScriptableObject
    {

        public int MinimumEnemyAmount;
        public int MaximumEnemyAmount;
        public float TimeBaseSpawnInterval;
        public int MaximumFinitePlayerLevel;
        public int MaximumWorldScaleLevel;
        public int MaximumEvolutions;

    }

}