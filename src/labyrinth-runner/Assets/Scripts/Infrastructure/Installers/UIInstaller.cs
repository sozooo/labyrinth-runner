using Application.Services;
using Application.UI;
using Infrastructure.Services;
using Zenject;

namespace Infrastructure.Installers
{
    public class UIInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<UISwitcher>().AsSingle();
        }
    }
}
