using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public enum GameState
    {
        Running,
        Over,
    }

    // Handles state of the game (end, restart).
    public class GameManager : MonoBehaviour
    {
        public Transform ball;

        [Tooltip("The game is over once the ball reaches this Y coordinate")]
        public float drainPosition;

        public GameState gameState { get; private set; }

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
            gameState = GameState.Running;

            EventManager.instance.OnGameStart?.Invoke();
        }

        private void _EndGameIfNeeded()
        {
            bool isGameRunning = gameState == GameState.Running;
            bool isOutOfBorder = ball.position.y < drainPosition;
            bool shouldEndGame = isGameRunning && isOutOfBorder;
            if (shouldEndGame)
                _EndGame();
        }

        private void _EndGame()
        {
            gameState = GameState.Over;

            EventManager.instance.OnGameOver?.Invoke();
        }
    }
}
