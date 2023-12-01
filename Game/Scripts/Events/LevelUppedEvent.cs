namespace Game.Events
{

    public struct LevelUppedEvent
    {

        public static void Send()
        {
            GameEventManager.Send(new LevelUppedEvent());
        }

    }

}