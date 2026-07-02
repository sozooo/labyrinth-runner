using Application.GameFlow.States;
using sozooo.GameStateMachine.Factory;
using sozooo.GameStateMachine.StateMachine;
using Zenject;

namespace Infrastructure.Installers
{
    public class StateMachineInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}

