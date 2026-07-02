using Application.Gameplay.Collectibles;
using Application.GameFlow.UI;
using Application.GameFlow.UI.Panels;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _gemPrefab;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _diamondSpawnPointsParent;
        [SerializeField] private Transform _enemySpawnPointsParent;
        [SerializeField] private Door[] _doors;
        
        [SerializeField] private StartPanel _startPanel;
        [SerializeField] private GameplayPanel _gameplayPanel;
        [SerializeField] private WinPanel _winPanel;
        [SerializeField] private LosePanel _losePanel;

        public override void InstallBindings()
        {
            Container.Bind<List<UIPanel>>().FromInstance(new List<UIPanel>
            {
                _startPanel, _gameplayPanel, _winPanel, _losePanel
            });

            Container.Install<ConfigsInstaller>();
            Container.Install<InputInstaller>();
            Container.Install<StateMachineInstaller>();
            Container.Install<MessagesInstaller>();
            Container.Install<UIInstaller>();
            Container.Install<PlayerInstaller>();
            
            Container.Install<EnemyInstaller>(new object[]
            {
                _enemyPrefab,
                ExtractChildPositions(_enemySpawnPointsParent),
                ExtractChildPositions(_diamondSpawnPointsParent)
            });
            
            Container.Install<DiamondInstaller>(new object[]
            {
                _gemPrefab,
                ExtractChildPositions(_diamondSpawnPointsParent),
                _doors
            });
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

