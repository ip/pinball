using UnityEngine;
using TMPro;

namespace Pinball
{
    // Controller from MVC.
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreController : MonoBehaviour
    {
        public GameState gameState;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            Debug.Assert(gameState != null);

            gameState.score.OnChange += _UpdateView;
        }

        private void _UpdateView(int score)
        {
            _text.text = "Score: " + score;
        }
    }
}
