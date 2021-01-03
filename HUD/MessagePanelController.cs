using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public void displayMessage(int interactItemState, string name)
    {
        this.gameObject.SetActive(true);

        string textToDisplay = "";

        switch (interactItemState)
        {
            case InteractItem.States.LOCKED:
                textToDisplay = textMessageManager.getLocked(name);
                break;

            case InteractItem.States.PICKABLE:
                textToDisplay = textMessageManager.getPickup(name);
                break;

            case InteractItem.States.USABLE:
                textToDisplay = textMessageManager.getUse(name);
                break;
        }

        print(interactItemState);
        print(textToDisplay);
        message.text = textToDisplay;
    }

    //private string getMessageByType(int interactItemType)
    //{
    //    switch (interactItemType)
    //    {
    //        case InteractItem.Types.PRIMARY:
    //        case InteractItem.Types.SECONDARY:
    //            return messages.PICKUP;

    //        case InteractItem.Types.READABLE:
    //            return messages.READ;
    //    }

    //    return "";
    //}

    public void hideMessage()
    {
        this.gameObject.SetActive(false);

    }
}
