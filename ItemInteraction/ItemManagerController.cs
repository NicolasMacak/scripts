using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;
using static Constants;

public class ItemManagerController : MonoBehaviour
{

    Dictionary<string, InteractItem> items = new Dictionary<string, InteractItem>();

    // Start is called before the first frame update
    void Start()
    {
        initializeAllItems();
        
    }

    private void initializeAllItems()
    {
        // Primary items
        items.Add(interactObjectNames.SYRINGE, new InteractItem(interactObjectNames.SYRINGE, "Zahodená vakcína", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add("SyringeOltar", new InteractItem("SyringeOltar", "Oltár Zahodená vakcína", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.SYRINGE));
        items.Add("SyringeEnabler", new InteractItem("SyringeEnabler", "Enabler vakcína", ItemCategory.ENABLER, ItemState.ENABLED, null));

        items.Add(interactObjectNames.NOTE01, new InteractItem(interactObjectNames.NOTE01, "Poznámka"));




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
            print(interactItem.demandItem + " note owned");
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
    private bool isDemandItemOwned(string demandItemName)
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
    }

    private void enableItem(string enablerObjectName)
    {
        var itemToEnable = enablerObjectName.Replace(nameSpacesStrings.Enabler, "");
        print(itemToEnable);
        items[itemToEnable].state = ItemState.ENABLED;
        //itemToEnable+lasers - disable 
        // disableColider
    }

}
