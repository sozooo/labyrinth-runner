using Application.Collectibles;
using Application.Enemy;
using Application.UI;
using Infrastructure.Input;
using sozooo.GameStateMachine.StateInfrastructure;
using UnityEngine;

namespace Application.States
{
    public class GameplayState : SimpleState
    {
        private readonly UISwitcher _switcher;
        private readonly DiamondSpawner _diamondSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly IPlayerInputHandler _inputHandler;

        public GameplayState(
            UISwitcher switcher,
            DiamondSpawner diamondSpawner,
            EnemySpawner enemySpawner,
            IPlayerInputHandler inputHandler)
        {
            _switcher = switcher;
            _diamondSpawner = diamondSpawner;
            _enemySpawner = enemySpawner;
            _inputHandler = inputHandler;
        }

        public override void Enter()
        {
            _switcher.ShowPanel<GameplayPanel>();
            _inputHandler.EnableControls(true);
            Cursor.lockState = CursorLockMode.Locked;
            _diamondSpawner.Spawn();
            _enemySpawner.Spawn();
        }

        protected override void Exit()
        {
            _enemySpawner.DespawnAll();
            _inputHandler.EnableControls(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
