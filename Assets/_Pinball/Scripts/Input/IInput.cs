namespace Pinball
{
    public interface IInput
    {
        bool IsSidePressed(InputSide side);
        bool IsLaunchStarted();
        bool IsLaunchEnded();
    }
}
