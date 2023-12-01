using Game.GameAsset;
using HutongGames.PlayMaker;
using Irisheep.Runtime;
using UnityEngine;

namespace Game.FSM.States
{

    public class SpawnEnemyState : SimpleState
    {

        public FsmFloat maxDistance;
        
        protected override void Run()
        {
            var waveInfo = GetWaveSpawn();
            if (waveInfo != null)
            {
                for (int i = 0; i < waveInfo.SpawnAmount; i++)
                {
                    var enemy = GameSpawner.Spawn(waveInfo.Enemy.Prefab);
                    var spawnPoint = RandomPointOffscreen(Gameplay.MainCamera.GameCamera, maxDistance.Value);
                    enemy.transform.position = spawnPoint;
                }
            }

            Gameplay.LastWaveSpawnTime = Time.time;
        }
        
        public static Vector2 RandomPointOffscreen(Camera camera, float maxViewportDistance)
        {
            
            var x = Random.Range(-maxViewportDistance, maxViewportDistance);
            if (x > 0) x += 1;
            var y = Random.Range(-maxViewportDistance, 1 + maxViewportDistance);
            
            if (Random.value < 0.5)
            {
                (x, y) = (y, x);
            }
            var viewportPoint = new Vector3(x, y);
            var worldPoint = camera.ViewportToWorldPoint(viewportPoint);
            return worldPoint;
        }

        public WaveInfo GetWaveSpawn()
        {
            var playerLevel = Gameplay.Player.PlayerLevel;
            return Gameplay.LevelAsset.Waves.PickRandomWeight(wave=>wave.GetWeight(playerLevel));
        }

    }

}