using Application.Core.Services;
using Infrastructure.Input;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle().NonLazy();
        }
    }
}

