using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameAsset
{

    public abstract class AssetBase : ScriptableObject
    {

        public string Id;

        public override string ToString()
        {
            return $"<{GetType().Name}>({Id})";
        }

    }

}