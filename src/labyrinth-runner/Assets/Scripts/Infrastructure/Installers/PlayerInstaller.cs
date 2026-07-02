using Application.Player;
using Zenject;

namespace Infrastructure.Installers
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerBehaviour>().FromComponentInHierarchy().AsCached();
            Container.Bind<IPlayer>().To<PlayerBehaviour>().FromComponentInHierarchy().AsCached();
        }
    }
}
