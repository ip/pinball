using UnityEngine;

namespace Pinball
{
    // Depending on the current game state, provides either player input,
    // bot or null input.
    [RequireComponent(typeof(PlayerInput))]
    public class InputProvider : MonoBehaviour, IInput
    {
        public IInput input;

        private NullInput _nullInput;

        private void Awake()
        {
            _nullInput = new NullInput();

            EventManager.instance.OnGameOver += () =>
                input = _nullInput;
        }

        public bool IsSidePressed(InputSide side) => input.IsSidePressed(side);
        public bool IsLaunchStarted() => input.IsLaunchStarted();
        public bool IsLaunchEnded() => input.IsLaunchEnded();
    }
}
