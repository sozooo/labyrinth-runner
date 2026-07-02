using Application.Collectibles;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class DiamondInstaller : Installer
    {
        private readonly GameObject _gemPrefab;
        private readonly Vector3[] _diamondSpawnPositions;
        private readonly Door[] _doors;

        public DiamondInstaller(GameObject gemPrefab, Vector3[] diamondSpawnPositions, Door[] doors)
        {
            _gemPrefab = gemPrefab;
            _diamondSpawnPositions = diamondSpawnPositions;
            _doors = doors;
        }

        public override void InstallBindings()
        {
            Container.BindFactory<Diamond, Diamond.Factory>()
                .FromComponentInNewPrefab(_gemPrefab)
                .UnderTransformGroup("Diamonds");

            Container.Bind<DiamondSpawner>()
                .AsSingle()
                .WithArguments(_diamondSpawnPositions);

            Container.BindInterfacesAndSelfTo<DoorOpener>().AsSingle().WithArguments(_doors);
        }
    }
}
