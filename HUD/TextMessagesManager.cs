using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class TextMessagesManager
{
    // Start is called before the first frame update
    Dictionary<string, string> impossible = new Dictionary<string, string>();
    Dictionary<string, string> pickup = new Dictionary<string, string>();
    Dictionary<string, string> use = new Dictionary<string, string>();

    public TextMessagesManager()
    {
        pickup.Add(interactObjectNames.SYRINGE, "Zahodená vakcína " + textSuffixes.PICK_UP);
        pickup.Add(interactObjectNames.BOTTLE, "Vodka s Bromhexinom " + textSuffixes.PICK_UP);
        pickup.Add(interactObjectNames.PHONE, "Dezinformačný mobil " + textSuffixes.PICK_UP);

        pickup.Add(interactObjectNames.SyringeEnabler, "Vypnúť Lasery");
        pickup.Add(interactObjectNames.BottleEnabler, "Vypnúť Lasery");
        pickup.Add(interactObjectNames.PhoneEnabler, "Vypnúť Lasery");

        use.Add(interactObjectNames.SYRINGE, "Zahodená vakcína " + textSuffixes.USE);
        use.Add(interactObjectNames.BOTTLE, "Zahodená vakcína " + textSuffixes.USE);
        use.Add(interactObjectNames.PHONE, "Zahodená vakcína " + textSuffixes.USE);

        impossible.Add(interactObjectNames.SYRINGE, "Je potrebné overenie");
        impossible.Add(interactObjectNames.BOTTLE, "Je potrebné overenie");
        impossible.Add(interactObjectNames.PHONE, "Je potrebné overenie");


    }

    /// <summary>Returns message for locked items</summary>
    public string getImpossible(string objectName)
    {
        return impossible.ContainsKey(objectName) ? impossible[objectName] : objectName + "";
    }

    /// <summary>Returns message for pickup items</summary>
    public string getPickup(string objectName)
    {

        return pickup.ContainsKey(objectName) ? pickup[objectName] : "";
    }

    /// <summary>Returns message for usable items</summary>
    public string getUse(string objectName)
    {
        return use.ContainsKey(objectName) ? use[objectName] : "";
    }
}
