using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public enum GameRunState
    {
        Running,
        Over,
    }

    // Contains game state and notifies about its changes (no logic there).
    public class GameState : MonoBehaviour
    {
        public GameRunState runState { get; private set; }
        public Observable<int> score;

        private void Awake()
        {
            score = new Observable<int>();
        }

        private void Start()
        {
            RestartGame();
        }

        public void RestartGame()
        {
            runState = GameRunState.Running;

            EventManager.instance.OnGameStart?.Invoke();
        }

        public void EndGame()
        {
            runState = GameRunState.Over;

            EventManager.instance.OnGameOver?.Invoke();
        }
    }
}
