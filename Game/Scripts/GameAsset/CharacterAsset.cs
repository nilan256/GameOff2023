using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Character")]
    public class CharacterAsset : AssetBase
    {

        [AssetsOnly, Required]
        public GameObject Prefab;

    }

}