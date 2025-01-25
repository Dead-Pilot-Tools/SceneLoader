using System.Collections;
using UnityEngine;

namespace DeadPilotTools.SceneLoader.runtime
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float fadeTime = 2;

        private bool isFading = false;


        private void OnEnable()
        {
            _canvasGroup.alpha = 0f;

            StartCoroutine(FadeLoaderCoroutine(true));
        }

        public bool IsFading() => isFading;

        public void HideLoader()
        {
            StopAllCoroutines();
            StartCoroutine(FadeLoaderCoroutine(false));
        }

        private IEnumerator FadeLoaderCoroutine(bool fadeIn)
        {
            isFading = true;

            float fadeTarget = fadeIn ? 1.0f : 0.0f;
            float tempAlpha = _canvasGroup.alpha;

            while (Mathf.Abs(fadeTarget - _canvasGroup.alpha) > .001f)
            {
                tempAlpha = Mathf.Lerp(_canvasGroup.alpha, fadeTarget, fadeTime * Time.deltaTime);

                _canvasGroup.alpha = tempAlpha;

                yield return null;
            }

            _canvasGroup.alpha = fadeTarget;

            isFading = false;

            if (!fadeIn)
            {
                gameObject.SetActive(false);

            }

            yield return null;
        }


        //private IEnumerator FadeCoroutine(bool fadeIn)
        //{
        //    isFading = true;

        //    float fadeTarget = fadeIn ? 1.0f : 0.0f;
        //    Color tempColor = loaderMaterial.color;

        //    while (Mathf.Abs(fadeTarget - loaderMaterial.color.a) > .01f)
        //    {
        //        tempColor.a = Mathf.Lerp(loaderMaterial.color.a, fadeTarget, fadeTime * Time.deltaTime);

        //        loaderMaterial.color = tempColor;

        //        yield return null;
        //    }

        //    tempColor.a = fadeTarget;
        //    loaderMaterial.color = tempColor;

        //    isFading = false;

        //    if (!fadeIn)
        //        gameObject.SetActive(false);

        //    yield return null;
        //}

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
