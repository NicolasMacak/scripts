using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenController : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update

    public void openClose()
    {
        if (door != null)
        {
            Animator animator = door.GetComponent<Animator>();

            if (animator != null)
            {
                bool opened = animator.GetBool("Opened");
                animator.SetBool("Opened", !opened);
            }
        }
    }
}
