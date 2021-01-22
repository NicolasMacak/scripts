using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class TextMessagesManager
{
    // Start is called before the first frame update
    Dictionary<string, string> disabledMessages = new Dictionary<string, string>();
    Dictionary<string, string> enabledMessages = new Dictionary<string, string>();
    Dictionary<string, string> usableMessages = new Dictionary<string, string>();

    public TextMessagesManager()
    {
        enabledMessages.Add(interactObjectNames.SYRINGE,  textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.BOTTLE, textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.PHONE, textSuffixes.PICK_UP);

        enabledMessages.Add(interactObjectNames.SyringeEnabler, "Vypnúť Lasery");
        enabledMessages.Add(interactObjectNames.BottleEnabler, "Vypnúť Lasery");
        enabledMessages.Add(interactObjectNames.PhoneEnabler, "Vypnúť Lasery");

        //usableMessages.Add(interactObjectNames.SYRINGE, "Zahodená vakcína " + textSuffixes.USE);
        //usableMessages.Add(interactObjectNames.BOTTLE, "Zahodená vakcína " + textSuffixes.USE);
        //usableMessages.Add(interactObjectNames.PHONE, "Zahodená vakcína " + textSuffixes.USE);

        disabledMessages.Add(interactObjectNames.SYRINGE, "Je potrebné overenie");
        disabledMessages.Add(interactObjectNames.BOTTLE, "Je potrebné overenie");
        disabledMessages.Add(interactObjectNames.PHONE, "Je potrebné overenie");

    }

    /// <summary>Returns message for locked items</summary>
    public string getDisabledMessage(string objectName)
    {
        return disabledMessages.ContainsKey(objectName) ? disabledMessages[objectName] : "";
    }

    /// <summary>Returns message for pickup items</summary>
    public string getEnabledMessage(string objectName)
    {

        return enabledMessages.ContainsKey(objectName) ? enabledMessages[objectName] : "";
    }

    /// <summary>Returns message for usable items</summary>
    //public string getUsableMessage(string objectName)
    //{
    //    return usableMessages.ContainsKey(objectName) ? usableMessages[objectName] : "";
    //}
}
