using System;
using Infrastructure.Input;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind(typeof(IPlayerInputHandler), typeof(IInitializable), typeof(IDisposable))
                .To<PlayerInputHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}