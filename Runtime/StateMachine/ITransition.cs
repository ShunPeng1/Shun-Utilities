namespace Shun_Utilities
{
    public interface ITransition
    {
        public IState ToState { get; }
        public IPredicate Condition { get; }
        
    }

}