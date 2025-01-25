using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeadPilotTools.SceneLoader.runtime
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Loader _loader;

        private float _loadProgress;

        #region Singleton
        private static SceneLoader instance;
        public static SceneLoader Instance() => instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        #endregion

        public void LoadScene(string sceneName) => StartCoroutine(LoadSceneCoroutine(sceneName));

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);

            ShowLoader();

            _loadProgress = 0;
            asyncScene.allowSceneActivation = false;

            do
            {
                yield return new WaitForSeconds(1);
                _loadProgress = asyncScene.progress;
            } while (asyncScene.progress < 0.9f);

            yield return new WaitForSeconds(1);
            asyncScene.allowSceneActivation = true;

            yield return new WaitUntil(() => asyncScene.isDone);
            yield return new WaitUntil(() => !_loader.IsFading());

            HideLoader();
        }

        public void HideLoader() => _loader.HideLoader();

        public void ShowLoader() => _loader.gameObject.SetActive(true);
    }

}
