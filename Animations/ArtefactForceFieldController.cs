﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Help;

public class ArtefactForceFieldController : MonoBehaviour
{
    public BlackscreenController blackscreenManager;

    private float startingScale;
    private GameObject explosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        startingScale = transform.localScale.x; // scale is the same for all axes
        explosionParticles = Help.FindChild(GameObject.Find("RoomBig"), "Explosion");

    }

    public void TriggerDestruction()
    {
        StartCoroutine(DestroyArtefact());
    }

    private IEnumerator DestroyArtefact() 
    {
        StartCoroutine(ScaleToZero()); // shrink artefact

        while (transform.localScale.x > 0)
        {
            yield return null;
        }

        explosionParticles.SetActive(true); // trigger explosion
        yield return new WaitForSeconds(1.8f); // wait
        Destroy(GameObject.Find("Explosion")); // destroy explosion

        blackscreenManager.TriggerGameOver(); // end the game

    }

    // Update is called once per frame

    private IEnumerator ScaleToZero() // shrink artefact
    {
        float counter = 0f;

        while (counter < 5f)
        {
            counter += Time.deltaTime;
            float scale = Mathf.Lerp(startingScale, 0, counter / 5f);

            transform.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }
    
    }

    //private IEnumerator MakeExplostionEffect()
    //{
    //    explosionParticles.SetActive(true);
    //    yield return new WaitForSeconds(2);
    //    print("Nicim");
    //    Destroy(GameObject.Find("Explosion"));
    //}
}
