using Application.Core.Configs;
using UnityEngine;

namespace Application.Gameplay.Enemy
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

            var indices = new int[_spawnPoints.Length];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            for (int i = indices.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            for (int i = 0; i < count; i++)
            {
                var enemy = _enemyFactory.Create();
                enemy.transform.position = _spawnPoints[indices[i]];
                _spawnedEnemies.Add(enemy);
            }
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

