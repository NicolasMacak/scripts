using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;
using static Constants;

public class ItemManagerController : MonoBehaviour
{

    private Dictionary<string, InteractItem> items = new Dictionary<string, InteractItem>();

    public HudInventoryController inventoryHUD;
    public RespawnController respawnController;

    // Start is called before the first frame update
    void Start()
    {
        initializeAllItems();    
    }

    private void initializeAllItems()
    {
        // Items of power
        items.Add(interactObjectNames.SYRINGE, new InteractItem(interactObjectNames.SYRINGE, "Zahodená vakcína", ItemCategory.PICKABLE, ItemState.ENABLED, null));
        items.Add(interactObjectNames.BOTTLE, new InteractItem(interactObjectNames.BOTTLE, "Vodka s Bromhexinom", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add(interactObjectNames.PHONE, new InteractItem(interactObjectNames.PHONE, "Dezinformačný mobil", ItemCategory.PICKABLE, ItemState.DISABLED, null));

        // Oltars
        items.Add(interactObjectNames.SyringeOltar, new InteractItem(interactObjectNames.SyringeOltar, "Oltár Vakcína", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.SYRINGE));
        items.Add(interactObjectNames.BottleOltar, new InteractItem(interactObjectNames.BottleOltar, "Oltár Vodka", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.BOTTLE));
        items.Add(interactObjectNames.PhoneOltar, new InteractItem(interactObjectNames.PhoneOltar, "Oltár Mobil", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.PHONE));

        // Enablers
        items.Add(interactObjectNames.SyringeEnabler, new InteractItem(interactObjectNames.SyringeEnabler, "Enabler vakcína", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.SyringeCard));
        items.Add(interactObjectNames.BottleEnabler, new InteractItem(interactObjectNames.BottleEnabler, "Enabler Vodka", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.BottleCard));
        items.Add(interactObjectNames.PhoneEnabler, new InteractItem(interactObjectNames.PhoneEnabler, "Enabler Mobil", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.PhoneCard));

        // Cards
        items.Add(interactObjectNames.SyringeCard, new InteractItem(interactObjectNames.SyringeCard, "Karta Dutoschwarza", ItemCategory.PICKABLE, ItemState.ENABLED, null));
        items.Add(interactObjectNames.BottleCard, new InteractItem(interactObjectNames.BottleCard, "Karta Bottla", ItemCategory.PICKABLE, ItemState.ENABLED, null));
        items.Add(interactObjectNames.PhoneCard, new InteractItem(interactObjectNames.PhoneCard, "Karta Phona", ItemCategory.PICKABLE, ItemState.ENABLED, null));

        // Notes
        items.Add(interactObjectNames.NOTE01, new InteractItem(interactObjectNames.NOTE01, "Nakabot updaty"));

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
        
        //print(objectName.Replace(nameSpacesStrings.Card, ""));
        if (objectName.Contains(nameSpacesStrings.Card)) // is object Card
        {
            inventoryHUD.UnlockIcon(objectName.Replace(nameSpacesStrings.Card, "")); // Remove substring Card from object name and remove lock
        } 
        else // object is item of power
        {
            inventoryHUD.MakeOwned(objectName);
            respawnController.setCheckPoint(objectName);
        }
    }

    private void enableItem(string enablerObjectName)
    {
        var itemToEnable = enablerObjectName.Replace(nameSpacesStrings.Enabler, "");
        print("toEnable " + itemToEnable);
        items[itemToEnable].state = ItemState.ENABLED; // enable item to pickup
        items[enablerObjectName].state = ItemState.REMOVED; // prevent from using again
        
        if(isPrimaryItemEnabler(enablerObjectName)) { disableLasers(itemToEnable); } // tu sa moze podmienka odstranit
    }

    private bool isPrimaryItemEnabler(string itemToEnable)
    {
        return itemToEnable.Contains(interactObjectNames.SYRINGE) || itemToEnable.Contains(interactObjectNames.BOTTLE) || itemToEnable.Contains(interactObjectNames.PHONE); // doplnit dalsie
    }

    private void disableLasers(string primaryItemName)
    {
       var lasers = GameObject.Find(primaryItemName + nameSpacesStrings.Lasers);
        
       if(lasers == null) { print("no such lasers");  return; }

       lasers.SetActive(false);
    }
}
