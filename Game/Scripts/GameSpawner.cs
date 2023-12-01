using Game.Data;
using Game.Pickup;
using Irisheep.Runtime;
using Irisheep.Runtime.Pool;
using Irisheep.Runtime.Singleton;
using UnityEngine;

namespace Game
{

    public static class GameSpawner
    {

        #region Wrappers

        // because unity does not support generic type component
        // so here is some wrappers of ComponentPool

        public class EnemyPool : ComponentPool<Enemy> { }

        public class ProjectilePool : ComponentPool<Projectile> { }

        public class PickablePool : ComponentPool<Pickable> { }

        #endregion

        private static Spawner spawner;

        public static Spawner Spawner
        {
            get
            {
                SingletonMonoBehaviour.GetOrCreateSingleton(ref spawner);
                return spawner;
            }
        }

        public static void DestroyPoolOf(Component prefab)
        {
            if (!spawner) return;
            spawner.DestroyPoolOf(prefab);
        }

        public static Projectile Spawn(Projectile prefab)
        {
            return Spawner.Spawn<ProjectilePool, Projectile>(prefab);
        }

        public static Enemy Spawn(Enemy prefab)
        {
            return Spawner.Spawn<EnemyPool, Enemy>(prefab);
        }

        public static void SpawnXp(int value, Vector2 position)
        {
            if (GameplayController.Current.ActivePickableCount > 500) return;
            var prefab = AssetManager.Current.Database.Pickups.Xp;
            var inst = (PickableXp)Spawner.Spawn<PickablePool, Pickable>(prefab);
            inst.Value = value;
            inst.transform.position = position;
        }

    }

}