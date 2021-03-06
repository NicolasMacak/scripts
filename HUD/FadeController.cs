﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isShown = false;
    private CanvasGroup canvasGroup;

    public float Duration = 1;
    public CanvasGroup externalCanvasGroup;
    void Start()
    {
        canvasGroup = externalCanvasGroup == null ? GetComponent<CanvasGroup>() : externalCanvasGroup; // if external canvasGroup is not present, use canvasGroup on this object
        
        if(externalCanvasGroup == null)
        StartCoroutine(DoFade());
    }

    public IEnumerator DoFade()
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / Duration);

            yield return null;
        }
    }

    private IEnumerator Unfade()
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, counter / Duration);

            yield return null;
        }
    }

    public void HideAndShow() 
    {
        if (isShown) { 
             StartCoroutine(Unfade());
            isShown = false;
        } else
        {
            StartCoroutine(DoFade());
            isShown = true;
        }

    }
}
