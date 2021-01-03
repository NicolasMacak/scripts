using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    // Start is called before the first frame update

    public ItemManagerController itemManager;
    public MessagePanelController messagePanel;

    private InteractItem itemInRange;

    public GameObject synerge;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (itemInRange.state == InteractItem.States.PICKABLE || itemInRange.state == InteractItem.States.LOCKED) // remove Locked
            {
                itemManager.addToInventory(itemInRange.objectName);
            }
            else if (itemInRange.state == InteractItem.States.USABLE)
            {
                // itemManager.useItem
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        itemInRange = itemManager.getItem(other.gameObject.name);
        if (itemInRange != null)
        {
            messagePanel.displayMessage(itemInRange.state, itemInRange.objectName);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        itemInRange = null;
        messagePanel.hideMessage();
            
    }

    private void addItemToInventory(string objectName)
    {
        itemManager.addToInventory("Syringe");
        messagePanel.hideMessage();
    }

    private void removeGameObject(string objectName)
    {
        Destroy(GameObject.Find(objectName));
    }
}
