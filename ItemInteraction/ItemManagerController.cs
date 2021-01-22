using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;
using static Constants;

public class ItemManagerController : MonoBehaviour
{

    private Dictionary<string, InteractItem> items = new Dictionary<string, InteractItem>();

    public HudInventoryController inventoryHUD;

    // Start is called before the first frame update
    void Start()
    {
        initializeAllItems();
        
    }

    private void initializeAllItems()
    {
        // Primary items
        items.Add(interactObjectNames.SYRINGE, new InteractItem(interactObjectNames.SYRINGE, "Zahodená vakcína", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add(interactObjectNames.BOTTLE, new InteractItem(interactObjectNames.BOTTLE, "Vodka s Bromhexinom", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add(interactObjectNames.PHONE, new InteractItem(interactObjectNames.PHONE, "Dezinformačný mobil", ItemCategory.PICKABLE, ItemState.DISABLED, null));


        items.Add(interactObjectNames.SyringeOltar, new InteractItem(interactObjectNames.SyringeOltar, "Oltár Vakcína", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.SYRINGE));
        items.Add(interactObjectNames.BottleOltar, new InteractItem(interactObjectNames.BottleOltar, "Oltár Vodka", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.BOTTLE));
        items.Add(interactObjectNames.PhoneOltar, new InteractItem(interactObjectNames.PhoneOltar, "Oltár Mobil", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.PHONE));


        items.Add(interactObjectNames.SyringeEnabler, new InteractItem(interactObjectNames.SyringeEnabler, "Enabler vakcína", ItemCategory.ENABLER, ItemState.ENABLED, null));
        items.Add(interactObjectNames.BottleEnabler, new InteractItem(interactObjectNames.BottleEnabler, "Enabler Vodka", ItemCategory.ENABLER, ItemState.ENABLED, null));
        items.Add(interactObjectNames.PhoneEnabler, new InteractItem(interactObjectNames.PhoneEnabler, "Enabler Mobil", ItemCategory.ENABLER, ItemState.ENABLED, null));

        items.Add(interactObjectNames.NOTE01, new InteractItem(interactObjectNames.NOTE01, "Poznámka" + System.Environment.NewLine + "boga boga"));

        // Secondary items
        // items.Add("Synerge", new InteractItem("Zahodená vakcína", InteractItem.Category.OLTAR, InteractItem.ItemState.DISABLED));

        // Readable items
        // items.Add("Synerge", new InteractItem("Zahodená vakcína", InteractItem.Category.OLTAR, InteractItem.ItemState.DISABLED));

    }

    public InteractItem getItem(string name)
    {
       //return items.ContainsKey(name) ? items[name]: null;
       if (items.TryGetValue(name, out InteractItem item))
        {
            return item;

        }

        return null;
    }
    
    public void interactWithItem(InteractItem interactItem)
    {
        if (!isDemandItemOwned(interactItem.demandItem))
        {
            print(interactItem.demandItem + " not owned");
            return;
        }

        switch (interactItem.category)
        {
            case ItemCategory.OLTAR:
                interactWithOltar(interactItem.objectName);
                break;

            case ItemCategory.PICKABLE:
                pickItem(interactItem.objectName);
                break;

            case ItemCategory.ENABLER:
                enableItem(interactItem.objectName);
                break;
        }
    }

    /// <summary>
    /// Checks if demand item is in the inventory. Return true if item is owned or if there is no item to be demanded. False otherwise.
    /// </summary>
    /// <param name="demandItemName"></param>
    /// <returns></returns>
    public bool isDemandItemOwned(string demandItemName)
    {
        return demandItemName != null && items.ContainsKey(demandItemName) ? items[demandItemName].state == ItemState.USABLE : true;
    }

  
    private void interactWithOltar(string objectName)
    {   
        Help.activateOltarChild(objectName); //place item on pillar. Spawn items to the game
    }

    private void pickItem(string objectName)
    {
        items[objectName].state = ItemState.USABLE;
        Destroy(GameObject.Find(objectName));
        inventoryHUD.MakeOwned(objectName);
    }

    private void enableItem(string enablerObjectName)
    {
        var itemToEnable = enablerObjectName.Replace(nameSpacesStrings.Enabler, "");
        print("toEnable" + itemToEnable);
        items[itemToEnable].state = ItemState.ENABLED;
        items[enablerObjectName].state = ItemState.DISABLED; // enabler is used

        if(isPrimaryItemEnabler(enablerObjectName)) { disableLasers(itemToEnable); }
    }

    private bool isPrimaryItemEnabler(string itemToEnable)
    {
        return itemToEnable.Contains(interactObjectNames.SYRINGE); // doplnit dalsie
    }

    private void disableLasers(string primaryItemName)
    {
       var lasers = GameObject.Find(primaryItemName + nameSpacesStrings.Lasers);
        
       if(lasers == null) { print("no such lasers");  return; }

       lasers.SetActive(false);
    }
}
