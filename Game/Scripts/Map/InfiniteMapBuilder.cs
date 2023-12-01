using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{

    public class InfiniteMapBuilder : MapBuilder
    {

        public List<Entry> Entries = new List<Entry>();

        protected override GameObject CreateChunk(int worldScaleLevel, Vector3Int chunkPosition)
        {
            var prefab = GetPrefabByLevel(worldScaleLevel);
            var inst = Instantiate(prefab, transform);
            return inst;
        }

        protected override void DestroyChunk(GameObject obj)
        {
            Destroy(obj);
        }

        private GameObject GetPrefabByLevel(int level)
        {
            if (Entries.Count == 0)
            {
                Debug.LogError("Empty chunk set", this);
                return null;
            }
            var entry = Entries.Find(e=>e.Level == level);
            if (entry == null)
            {
                Debug.LogError($"No prefab defined with level {level}", this);
                entry = Entries[0];
            }

            return entry.Prefab;
        }

        [Serializable]
        public class Entry
        {

            public int Level;
            public GameObject Prefab;

        }

    }

}