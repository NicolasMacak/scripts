using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMessagesManager
{
    // Start is called before the first frame update
    Dictionary<string, string> impossible = new Dictionary<string, string>();
    Dictionary<string, string> pickup = new Dictionary<string, string>();
    Dictionary<string, string> use = new Dictionary<string, string>();

    public TextMessagesManager()
    {
        impossible.Add(Constants.interactObjectNames.SYRINGE, "Je potrebný scan ruky");

        pickup.Add(Constants.interactObjectNames.SYRINGE, "Zahodená vakcína " + Constants.textSuffixes.PICK_UP);

        use.Add(Constants.interactObjectNames.SYRINGE, "Zahodená vakcína " + Constants.textSuffixes.USE);
    }

    /// <summary>Returns message for locked items</summary>
    public string getLocked(string objectName)
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
