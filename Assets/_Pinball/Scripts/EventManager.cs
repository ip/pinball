using System;

namespace Pinball
{
    public class EventManager
    {
        public static EventManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventManager();

                return _instance;
            }
        }

        private static EventManager _instance;

        // Argument: isBotMode
        public Action<bool> OnGameStart;
        public Action OnGameOver;
    }
}
