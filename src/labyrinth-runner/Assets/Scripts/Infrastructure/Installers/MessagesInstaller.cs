using Application.Gameplay.Collectibles;
using Application.Core.Messages;
using MessagePipe;
using Zenject;

namespace Infrastructure.Installers
{
    public class MessagesInstaller : Installer
    {
        public override void InstallBindings()
        {
            var options = Container.BindMessagePipe();
            Container.BindMessageBroker<DiamondCollectedMessage>(options);
            Container.BindMessageBroker<DiamondsSpawnedMessage>(options);
            Container.BindMessageBroker<ExitReachedMessage>(options);
            Container.BindMessageBroker<PlayerDiedMessage>(options);
        }
    }
}

