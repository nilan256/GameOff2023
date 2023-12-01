namespace Game.Events
{

    public struct ClearProjectilesEvent
    {

        public static void Send()
        {
            GameEventManager.Send(new InstantKillAllEvent());
        }
        
    }

}