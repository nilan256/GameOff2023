using Game.Events;
using Irisheep.Runtime;
using QFSW.QC;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Debugging
{

    public static class ConsoleCommands
    {

        private static GameplayController Gameplay => GameplayController.Current;
        private static Player Player => Gameplay.Player;

        [Command(aliasOverride:"spawn-xp")]
        public static void SpawnXp(int count, int value=10)
        {
            for (int i = 0; i < count; i++)
            {
                GameSpawner.SpawnXp(value, PickRandomPositionNearbyPlayer());
            }
        }

        [Command("spawn-enemy")]
        public static void SpawnEnemy(string id, int count = 1)
        {
            var enemy = AssetManager.Current.GetEnemy(id);
            for (int i = 0; i < count; i++)
            {
                var inst = GameSpawner.Spawn(enemy.Prefab);
                inst.transform.position = PickRandomPositionNearbyPlayer();
            }
        }

        [Command(aliasOverride:"levelup")]
        public static void LevelUp(int amount = 1)
        {
            AssertUtil.NotNull(AssetManager.Current);
            if (amount <= 0) return;
            var totalXp = 0;
            for (int i = 0; i < amount; i++)
            {
                var lv = Player.PlayerLevel + i;
                if (i == 0)
                {
                    totalXp += Player.XpRequired - Player.Xp;
                }
                else
                {
                    totalXp += Evaluator.GetXpRequired(lv);
                }
            }
            
            GainXpEvent.Trigger(totalXp);
        }

        [Command("skill-add")]
        public static void SkillAdd(string id)
        {
            var skill = AssetManager.Current.GetSkill(id);
            Player.AddSkill(skill);
        }

        [Command("tp")]
        public static void Teleport(Vector2 position)
        {
            Player.transform.position = position;
        }

        [Command("test")]
        public static void Test()
        {
            GameplayController.Current.MainCamera.UpdateScreenSize(135*2, 3);
        }

        private static Vector3 PickRandomPositionNearbyPlayer()
        {
            var playerPosition = Player.transform.position;
            var dir = (Vector3) Random.insideUnitCircle;
            var point = playerPosition + dir * 100;
            point = Vector3.LerpUnclamped(playerPosition, point, Random.value + 1f);
            return point;
        }

    }

}