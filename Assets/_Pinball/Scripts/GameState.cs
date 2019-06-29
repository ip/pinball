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

    public class GameState : MonoBehaviour
    {
        public GameRunState runState { get; private set; }

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
