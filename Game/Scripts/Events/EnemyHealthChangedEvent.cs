namespace Game.Events
{

    public struct EnemyHealthChangedEvent
    {

        public Enemy Target { get; }
        public float Delta { get; }

        public EnemyHealthChangedEvent(Enemy target, float delta)
        {
            Target = target;
            Delta = delta;
        }

        public static void Send(Enemy target, float delta)
        {
            GameEventManager.Send(new EnemyHealthChangedEvent(target, delta));
        }

    }

}