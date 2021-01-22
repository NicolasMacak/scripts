using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static InteractItem;

public class MessagePanelController : MonoBehaviour
{
    //class messages
    //{
    //  public const string PICKUP = "Zobrať (E)";
    //  public const string READ = "Prečítať (E)";
    //}
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

        textToDisplay = interactItem.title + " (E)";
        textToDisplay += addSecondRowDetail(interactItem);
        
        message.text = textToDisplay;
    }

    private string addSecondRowDetail(InteractItem interactItem)
    {
        string textToReturn = "";

        if (interactItem.category == ItemCategory.READABLE)
        {
            textToReturn += Constants.textSuffixes.READ; // item to read
        }
        else if(!inventory.isDemandItemOwned(interactItem.demandItem) || interactItem.state == ItemState.DISABLED)
        {
            textToReturn += textMessageManager.getDisabledMessage(interactItem.objectName); // cant interact because item is not owned
        }
        else
        {
            textToReturn += textMessageManager.getEnabledMessage(interactItem.objectName); // all good
        }

        return System.Environment.NewLine + textToReturn;
    }

    public void hideMessage()
    {
        this.gameObject.SetActive(false);

    }
}
