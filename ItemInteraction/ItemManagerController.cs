﻿using System.Collections;
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
        items.Add(Constants.interactObjectNames.SYRINGE, new InteractItem(Constants.interactObjectNames.SYRINGE, "Zahodená vakcína", ItemCategory.PICKABLE, ItemState.DISABLED));
        items.Add("SyringeOltar", new InteractItem("SyringeOltar", "Oltár Zahodená vakcína", ItemCategory.OLTAR, ItemState.ENABLED));

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
        switch (interactItem.category)
        {
            case InteractItem.ItemCategory.OLTAR:
                interactWithOltar(interactItem.objectName);
                break;

            case InteractItem.ItemCategory.PICKABLE:
                pickItem(interactItem.objectName);
                break;

            case InteractItem.ItemCategory.ENABLER:
                enableItem(interactItem.objectName);
                break;

            case InteractItem.ItemCategory.READABLE:
                interactWithReadable(interactItem.objectName);
                break;
        }
    }

    //might be renamed to pillar 
    private void interactWithOltar(string objectName)
    {   
        Help.activateOltarChild(objectName); //place item on pillar. Spawn items to the game
    }

    private void interactWithPicklable(string objectName)
    {
        //add item to inventory
        //Destroy item


    }

    private void interactWithReadable(string objectName)
    {
        // read note
    }

    private void pickItem(string objectName)
    {
        items[objectName].state = InteractItem.ItemState.USABLE;
        Destroy(GameObject.Find(objectName));
    }

    private void enableItem(string itemToEnable)
    {
        items[itemToEnable].state = InteractItem.ItemState.ENABLED;
        //itemToEnable+lasers - disable 
        // disableColider
    }
}
