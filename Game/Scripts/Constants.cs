namespace Game
{

    public static class Constants
    {

        public const int PixelsPerTile = 16;
        public const int TilesPerChunk = 40;
        public const int PixelsPerChunk = PixelsPerTile * TilesPerChunk;

    }

    public static class InspectorGroup
    {

        public const string References = nameof(References);
        public const string Parameters = nameof(Parameters);
        public const string Animation = nameof(Animation);
        public const string RuntimeInfo = nameof(RuntimeInfo);
        public const string Events = nameof(Events);

    }

}