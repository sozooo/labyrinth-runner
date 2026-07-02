using sozooo.GameStateMachine.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.UI
{
    public class StartPanel : UIPanel
    {
        [SerializeField] private Button _startButton;
        
        private IGameStateMachine _stateMachine;

        [Inject]
        private void Construct(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        private void OnEnable() =>
            _startButton.onClick.AddListener(OnPlayButtonClicked);
        
        private void OnDisable() =>
            _startButton.onClick.RemoveListener(OnPlayButtonClicked);

        private void OnPlayButtonClicked() =>
            _stateMachine.Enter<States.GameplayState>();
    }
}
