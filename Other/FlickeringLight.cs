﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    //private bool isFlickering = false;
    public float randomDelayFrom = 1;
    public float randomDelayTo = 2;
    public bool isFlickering = true;

    private Light light;
    private float delayInterval;


    private void Start()
    {
        if (isFlickering)
        {
            light = this.GetComponent<Light>();
            StartCoroutine(FlickerLight());
        }
        
    }

     private IEnumerator FlickerLight()
    {
        //this.gameObject.GetComponent<Light>().enabled = false;
        light.enabled = false;
        delayInterval = Random.Range(randomDelayFrom, randomDelayTo);
        yield return new WaitForSeconds(delayInterval);

        //this.gameObject.GetComponent<Light>().enabled = true;
        light.enabled = true;
        delayInterval = Random.Range(randomDelayFrom, randomDelayTo);
        yield return new WaitForSeconds(delayInterval);

        StartCoroutine(FlickerLight());
    }
}
