using Application.Collectibles;
using Infrastructure.Input;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gemPrefab;
        [SerializeField] private Transform _diamondSpawnPointsParent;
        [SerializeField] private Door[] _doors;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle().NonLazy();

            var options = Container.BindMessagePipe();
            Container.BindMessageBroker<DiamondCollectedMessage>(options);
            Container.BindMessageBroker<DiamondsSpawnedMessage>(options);

            Container.BindFactory<Diamond, Diamond.Factory>()
                .FromComponentInNewPrefab(_gemPrefab)
                .UnderTransformGroup("Diamonds");
            
            Container.BindInterfacesAndSelfTo<DiamondSpawner>()
                .AsSingle()
                .WithArguments(ExtractChildPositions(_diamondSpawnPointsParent));

            Container.BindInterfacesAndSelfTo<DoorOpener>().AsSingle().WithArguments(_doors);
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