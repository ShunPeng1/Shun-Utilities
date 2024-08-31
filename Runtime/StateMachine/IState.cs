using System;

namespace Shun_Utilities
{
    public interface IState
    {
        void OnEnterState(ITransitionData enterTransitionData = null);
        void OnExitState(ITransitionData exitTransitionData = null);
        void UpdateState();
        void FixedUpdateState();
    }
}