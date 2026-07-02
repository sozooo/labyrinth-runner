using System.Collections.Generic;
using UnityEngine;

namespace Application.Enemy
{
    public class EnemySpawner
    {
        private readonly EnemyBehaviour.Factory _enemyFactory;
        private readonly Vector3[] _spawnPoints;
        private readonly List<EnemyBehaviour> _spawnedEnemies = new();

        public EnemySpawner(Vector3[] spawnPoints, EnemyBehaviour.Factory enemyFactory)
        {
            _spawnPoints = spawnPoints;
            _enemyFactory = enemyFactory;
        }

        public void Spawn()
        {
            foreach (Vector3 point in _spawnPoints)
            {
                var enemy = _enemyFactory.Create();
                enemy.transform.position = point;
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
