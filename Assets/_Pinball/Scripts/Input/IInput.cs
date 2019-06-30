using System;

namespace Pinball
{
    [Serializable]
    public enum InputSide
    {
        Left,
        Right,
    }

    public interface IInput
    {
        bool IsSidePressed(InputSide side);
        bool IsLaunchStarted();
        bool IsLaunchEnded();
    }
}
