using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    // Start is called before the first frame update

    public ItemManagerController itemManager; //= GetComponent<ItemManagerController>();// new ItemManagerController();
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
            addItemToInventory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        itemInRange = itemManager.getItem(other.gameObject.name); // check ci je to interactableItem, ci sa taky item nachadza 
        print(itemInRange);
        if (itemInRange != null)
        {
            // itemInRange = synerge;
            messagePanel.displayMessage(itemInRange.type, itemInRange.title);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        itemInRange = null;
        messagePanel.hideMessage();
            
    }

    private void addItemToInventory()
    {
        itemManager.addToInventory("Syringe");
        messagePanel.hideMessage();
    }

    private void removeGameObject(string objectName)
    {
        Destroy(GameObject.Find(objectName));
    }
}
