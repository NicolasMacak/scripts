using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackscreenController : MonoBehaviour
{

    public ScenaManager sceneManager;

    private CanvasGroup canvasGroup;
    

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private IEnumerator MakeBlackScreenAndFadeBack()
    {      
        MakeBackScreen();
        yield return new WaitForSeconds(1);
        StartCoroutine(TransitionToTransparent());
    }

    public void TriggerGameOver()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(TransitionToBlack());

        while (canvasGroup.alpha < 1)
        {
            yield return null;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sceneManager.LoadNextScene();
    }

    public void TriggerBlackScreen()
    {
        StartCoroutine(MakeBlackScreenAndFadeBack());
    }

    private void MakeBackScreen()
    {
        canvasGroup.alpha = 1;
    }

    private IEnumerator TransitionToTransparent()
    {
        float counter = 0f;

        while (counter < 2f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, counter / 2f);

            yield return null;
        }
        
    }

    private IEnumerator TransitionToBlack()
    {
        float counter = 0f;

        while (counter < 4f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / 4f);

            yield return null;
        }

    }
}
