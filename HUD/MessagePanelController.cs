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

        if(interactItem.category == ItemCategory.READABLE)
        {
            textToDisplay = interactItem.title + Constants.textSuffixes.READ;
        }
        else
        {
            switch (interactItem.state)
            {
                case ItemState.DISABLED:
                    textToDisplay = textMessageManager.getLocked(interactItem.objectName);
                    break;

                case ItemState.ENABLED:
                    textToDisplay = textMessageManager.getPickup(interactItem.objectName);
                    break;

                case ItemState.USABLE:
                    textToDisplay = textMessageManager.getUse(interactItem.objectName);
                    break;
            }
        }

        if(textToDisplay == "")
        {
            print("text este nebol priradeny");
        }
        else
        {
            print(textToDisplay);
        }
        
        message.text = textToDisplay;
    }

    public void hideMessage()
    {
        this.gameObject.SetActive(false);

    }
}
