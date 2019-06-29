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
        public Transform ball;

        [Tooltip("The game is over once the ball reaches this Y coordinate")]
        public float drainPosition;

        public GameRunState runState { get; private set; }

        private void Awake()
        {
            Debug.Assert(ball != null);
        }

        private void Update()
        {
            _EndGameIfNeeded();
        }

        public void RestartGame()
        {
            runState = GameRunState.Running;

            EventManager.instance.OnGameStart?.Invoke();
        }

        private void _EndGameIfNeeded()
        {
            bool isGameRunning = runState == GameRunState.Running;
            bool isOutOfBorder = ball.position.y < drainPosition;
            bool shouldEndGame = isGameRunning && isOutOfBorder;
            if (shouldEndGame)
                _EndGame();
        }

        private void _EndGame()
        {
            runState = GameRunState.Over;

            EventManager.instance.OnGameOver?.Invoke();
        }
    }
}
