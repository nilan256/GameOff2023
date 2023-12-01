namespace Game.Events
{

    public struct InstantKillAllEvent
    {

        public static void Send()
        {
            GameEventManager.Send(new InstantKillAllEvent());
        }

    }

}