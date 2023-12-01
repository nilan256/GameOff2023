namespace Game.Events
{

    public struct GainXpEvent
    {

        public int Value { get; }

        public GainXpEvent(int value)
        {
            Value = value;
        }

        public static void Trigger(int value)
        {
            GameEventManager.Send(new GainXpEvent(value));
        }

    }

}