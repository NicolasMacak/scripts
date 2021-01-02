using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanelController : MonoBehaviour
{
    class messages
    {
      public const string PICKUP = "Zobrať (E)";
      public const string READ = "Prečítať (E)";
    }

    public TextMeshProUGUI message;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        message.text = "";
    }

    public void displayMessage(int interactItemType, string name)
    {
        this.gameObject.SetActive(true);
        
        message.text = name + " " + getMessageByType(interactItemType);
    }

    private string getMessageByType(int interactItemType)
    {
        switch (interactItemType)
        {
            case InteractItem.Types.PRIMARY:
            case InteractItem.Types.SECONDARY:
                return messages.PICKUP;

            case InteractItem.Types.READABLE:
                return messages.READ;
        }

        return "";
    }

    public void hideMessage()
    {
        this.gameObject.SetActive(false);

    }
}
