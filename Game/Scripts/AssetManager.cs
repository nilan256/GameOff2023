using System.Collections.Generic;
using Game.GameAsset;
using Game.SkillSystem;
using Irisheep.Runtime;
using Irisheep.Runtime.Singleton;

namespace Game
{
    [DontDestroyOnLoad]
    public class AssetManager : SingletonMonoBehaviour<AssetManager>
    {

        public GameDatabase Database;

        public CharacterAsset GetCharacter(string id)
        {
            AssertDatabaseAssigned();
            return FindById(Database.Characters, id);
        }

        public EnemyAsset GetEnemy(string id)
        {
            AssertDatabaseAssigned();
            return FindById(Database.Enemies, id);
        }

        public LevelAsset GetLevel(string id)
        {
            AssertDatabaseAssigned();
            return FindById(Database.Levels, id);
        }

        public SkillData GetSkill(string id)
        {
            AssertDatabaseAssigned();
            if (id == Database.FallbackSkill.Id) return Database.FallbackSkill;
            return FindById(Database.Skills, id);
        }

        private T FindById<T>(List<T> list, string id) where T : AssetBase
        {
            return list.Find(item => item && item.Id == id);
        }

        private void AssertDatabaseAssigned()
        {
            AssertUtil.NotNull(Database, "Database is not assigned");
        }

    }

}