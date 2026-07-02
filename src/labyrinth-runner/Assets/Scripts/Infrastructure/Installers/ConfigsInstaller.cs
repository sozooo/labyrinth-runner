using Configs;
using Zenject;

namespace Infrastructure.Installers
{
    public class ConfigsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromResources("Configs/EnemyConfig").AsSingle();
            Container.Bind<PlayerConfig>().FromResources("Configs/PlayerConfig").AsSingle();
            Container.Bind<GameplayConfig>().FromResources("Configs/GameplayConfig").AsSingle();
        }
    }
}
