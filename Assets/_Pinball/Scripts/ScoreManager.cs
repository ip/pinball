using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pinball
{
    [RequireComponent(typeof(GameState))]
    public class ScoreManager : MonoBehaviour
    {
        private GameState _gameState;

        private void Awake()
        {
            _gameState = GetComponent<GameState>();

            _SubscribeToScoreAdders();
            _ResetScoreOnRestart();
        }

        private void _SubscribeToScoreAdders()
        {
            IEnumerable<IScoreAdder> scoreAdders =
                FindObjectsOfType<MonoBehaviour>()
                .OfType<IScoreAdder>();

            foreach (var adder in scoreAdders)
                adder.OnScoreAdded += _AddScore;
        }

        private void _AddScore(int score)
        {
            _gameState.score.value += score;
        }

        private void _ResetScoreOnRestart()
        {
            EventManager.instance.OnGameStart += _ =>
                _gameState.score.value = 0;
        }
    }
}
