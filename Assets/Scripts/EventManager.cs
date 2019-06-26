using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public Action OnGameStart;
        public Action OnGameOver;
    }
}
