using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;

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
        makeInteractableHand();

        if (Input.GetKeyUp(KeyCode.E))
        {  
            if (itemInRange != null && itemInRange.state == ItemState.ENABLED) // remove Locked
            {
                if(itemInRange.category == ItemCategory.READABLE)
                {
                    isPlayerReading = isPlayerReading ? noteManager.hideNote() : noteManager.displayNote(itemInRange.objectName); // switch between reading state
                }
                else
                {
                    itemManager.interactWithItem(itemInRange);
                }

                
                //itemManager.addToInventory(itemInRange.objectName);
            }
            //else if (itemInRange.state == InteractItem.ItemState.USABLE)
            //{
            //    //itemManager.noteManager.hideNote();
            //    // itemManager.useItem 
            //}
            
        }
    }

    private void makeInteractableHand()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
        {
            hittingObject(hit.collider.gameObject);
        } else
        {
            notHittingObject();
        }
    }

    private void hittingObject(GameObject objectInRange)
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        itemInRange = itemManager.getItem(objectInRange.name);
        //print(itemInRange);

        if (itemInRange != null)
        {
            messagePanel.displayMessage(itemInRange);
        }
    }

    private void notHittingObject()
    {
        itemInRange = null;
        messagePanel.hideMessage();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    itemInRange = itemManager.getItem(other.gameObject.name);
    //    print(itemInRange);
    //    if (itemInRange != null)
    //    {
    //        messagePanel.displayMessage(itemInRange);
    //    }
            
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    itemInRange = null;
    //    messagePanel.hideMessage();
            
    //}

    //private void addItemToInventory(string objectName)
    //{
    //    itemManager.addToInventory("Syringe");
    //    messagePanel.hideMessage();
    //}
}
