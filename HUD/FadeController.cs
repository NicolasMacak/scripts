using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    // Start is called before the first frame update

    public float Duration = 1;
    void Start()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup));
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup)
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / Duration);

            yield return null;
        }
    }
}
