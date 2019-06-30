using System;

namespace Pinball
{
    // Each object which adds score needs to implement this interface
    // in its MonoBehaviour.
    public interface IScoreAdder
    {
        Action<int> OnScoreAdded { get; set; }
    }
}
