using Com.LuisPedroFonseca.ProCamera2D;
using Game.GameAsset;
using UnityEngine;
using Game.Map;
using Game.SkillSystem;
using Irisheep.Runtime.Singleton;
using Sirenix.OdinInspector;

namespace Game
{

    public sealed class GameplayController : SingletonMonoBehaviour<GameplayController>
    {

        #region Serialized Fields
        
        public ProCamera2D MainCamera;
        public MapBuilder MapBuilder;
        public Vector2 InitialPlayerPosition;

        #endregion

        #region Fields And Properties
        
        public int WorldScale { get; set; } = 1;
        public Player Player { get; set; }
        public LevelAsset LevelAsset { get; private set; }
        public float LastWaveSpawnTime { get; set; }
        public int PendingSkillBonus { get; set; }
        public bool Initialized { get; private set; }
        public SkillPool SkillPool { get; private set; }
        public int TotalKills { get; set; }
        public int ActivePickableCount { get; set; }

        public bool IsWin =>
            Player.PlayerLevel >= AssetManager.Current.Database.GameplaySetting.MaximumFinitePlayerLevel;
        
        #endregion

        #region Public Methods

        public void Initialize()
        {
            LevelAsset = AssetManager.Current.GetLevel(GameSession.Current.SelectedLevelId);
            SkillPool = new SkillPool(AssetManager.Current.Database.Skills);
            SkillPool.Fallback = AssetManager.Current.Database.FallbackSkill;

            SpawnPlayer();
            
            Initialized = true;
        }

        #endregion

        #region Nonpublic Methods

        [Button]
        private void SpawnPlayer()
        {
            var asset = AssetManager.Current.GetCharacter(GameSession.Current.SelectedPlayerCharacterId);
            var playerGo = Instantiate(asset.Prefab, InitialPlayerPosition, Quaternion.identity);
            Player = playerGo.GetComponent<Player>();
        }

        #endregion

    }

}