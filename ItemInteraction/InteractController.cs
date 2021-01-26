using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;
using static SoundController;

public class InteractController : MonoBehaviour
{
    // Start is called before the first frame update

    public ItemManagerController itemManager;

    public MessagePanelController messagePanel;
    public NoteController noteManager;

    private InteractItem itemInRange;

    private bool isPlayerReading;
    void Start()
    {
        isPlayerReading = false;
    }

    // Update is called once per frame
    void Update()
    {
        ZaHando();

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (itemInRange != null && itemInRange.state == ItemState.ENABLED) // is item enabled
            {
                if(itemInRange.category == ItemCategory.READABLE)
                {
                    isPlayerReading = isPlayerReading ? noteManager.hideNote() : noteManager.displayNote(itemInRange.objectName); // switch between reading state
                }
                else
                {
                    itemManager.interactWithItem(itemInRange);
                }

            }            
        }
    }

    private void ZaHando() // The Hand. Evaluates wheter player is touching interactable object or not
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position + transform.forward*0.5f, transform.forward, Color.red);
        if (Physics.Raycast(transform.position + transform.forward * 0.5f, transform.forward, out hit, 3f))
        {
            touchingObject(hit.collider.gameObject);
        } else
        {
            notTouchingAnyObject();
        }
    }

    private void touchingObject(GameObject objectInRange)
    {
        itemInRange = itemManager.getItem(objectInRange.name);
        
        if (itemInRange != null) // item can be interacted with
        {
            messagePanel.displayMessage(itemInRange);
        } else
        {
            messagePanel.hideMessage();
        }
    }

    private void notTouchingAnyObject()
    {
        itemInRange = null;
        messagePanel.hideMessage();
    }
}
