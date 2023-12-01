using System.Collections.Generic;
using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Level")]
    public class LevelAsset : AssetBase
    {
        
        public List<WaveInfo> Waves = new List<WaveInfo>();

    }

}