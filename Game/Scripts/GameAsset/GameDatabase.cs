using System.Collections.Generic;
using Game.SkillSystem;
using Irisheep.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameAsset
{

    [CreateAssetMenu(menuName = "Game/Data/Database", order = 0)]
    public class GameDatabase : ScriptableObject
    {

        [ListDrawerSettings(ListElementLabelName = nameof(AssetBase.Id),OnTitleBarGUI = nameof(GUI_RefreshCharacters))]
        public List<CharacterAsset> Characters = new List<CharacterAsset>();

        [ListDrawerSettings(ListElementLabelName = nameof(AssetBase.Id),OnTitleBarGUI = nameof(GUI_RefreshEnemies))]
        public List<EnemyAsset> Enemies = new List<EnemyAsset>();
        
        [ListDrawerSettings(ListElementLabelName = nameof(AssetBase.Id),OnTitleBarGUI = nameof(GUI_RefreshProjectiles))]
        public List<ProjectileAsset> Projectiles = new List<ProjectileAsset>();
        
        [ListDrawerSettings(ListElementLabelName = nameof(AssetBase.Id), OnTitleBarGUI = nameof(GUI_RefreshLevels))]
        public List<LevelAsset> Levels = new List<LevelAsset>();

        [ListDrawerSettings(ListElementLabelName = nameof(AssetBase.Id), OnTitleBarGUI = nameof(GUI_RefreshSkills))]
        public List<SkillData> Skills = new List<SkillData>();

        public SkillData FallbackSkill;

        public GeneralPickups Pickups;

        public GameplaySetting GameplaySetting;


        [Button(SdfIconType.ArrowRepeat, IconAlignment.RightEdge, Stretch = false, ButtonAlignment = 1f)]
        public void RefreshAll()
        {
            RefreshAssetList(Characters);
            RefreshAssetList(Enemies);
            RefreshAssetList(Projectiles);
            RefreshAssetList(Levels);
            RefreshAssetList(Skills);
            EditorTools.SetDirty(this);
        }

        private void GUI_RefreshCharacters()
        {
            DrawRefreshAssetListButton(Characters);
        }

        private void GUI_RefreshEnemies()
        {
            DrawRefreshAssetListButton(Enemies);
        }

        private void GUI_RefreshProjectiles()
        {
            DrawRefreshAssetListButton(Projectiles);
        }

        private void GUI_RefreshLevels()
        {
            DrawRefreshAssetListButton(Levels);
        }

        private void GUI_RefreshSkills()
        {
            DrawRefreshAssetListButton(Skills);
        }

        private void DrawRefreshAssetListButton<T>(List<T> list) where T : Object
        {
#if UNITY_EDITOR
            if (!Sirenix.Utilities.Editor.SirenixEditorGUI.ToolbarButton(Sirenix.Utilities.Editor.EditorIcons.Refresh)) return;
            RefreshAssetList(list);
            EditorTools.SetDirty(this);
#endif
        }

        private void RefreshAssetList<T>(List<T> list) where T : Object
        {
            // match NOT starts with '_'
            EditorTools.RefreshAssetsList(list, filenameFilterRegex: "^[^_].*");
        }
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Game/Database/Refresh")]
        public static void RefreshDatabaseMenuItem()
        {
            var database = EditorTools.FindAssetOfType<GameDatabase>();
            AssertUtil.Found(database);
            database.RefreshAll();
        }
#endif
        
    }

}