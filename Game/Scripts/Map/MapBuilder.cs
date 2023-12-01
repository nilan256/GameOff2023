using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{

    public abstract class MapBuilder : MonoBehaviour
    {
        
        [FoldoutGroup("Basic Settings")]
        public Grid ChunkGrid;
        [FoldoutGroup("Basic Settings")]
        public bool InitializeOnStart;
        [FoldoutGroup("Basic Settings"), ShowIf(nameof(InitializeOnStart))]
        public int InitScaleLevel = 1;

        [FoldoutGroup("Runtime Info"), ShowInInspector, ReadOnly]
        public List<RuntimeMapChunk> Chunks { get; } = new List<RuntimeMapChunk>();
        [FoldoutGroup("Runtime Info"), ShowInInspector, ReadOnly]
        public Vector3Int TargetInChunk { get; private set; }
        [FoldoutGroup("Runtime Info"), ShowInInspector, ReadOnly]
        public int CurrentScaleLevel { get; private set; }
        private Camera mainCamera;
        private GameObject chunkContainer;
        
        public bool Initialized { get; protected set; }

        protected virtual void Start()
        {
            if (InitializeOnStart)
            {
                Initialize(InitScaleLevel);
            }
        }

        public virtual void Initialize(int worldScaleLevel)
        {
            mainCamera = Camera.main;
            if (!mainCamera)
            {
                Debug.LogError("No main camera in scene", this);
                return;
            }

            CurrentScaleLevel = worldScaleLevel;
            
            ProcessChunks();

            Initialized = true;
        }

        protected virtual void Update()
        {
            if (!mainCamera) return;
            ProcessChunks();
        }

        [Button(Expanded = true), DisableInEditorMode]
        public void ChangeScaleLevel(int newLevel)
        {
            CurrentScaleLevel = newLevel;
            ClearChunks();
            ProcessChunks();
        }

        protected void ProcessChunks()
        {
            var chunkPosition = ChunkGrid.WorldToCell(mainCamera.transform.position);
            TargetInChunk = chunkPosition;
            FillingVisibleChunks(chunkPosition);
            CullingInvisibleChunks(chunkPosition);
        }

        protected void FillingVisibleChunks(Vector3Int centerChunk)
        {
            for (int x = centerChunk.x - 1; x <= centerChunk.x + 1; x++)
            {
                for (int y = centerChunk.y - 1; y <= centerChunk.y + 1; y++)
                {
                    var chunkPos = new Vector3Int(x, y);
                    if (ContainsChunk(chunkPos)) continue;
                    var chunkObj = CreateChunk(CurrentScaleLevel, chunkPos);
                    var chunk = new RuntimeMapChunk(chunkObj, chunkPos);
                    InitializeNewChunk(chunk);
                    Chunks.Add(chunk);
                }
            }
        }

        protected void CullingInvisibleChunks(Vector3Int centerChunk)
        {
            for (var index = Chunks.Count - 1; index >= 0; index--)
            {
                var chunk = Chunks[index];
                if (Distance(centerChunk, chunk.ChunkPosition) > 1)
                {
                    DestroyChunk(chunk.Obj);
                    Chunks.RemoveAt(index);
                }
            }
        }

        protected abstract GameObject CreateChunk(int worldScaleLevel, Vector3Int chunkPosition);

        protected abstract void DestroyChunk(GameObject obj);

        private void InitializeNewChunk(RuntimeMapChunk chunk)
        {
            var position = ChunkGrid.GetCellCenterWorld(chunk.ChunkPosition);
            var half = Constants.PixelsPerChunk / 2;
            position += new Vector3(-half, half);
            chunk.Obj.transform.position = position;
            chunk.Obj.name = $"MapChunk({chunk.ChunkPosition.x}, {chunk.ChunkPosition.y})";

            if (!chunkContainer)
            {
                chunkContainer = new GameObject($"{name} - Chunks");
            }
            chunk.Obj.transform.SetParent(chunkContainer.transform);
        }

        private int Distance(Vector3Int a, Vector3Int b)
        {
            return Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y));
        }

        private bool ContainsChunk(Vector3Int chunkPosition)
        {
            return Chunks.Exists(chunk => chunk.ChunkPosition == chunkPosition);
        }

        private void ClearChunks()
        {
            foreach (var chunk in Chunks)
            {
                if (chunk.Obj) Destroy(chunk.Obj);
            }
            Chunks.Clear();
        }

    }

}