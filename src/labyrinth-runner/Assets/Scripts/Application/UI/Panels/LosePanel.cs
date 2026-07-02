using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Application.UI
{
    public class LosePanel : UIPanel
    {
        [SerializeField] private Button _mainMenuButton;

        private void OnEnable() => 
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);

        private void OnDisable() => 
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);

        private void OnMainMenuButtonClicked() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
