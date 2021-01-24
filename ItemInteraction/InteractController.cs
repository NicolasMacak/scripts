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
            if (itemInRange != null && itemInRange.state == ItemState.ENABLED) 
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

    private void ZaHando() // ruka. Evaluates wheter player is touching interactable object or not
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
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
        

        if (itemInRange != null)
        {
            messagePanel.displayMessage(itemInRange);
        }
    }

    private void notTouchingAnyObject()
    {
        itemInRange = null;
        messagePanel.hideMessage();
    }
}
