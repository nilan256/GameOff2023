using System.Collections.Generic;
using Game.CharacterControl;
using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Model Series")]
    public class ModelSeries : AssetBase
    {

        public List<CharacterModel> Models = new List<CharacterModel>();

        public CharacterModel GetModel(int evolutions)
        {
            var index = evolutions - 1;
            if (index < 0 || index >= Models.Count) return null;
            return Models[index];
        }

    }

}