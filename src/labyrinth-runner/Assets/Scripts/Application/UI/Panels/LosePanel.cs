using Application.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.UI
{
    public class LosePanel : UIPanel
    {
        [SerializeField] private Button _mainMenuButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        private void OnEnable() => 
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        private void OnDisable() => 
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        private void OnMainMenuButtonClicked() =>
            _sceneLoader.ReloadCurrentScene();
    }
}
