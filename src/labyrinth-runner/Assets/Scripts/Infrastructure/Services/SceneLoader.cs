using Application.Core.Services;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        public void ReloadCurrentScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

