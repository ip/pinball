using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(GameState))]
    public class GameOverManager : MonoBehaviour
    {
        public Transform ball;

        [Tooltip("The game is over once the ball reaches this Y coordinate")]
        public float drainPosition = -20.72f;

        private GameState _gameState;

        private void Awake()
        {
            Debug.Assert(ball != null);

            _gameState = GetComponent<GameState>();
        }

        private void Update()
        {
            _EndGameIfNeeded();
        }

        private void _EndGameIfNeeded()
        {
            bool isGameRunning = _gameState.runState == GameRunState.Running;
            bool isOutOfBorder = ball.position.y < drainPosition;
            bool shouldEndGame = isGameRunning && isOutOfBorder;
            if (shouldEndGame)
                _gameState.EndGame();
        }
    }
}
