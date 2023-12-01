using UnityEngine;

namespace Game.Map
{

    public class RuntimeMapChunk
    {

        public Vector3Int ChunkPosition;
        public GameObject Obj;

        public RuntimeMapChunk(GameObject obj, Vector3Int chunkPosition)
        {
            Obj = obj;
            ChunkPosition = chunkPosition;
        }

    }

}