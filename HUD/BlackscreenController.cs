using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackscreenController : MonoBehaviour
{

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
}
