namespace Pinball
{
    public class NullInput : IInput
    {
        public bool IsSidePressed(InputSide side) => false;
        public bool IsLaunchStarted() => false;
        public bool IsLaunchEnded() => false;
    }
}
