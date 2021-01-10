using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool secretDoors;
    public ItemManagerController itemManager;
    Animator animator;
    AudioSource doorSound;

    private void Start()
    { 
        animator = this.GetComponent<Animator>();
        doorSound = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetOpenedParam(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetOpenedParam(false);
        }
    }

    public void SetOpenedParam(bool opened)
    {
            if (animator != null)
            {
                animator.SetBool("Opened", opened); 
                doorSound.Play();
            }
    }
}
