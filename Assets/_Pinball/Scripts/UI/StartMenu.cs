using UnityEngine;

namespace Pinball
{
    public class StartMenu : MonoBehaviour
    {
        private GameObject _content;

        private void Awake()
        {
            _content = transform.GetChild(0).gameObject;

            EventManager.instance.OnGameOver += () => _SetVisibility(true);
            EventManager.instance.OnGameStart += _ => _SetVisibility(false);
        }

        private void _SetVisibility(bool visible)
        {
            _content.SetActive(visible);
        }
    }
}
