using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{

    [CreateAssetMenu(menuName = "Game/World Map Config")]
    public class WorldMapConfig : ScriptableObject
    {

        public int ScaleLevel;

        [ListDrawerSettings(NumberOfItemsPerPage = 5)]
        public List<GameObject> Chunks = new List<GameObject>();
        
        [ListDrawerSettings(NumberOfItemsPerPage = 5)]
        public List<ChunkMappingInfo> MappingInfos = new List<ChunkMappingInfo>();

#if UNITY_EDITOR

        public class ImporterWindow : Sirenix.OdinInspector.Editor.OdinEditorWindow
        {

            [Multiline(10)]
            public string Input;

            public WorldMapConfig Config;

            [Button]
            public void Import()
            {
                var nameToPrefab = Config.Chunks.ToDictionary(e=>e.name);
                Config.MappingInfos.Clear();
                foreach (var line in Input.Split('\n'))
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    var cols = line.Split('\t');
                    if (cols.Length != 3) continue;
                    var x = int.Parse(cols[0]);
                    var y = int.Parse(cols[1]);
                    var chunkName = cols[2].Trim();
                    if (!nameToPrefab.ContainsKey(chunkName))
                    {
                        Debug.LogWarning($"Not found chunk '{chunkName}'");
                        continue;
                    }
                    Config.MappingInfos.Add(new ChunkMappingInfo()
                    {
                        Position = new Vector2Int(x, y),
                        ChunkName = chunkName,
                    });
                }

                UnityEditor.EditorUtility.SetDirty(Config);
            }

        }

        [Button]
        public void ImportMappingInfo()
        {
            var window = ImporterWindow.GetWindow<ImporterWindow>(true);
            window.Config = this;
        }

        [Button]
        public void FlipYCoord()
        {
            var maxY = MappingInfos.Max(item => item.Position.y);
            foreach (var info in MappingInfos)
            {
                var y = info.Position.y;
                info.Position.y = maxY - y;
            }
        }
        
#endif
        
    }

}