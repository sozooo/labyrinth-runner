using Application.Collectibles;
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
        private readonly IPlayerInputHandler _inputHandler;

        public GameplayState(
            UISwitcher switcher,
            DiamondSpawner diamondSpawner,
            IPlayerInputHandler inputHandler)
        {
            _switcher = switcher;
            _diamondSpawner = diamondSpawner;
            _inputHandler = inputHandler;
        }

        public override void Enter()
        {
            _switcher.ShowPanel<GameplayPanel>();
            _inputHandler.EnableControls(true);
            Cursor.lockState = CursorLockMode.Locked;
            _diamondSpawner.Spawn();
        }

        protected override void Exit()
        {
            _inputHandler.EnableControls(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
