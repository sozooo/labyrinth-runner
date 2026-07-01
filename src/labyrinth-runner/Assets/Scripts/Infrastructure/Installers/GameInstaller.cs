using Infrastructure.Input;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputHandler>().FromNew().AsSingle().NonLazy();
        }
    }
}