using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    // Ends game when the ball falls into drain.
    [RequireComponent(typeof(GameState))]
    public class GameOverManager : MonoBehaviour
    {
        [Tooltip("The game is over once the ball reaches this Y coordinate")]
        public float drainPosition = -20.72f;

        private Transform _ball;
        private GameState _gameState;

        private void Awake()
        {
            _ball = GameObject.FindWithTag("Ball").transform;

            _gameState = GetComponent<GameState>();
        }

        private void Update()
        {
            _EndGameIfNeeded();
        }

        private void _EndGameIfNeeded()
        {
            bool isGameRunning = _gameState.runState == GameRunState.Running;
            bool isOutOfBorder = _ball.position.y < drainPosition;
            bool shouldEndGame = isGameRunning && isOutOfBorder;
            if (shouldEndGame)
                _gameState.EndGame();
        }
    }
}
