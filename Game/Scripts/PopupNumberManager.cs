using DamageNumbersPro;
using Game.Events;
using Irisheep.Runtime;
using Irisheep.Runtime.Singleton;
using UnityEngine;

namespace Game
{

    public class PopupNumberManager : SingletonMonoBehaviour<PopupNumberManager>,
        IGameEventListener<GainXpEvent>,
        IGameEventListener<LevelUppedEvent>,
        IGameEventListener<EnemyHealthChangedEvent>,
        IGameEventListener<PlayerHealthChangedEvent>
    {

        public Vector3 PlayerPositionOffset;
        public DamageNumber XpPrefab;
        public DamageNumber LevelUpPrefab;
        public DamageNumber DamagePrefab;

        private void OnEnable()
        {
            this.StartListening<GainXpEvent>();
            this.StartListening<LevelUppedEvent>();
            this.StartListening<PlayerHealthChangedEvent>();
            this.StartListening<EnemyHealthChangedEvent>();
        }

        private void OnDisable()
        {
            this.StopListening<GainXpEvent>();
            this.StopListening<LevelUppedEvent>();
            this.StopListening<PlayerHealthChangedEvent>();
            this.StopListening<EnemyHealthChangedEvent>();
        }

        public void ShowAtPlayer(DamageNumber prefab, float number)
        {
            var player = GetPlayerTransform();
            var position = player.position + PlayerPositionOffset;
            prefab.Spawn(position, number, player);
        }

        public void ShowAtPlayer(DamageNumber prefab, string text)
        {
            var player = GetPlayerTransform();
            var position = player.position + PlayerPositionOffset;
            prefab.Spawn(position, text, player);
        }

        public void ShowAtTarget(DamageNumber prefab, GameObject target, float number)
        {
            prefab.Spawn(target.transform.position, number);
        }

        private Transform GetPlayerTransform()
        {
            return GameplayController.Current.Player.transform;
        }

        public void OnEvent(GainXpEvent evt)
        {
            ShowAtPlayer(XpPrefab, evt.Value);
        }

        public void OnEvent(LevelUppedEvent evt)
        {
            ShowAtPlayer(LevelUpPrefab, "Level Up");
        }

        public void OnEvent(PlayerHealthChangedEvent evt)
        {
            if (evt.Delta > 0)
            {
                DamagePrefab.Spawn(evt.Target.transform.position, evt.Delta, Color.green);
            }
            else
            {
                DamagePrefab.Spawn(evt.Target.transform.position, Mathf.Abs(evt.Delta), Color.red);
            }
        }

        public void OnEvent(EnemyHealthChangedEvent evt)
        {
            if (evt.Delta < 0)
            {
                DamagePrefab.Spawn(evt.Target.transform.position, Mathf.Abs(evt.Delta), Color.white);
            }
        }

    }

}