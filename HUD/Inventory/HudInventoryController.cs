using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class HudInventoryController : MonoBehaviour
{
    // Start is called before the first frame update

    private Dictionary<string, ItemIconController> icons = new Dictionary<string, ItemIconController>();

    public ItemIconController syringe;
    public ItemIconController bottle;
    public ItemIconController phone;


    void Start()
    {   
        icons.Add(interactObjectNames.SYRINGE, syringe);
        icons.Add(interactObjectNames.BOTTLE, bottle);
        icons.Add(interactObjectNames.PHONE, phone);

        //MakeOwned(interactObjectNames.SYRINGE);
    }

    private void Update()
    {
        //MakeOwned(interactObjectNames.SYRINGE);
    }

    public void UnlockIcon(string item)
    {
        var itemIcon = icons.ContainsKey(item) ? icons[item] : null;

        if(itemIcon == null)
        {
            print("no such icon: " + item);
            return;
        }

        itemIcon.RemoveLock();
    }

    public void MakeOwned(string item)
    {
        var itemIcon = icons.ContainsKey(item) ? icons[item] : null;

        if (itemIcon == null)
        {
            print("no such icon");
            return;
        }

        itemIcon.setMaxAlpha();
    }
}
