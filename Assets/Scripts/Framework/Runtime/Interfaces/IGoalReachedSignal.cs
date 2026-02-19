using System;

namespace Framework.Runtime.Interfaces
{
    public interface IGoalReachedSignal
    {
        event Action GoalReached;
    }
}