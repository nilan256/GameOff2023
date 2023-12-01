using Game.CharacterControl;

namespace Game.Events
{

    public struct PlayerHealthChangedEvent
    {

        public Player Target { get; }
        public float Delta { get; }

        public PlayerHealthChangedEvent(Player target, float delta)
        {
            Target = target;
            Delta = delta;
        }

        public static void Send(Player target, float delta)
        {
            GameEventManager.Send(new PlayerHealthChangedEvent(target, delta));
        }

    }

}