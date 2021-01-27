using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static InteractItem;

public class MessagePanelController : MonoBehaviour
{
    TextMessagesManager textMessageManager = new TextMessagesManager();

    public TextMeshProUGUI message;
    public ItemManagerController inventory;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        message.text = "";
    }

    /// <summary>Returns message for locked items</summary>
    public void displayMessage(InteractItem interactItem)
    {
        this.gameObject.SetActive(true);

        string textToDisplay = "";

        textToDisplay = interactItem.title; // first row is title of object
        textToDisplay += addSecondRowDetail(interactItem); // in second are addition instructions
        
        message.text = textToDisplay;
    }

    private string addSecondRowDetail(InteractItem interactItem)
    {
        string textToReturn = "";

        if (interactItem.category == ItemCategory.READABLE)
        {
            textToReturn += Constants.textSuffixes.READ; // item to read
        }
        else if (!inventory.IsDemandItemOwned(interactItem.demandItem) || interactItem.state == ItemState.DISABLED) // cant interact because item is not owned or item is disabled
        {
            textToReturn += textMessageManager.getDisabledMessage(interactItem.objectName); 
        }
        else if (interactItem.state == ItemState.ENABLED) // item can be interacted with
        {
            textToReturn += textMessageManager.getEnabledMessage(interactItem.objectName); 
        }

        return System.Environment.NewLine + textToReturn;
    }

    public void hideMessage()
    {
        this.gameObject.SetActive(false);
    }
}
