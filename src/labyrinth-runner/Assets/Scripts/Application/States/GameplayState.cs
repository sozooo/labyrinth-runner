using System;
using Application.Collectibles;
using Application.Enemy;
using Application.Messages;
using Application.UI;
using Infrastructure.Input;
using MessagePipe;
using sozooo.GameStateMachine.StateInfrastructure;
using sozooo.GameStateMachine.StateMachine;
using UnityEngine;

namespace Application.States
{
    public class GameplayState : SimpleState
    {
        private readonly UISwitcher _switcher;
        private readonly DiamondSpawner _diamondSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly IPlayerInputHandler _inputHandler;
        private readonly ISubscriber<ExitReachedMessage> _exitSubscriber;
        private readonly ISubscriber<PlayerDiedMessage> _diedSubscriber;
        private readonly IGameStateMachine _stateMachine;

        private IDisposable _exitSub;
        private IDisposable _diedSub;

        public GameplayState(
            UISwitcher switcher,
            DiamondSpawner diamondSpawner,
            EnemySpawner enemySpawner,
            IPlayerInputHandler inputHandler,
            ISubscriber<ExitReachedMessage> exitSubscriber,
            ISubscriber<PlayerDiedMessage> diedSubscriber,
            IGameStateMachine stateMachine)
        {
            _switcher = switcher;
            _diamondSpawner = diamondSpawner;
            _enemySpawner = enemySpawner;
            _inputHandler = inputHandler;
            _exitSubscriber = exitSubscriber;
            _diedSubscriber = diedSubscriber;
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            _switcher.ShowPanel<GameplayPanel>();
            _inputHandler.EnableControls(true);
            Cursor.lockState = CursorLockMode.Locked;
            _diamondSpawner.Spawn();
            _enemySpawner.Spawn();

            _exitSub = _exitSubscriber.Subscribe(_ => _stateMachine.Enter<WinState>());
            _diedSub = _diedSubscriber.Subscribe(_ => _stateMachine.Enter<LoseState>());
        }

        protected override void Exit()
        {
            _exitSub?.Dispose();
            _diedSub?.Dispose();
            _enemySpawner.DespawnAll();
            _inputHandler.EnableControls(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
