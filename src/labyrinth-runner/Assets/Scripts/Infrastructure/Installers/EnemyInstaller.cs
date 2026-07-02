using Application.Gameplay.Enemy;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class EnemyInstaller : Installer
    {
        private readonly GameObject _enemyPrefab;
        private readonly Vector3[] _enemySpawnPositions;
        private readonly Vector3[] _patrolPoints;

        public EnemyInstaller(GameObject enemyPrefab, Vector3[] enemySpawnPositions, Vector3[] patrolPoints)
        {
            _enemyPrefab = enemyPrefab;
            _enemySpawnPositions = enemySpawnPositions;
            _patrolPoints = patrolPoints;
        }

        public override void InstallBindings()
        {
            Container.Bind<Vector3[]>()
                .WithId("PatrolPoints")
                .FromInstance(_patrolPoints)
                .AsCached();

            Container.BindFactory<EnemyBehaviour, EnemyBehaviour.Factory>()
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.Bind<EnemySpawner>()
                .AsSingle()
                .WithArguments(_enemySpawnPositions);
        }
    }
}

