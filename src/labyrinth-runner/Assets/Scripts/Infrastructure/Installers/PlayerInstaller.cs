using Application.Gameplay.Player;
using Zenject;

namespace Infrastructure.Installers
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayer>().To<PlayerBehaviour>().FromComponentInHierarchy().AsCached();
        }
    }
}

