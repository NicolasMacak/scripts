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
    public SoundController playerSoundManager;

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
            if (itemInRange != null && itemInRange.state == ItemState.ENABLED) // remove Locked
            {
                if(itemInRange.category == ItemCategory.READABLE)
                {
                    isPlayerReading = isPlayerReading ? noteManager.hideNote() : noteManager.displayNote(itemInRange.objectName); // switch between reading state
                    if (isPlayerReading) { playerSoundManager.PlayClip(SoundName.Read); } 
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

    private void ZaHando() // ruka
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
        {
            touchingObject(hit.collider.gameObject);
        } else
        {
            notTouchingAnyObject();
        }
    }

    private void touchingObject(GameObject objectInRange)
    {
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        itemInRange = itemManager.getItem(objectInRange.name);
        //print(itemInRange);

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
