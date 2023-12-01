using UnityEngine;

namespace Game.FSM.Conditions
{

    public class CheckNeedSpawnEnemy : ConditionAction
    {

        protected override bool Test()
        {
            var setting = AssetManager.Current.Database.GameplaySetting;

            if (Enemy.AliveEnemyCount >= setting.MaximumEnemyAmount) return false;

            if (Enemy.AliveEnemyCount < setting.MinimumEnemyAmount) return true;
            
            return Time.time - Gameplay.LastWaveSpawnTime > setting.TimeBaseSpawnInterval;
        }

    }

}