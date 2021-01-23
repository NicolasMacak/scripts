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

        enabledMessages.Add(interactObjectNames.SyringeEnabler, "Deaktivovat Lasery");
        enabledMessages.Add(interactObjectNames.BottleEnabler, "Deaktivovat Lasery");
        enabledMessages.Add(interactObjectNames.PhoneEnabler, "Deaktivovat Lasery");

        // disabled pre Enablery
        disabledMessages.Add(interactObjectNames.SyringeEnabler, "Treba kartu Dutoschwarzca");
        disabledMessages.Add(interactObjectNames.BottleEnabler, "Treba kartu Bottla");
        disabledMessages.Add(interactObjectNames.PhoneEnabler, "Treba kartu Phona");
        
        // Disabled pre lasery
        disabledMessages.Add(interactObjectNames.SYRINGE, "Lasery su zapnute");
        disabledMessages.Add(interactObjectNames.BOTTLE, "Lasery su zapnute");
        disabledMessages.Add(interactObjectNames.PHONE, "Lasery su zapnute");



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
