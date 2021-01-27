using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class TextMessagesManager
{
    
    Dictionary<string, string> disabledMessages = new Dictionary<string, string>();
    Dictionary<string, string> enabledMessages = new Dictionary<string, string>();
    
    // messages used to inform player about item which he is about to interact with
    public TextMessagesManager()
    {
        enabledMessages.Add(interactObjectNames.SYRINGE,  textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.BOTTLE, textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.PHONE, textSuffixes.PICK_UP);

        enabledMessages.Add(interactObjectNames.SyringeEnabler, "Deaktivovat Lasery (E)");
        enabledMessages.Add(interactObjectNames.BottleEnabler, "Deaktivovat Lasery (E)");
        enabledMessages.Add(interactObjectNames.PhoneEnabler, "Deaktivovat Lasery (E)");

        enabledMessages.Add(interactObjectNames.SyringeOltar, "Znicit vakcinu (E)");
        enabledMessages.Add(interactObjectNames.PhoneOltar, "Znicit mobil (E)");
        enabledMessages.Add(interactObjectNames.BottleOltar, "Znicit vodku (E)");

        enabledMessages.Add(interactObjectNames.SyringeCard, textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.BottleCard, textSuffixes.PICK_UP);
        enabledMessages.Add(interactObjectNames.PhoneCard, textSuffixes.PICK_UP);

        // disabled pre Enablery
        disabledMessages.Add(interactObjectNames.SyringeEnabler, "Treba kartu Dutoschwarzca");
        disabledMessages.Add(interactObjectNames.BottleEnabler, "Treba kartu Nefaria");
        disabledMessages.Add(interactObjectNames.PhoneEnabler, "Treba kartu Selviga");
        
        // Disabled pre lasery
        disabledMessages.Add(interactObjectNames.SYRINGE, "Lasery su zapnute");
        disabledMessages.Add(interactObjectNames.BOTTLE, "Lasery su zapnute");
        disabledMessages.Add(interactObjectNames.PHONE, "Lasery su zapnute");

        disabledMessages.Add(interactObjectNames.SyringeOltar, "Nemam vakcinu");
        disabledMessages.Add(interactObjectNames.PhoneOltar, "Nemam mobil");
        disabledMessages.Add(interactObjectNames.BottleOltar, "Nemam vodku s bromhexinom");




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
}
