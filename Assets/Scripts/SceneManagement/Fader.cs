using System.Collections;
using UnityEngine;

 namespace RPG.SceneManagement
{ 
    public class Fader : MonoBehaviour
    {

     

        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOut(3f));
            //StartCoroutine(FadeIn(3f
        }
        

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) // Alpha is not 1
            {
                //Moving Alpha Toward 1
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        { 
            while (canvasGroup.alpha > 0) // Alpha is not 0
            {
                //Moving Alpha Toward 0
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

        }
    }
}
