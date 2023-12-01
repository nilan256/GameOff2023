namespace Game
{

    public class GameSession
    {

        #region Statics
        
        public static GameSession Current
        {
            get
            {
                if (userSession != null) return userSession;
                return mock;
            }
        }
        private static GameSession userSession;
        /// <summary>
        /// for start game scene directly in editor
        /// </summary>
        private static readonly GameSession mock = new GameSession()
        {
            SelectedLevelId = "grassland",
            SelectedPlayerCharacterId = "duck",
        };

        public static void Start(GameSession session)
        {
            userSession = session;
        }

        public static void Stop()
        {
            userSession = null;
        }

        #endregion

        #region Fields And Properties

        public string SelectedLevelId { get; set; }
        public string SelectedPlayerCharacterId { get; set; }

        #endregion

    }

}