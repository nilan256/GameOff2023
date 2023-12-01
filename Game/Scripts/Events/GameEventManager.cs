using System;
using System.Collections.Generic;

namespace Game.Events
{

    public static class GameEventManager
    {

        private static readonly Dictionary<Type, List<object>> global = new Dictionary<Type, List<object>>();

        public static void AddListener(Type type, object listener)
        {
            if (!global.TryGetValue(type, out var list))
            {
                list = new List<object>();
                global[type] = list;
            }

            list.Add(listener);
        }

        public static void AddListener<T>(IGameEventListener<T> listener)
        {
            AddListener(typeof(T), listener);
        }

        public static void RemoveListener(Type type, object listener)
        {
            if (!global.TryGetValue(type, out var list)) return;
            list.RemoveAll(item => item == listener);
        }

        public static void RemoveListener<T>(IGameEventListener<T> listener)
        {
            RemoveListener(typeof(T), listener);
        }

        public static void RemoveListeners<T>()
        {
            global.Remove(typeof(T));
        }

        public static void Clear()
        {
            global.Clear();
        }

        public static void Send<T>(T evt)
        {
            var type = typeof(T);
            if (!GameEventManager.global.TryGetValue(type, out var listeners)) return;
            
            // for avoiding modify listener list in OnEvent, it will trigger an exception
            Action<T> onEvent = delegate { };
            foreach (IGameEventListener<T> listener in listeners)
            {
                onEvent += listener.OnEvent;
            }

            onEvent.Invoke(evt);
        }

    }
    
    public interface IGameEventListener<in TEvent>
    {

        void OnEvent(TEvent evt);

    }

    public static class GameEventExtensions
    {

        public static void StartListening<T>(this IGameEventListener<T> listener)
        {
            GameEventManager.AddListener(listener);
        }

        public static void StopListening<T>(this IGameEventListener<T> listener)
        {
            GameEventManager.RemoveListener(listener);
        }

    }

}