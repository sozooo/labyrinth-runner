using System.Linq;
using Configs;
using UnityEngine;

namespace Application.Enemy
{
    public class EnemySpawner
    {
        private readonly EnemyBehaviour.Factory _enemyFactory;
        private readonly Vector3[] _spawnPoints;
        private readonly GameplayConfig _config;
        private readonly System.Collections.Generic.List<EnemyBehaviour> _spawnedEnemies = new();

        public EnemySpawner(Vector3[] spawnPoints, EnemyBehaviour.Factory enemyFactory, GameplayConfig config)
        {
            _spawnPoints = spawnPoints;
            _enemyFactory = enemyFactory;
            _config = config;
        }

        public void Spawn()
        {
            int count = Random.Range(_config.EnemyCountRange.x,
                Mathf.Min(_config.EnemyCountRange.y, _spawnPoints.Length));

            _spawnPoints.OrderBy(_ => Random.value)
                .Take(count)
                .ToList()
                .ForEach(point =>
                {
                    var enemy = _enemyFactory.Create();
                    enemy.transform.position = point;
                    _spawnedEnemies.Add(enemy);
                });
        }

        public void DespawnAll()
        {
            foreach (var enemy in _spawnedEnemies)
            {
                if (enemy == null)
                    continue;

                enemy.enabled = false;
                if (enemy.NavAgent != null)
                    enemy.NavAgent.isStopped = true;
            }

            _spawnedEnemies.Clear();
        }
    }
}
