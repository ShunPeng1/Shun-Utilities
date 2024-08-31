using System;

namespace Shun_Utilities
{
    /// <summary>
    /// Using Strategy Pattern to choose a History class
    /// </summary>
    public interface IStateHistoryStrategy
    {
        void Save(IState transitionState, ITransitionData transitionData);
        (IState transitionState, ITransitionData transitionData) Restore(bool isRemoveRestore = true);
    }
}