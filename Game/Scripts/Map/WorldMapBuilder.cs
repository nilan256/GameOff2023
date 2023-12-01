using System.Collections.Generic;
using System.Linq;
using Irisheep.Runtime;
using UnityEngine;

namespace Game.Map
{

    public class WorldMapBuilder : MapBuilder
    {

        public List<WorldMapConfig> Configs = new List<WorldMapConfig>();
        public GameObject OutsideChunk;
        private int loadedConfigLevel = -1;
        private Dictionary<Vector3Int, GameObject> chunkMap = new Dictionary<Vector3Int, GameObject>();
        
        protected override GameObject CreateChunk(int worldScaleLevel, Vector3Int chunkPosition)
        {
            if (worldScaleLevel != loadedConfigLevel)
            {
                var config = Configs.Find(conf=>conf.ScaleLevel == worldScaleLevel);
                AssertUtil.NotNull(config);
                RebuildChunkMap(config);
                loadedConfigLevel = worldScaleLevel;
            }

            if (!chunkMap.TryGetValue(chunkPosition, out var prefab))
            {
                prefab = OutsideChunk;
            }

            var chunk = Instantiate(prefab);
            return chunk;
        }

        protected override void DestroyChunk(GameObject obj)
        {
            Destroy(obj);
        }

        private void RebuildChunkMap(WorldMapConfig config)
        {
            chunkMap.Clear();
            var nameToPrefab = config.Chunks.ToDictionary(e => e.name);
            foreach (var mappingInfo in config.MappingInfos)
            {
                if (!nameToPrefab.TryGetValue(mappingInfo.ChunkName, out var prefab))
                {
                    throw new KeyNotFoundException(mappingInfo.ChunkName);
                }

                chunkMap[(Vector3Int)mappingInfo.Position] = prefab;
            }
        }

    }

}