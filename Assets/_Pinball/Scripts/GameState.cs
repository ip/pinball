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
            EndGame();
        }

        public void RestartGame(bool isBotMode)
        {
            runState = GameRunState.Running;

            EventManager.instance.OnGameStart?.Invoke(isBotMode);
        }

        public void EndGame()
        {
            runState = GameRunState.Over;

            EventManager.instance.OnGameOver?.Invoke();
        }
    }
}
