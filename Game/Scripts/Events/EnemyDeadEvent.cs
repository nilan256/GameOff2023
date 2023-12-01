namespace Game.Events
{

    public struct EnemyDeadEvent
    {

        public Enemy Enemy { get; }

        public EnemyDeadEvent(Enemy enemy)
        {
            Enemy = enemy;
        }

        public static void Send(Enemy enemy)
        {
            GameEventManager.Send(new EnemyDeadEvent(enemy));
        }

    }

}