using Application.Collectibles;
using Application.States;
using Application.UI;
using Infrastructure.Input;
using MessagePipe;
using sozooo.GameStateMachine.Factory;
using sozooo.GameStateMachine.StateMachine;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gemPrefab;
        [SerializeField] private Transform _diamondSpawnPointsParent;
        [SerializeField] private Door[] _doors;
        
        [SerializeField] private StartPanel _startPanel;
        [SerializeField] private GameplayPanel _gameplayPanel;
        [SerializeField] private WinPanel _winPanel;
        [SerializeField] private LosePanel _losePanel;

        public override void InstallBindings()
        {
            InstallPlayerInputBindings();
            InstallStateMachineBindings();
            InstallUIBindings();

            var options = Container.BindMessagePipe();
            InstallDiamondBindings(options);

            Container.BindInterfacesAndSelfTo<DoorOpener>().AsSingle().WithArguments(_doors);
        }

        private void InstallPlayerInputBindings() =>
            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle().NonLazy();

        private void InstallStateMachineBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }

        private void InstallUIBindings()
        {
            Container.Bind<UISwitcher>().AsSingle();
            Container.Bind<StartPanel>().FromInstance(_startPanel).AsSingle();
            Container.Bind<GameplayPanel>().FromInstance(_gameplayPanel).AsSingle();
            Container.Bind<WinPanel>().FromInstance(_winPanel).AsSingle();
            Container.Bind<LosePanel>().FromInstance(_losePanel).AsSingle();
        }

        private void InstallDiamondBindings(MessagePipeOptions options)
        {
            Container.BindMessageBroker<DiamondCollectedMessage>(options);
            Container.BindMessageBroker<DiamondsSpawnedMessage>(options);

            Container.BindFactory<Diamond, Diamond.Factory>()
                .FromComponentInNewPrefab(_gemPrefab)
                .UnderTransformGroup("Diamonds");

            Container.Bind<DiamondSpawner>()
                .AsSingle()
                .WithArguments(ExtractChildPositions(_diamondSpawnPointsParent));
        }

        private Vector3[] ExtractChildPositions(Transform parent)
        {
            Vector3[] positions = new Vector3[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
                positions[i] = parent.GetChild(i).position;

            return positions;
        }
    }
}