using UnityEngine;

namespace Pinball
{
    // Depending on the current game state, provides either player input,
    // bot or null input.
    [RequireComponent(typeof(PlayerInput))]
    public class InputProvider : MonoBehaviour, IInput
    {
        public IInput input;
        public bool isScreenDown { get; private set; }

        private NullInput _nullInput;

        // Was screen pressed in last frame?
        private bool _screenWasPressed;

        private void Awake()
        {
            _nullInput = new NullInput();

            EventManager.instance.OnGameOver += () =>
                input = _nullInput;
        }

        private void Update()
        {
            _UpdateIsScreenDown();
        }

        // IInput
        public bool IsSidePressed(InputSide side) => input.IsSidePressed(side);
        public bool IsLaunchStarted() => input.IsLaunchStarted();
        public bool IsLaunchEnded() => input.IsLaunchEnded();

        private void _UpdateIsScreenDown()
        {
            if (IsSidePressed(InputSide.Left) || IsSidePressed(InputSide.Right))
            {
                if (!isScreenDown && !_screenWasPressed)
                {
                    isScreenDown = true;
                    _screenWasPressed = true;
                }
                else
                    isScreenDown = false;
            }
            else
            {
                isScreenDown = false;
                _screenWasPressed = false;
            }
        }
    }
}
