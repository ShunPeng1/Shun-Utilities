namespace Shun_Utilities
{
    public class TransitionData : ITransitionData
    {
        public IState FromState { get; set; }
        public ITransition Transition { get; set; }
    }
}