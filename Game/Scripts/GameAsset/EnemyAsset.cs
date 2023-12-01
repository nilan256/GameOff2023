using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Enemy")]
    public class EnemyAsset : AssetBase
    {

        [AssetsOnly, Required]
        public Enemy Prefab;

    }

}