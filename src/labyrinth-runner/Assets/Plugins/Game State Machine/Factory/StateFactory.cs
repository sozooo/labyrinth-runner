using sozooo.GameStateMachine.StateInfrastructure;
using Zenject;

namespace sozooo.GameStateMachine.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _resolver;

        public StateFactory(IInstantiator resolver) => 
            _resolver = resolver;

        public T GetState<T>() where T : class, IExitableState
        {
            return _resolver.Instantiate<T>();
        }
    }
}