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
        private bool[] _isSideDown = new bool[(int)InputSide.Count];
        // Was screen pressed in last frame?
        private bool[] _sideWasPressed = new bool[(int)InputSide.Count];

        private void Awake()
        {
            _nullInput = new NullInput();

            EventManager.instance.OnGameOver += () =>
                input = _nullInput;
        }

        private void Update()
        {
            _UpdateIsSideDown(InputSide.Left);
            _UpdateIsSideDown(InputSide.Right);
        }

        // IInput
        public bool IsSidePressed(InputSide side) => input.IsSidePressed(side);
        public bool IsLaunchStarted() => input.IsLaunchStarted();
        public bool IsLaunchEnded() => input.IsLaunchEnded();

        public bool IsSideDown(InputSide side) => _isSideDown[(int)side];

        private void _UpdateIsSideDown(InputSide side)
        {
            int s = (int)side;

            if (IsSidePressed(side))
            {
                if (!_isSideDown[s] && !_sideWasPressed[s])
                {
                    _isSideDown[s] = true;
                    _sideWasPressed[s] = true;
                }
                else
                    _isSideDown[s] = false;
            }
            else
            {
                _isSideDown[s] = false;
                _sideWasPressed[s] = false;
            }
        }
    }
}
