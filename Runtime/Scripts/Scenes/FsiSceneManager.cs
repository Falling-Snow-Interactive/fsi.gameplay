using System;
using UnityEngine.SceneManagement;

namespace Fsi.Gameplay.Scenes
{
    public class FsiSceneManager : MbSingleton<FsiSceneManager>
    {
        public void LoadScene(string sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadScene(sceneName, mode);
        }

        public void LoadSceneAsync(string sceneName, LoadSceneMode mode, Action onComplete = null)
        {
            var sceneAsync = SceneManager.LoadSceneAsync(sceneName, mode);
            if (sceneAsync != null)
            {
                sceneAsync.completed += _ => onComplete?.Invoke();
            }
        }
    }
}
