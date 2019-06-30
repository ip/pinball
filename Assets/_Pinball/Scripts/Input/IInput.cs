using System;

namespace Pinball
{
    [Serializable]
    public enum InputSide
    {
        Left,
        Right,

        // Must be the last
        Count,
    }

    public interface IInput
    {
        bool IsSidePressed(InputSide side);
        bool IsLaunchStarted();
        bool IsLaunchEnded();
    }
}
