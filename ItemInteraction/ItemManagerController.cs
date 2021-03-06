﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractItem;
using static Constants;
using static Help;
using static SoundController;

public class ItemManagerController : MonoBehaviour
{

    private Dictionary<string, InteractItem> items = new Dictionary<string, InteractItem>();

    public HudInventoryController inventoryHUD;
    public MessagePanelController messageManager;
    public RespawnController respawnController;
    public ArtefactForceFieldController artefact;
    public SoundController playerSoundManager;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAllItems();       
    }

    private void InitializeAllItems()
    {
        // Items of power
        items.Add(interactObjectNames.SYRINGE, new InteractItem(interactObjectNames.SYRINGE, "Zahodena vakcina", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add(interactObjectNames.BOTTLE, new InteractItem(interactObjectNames.BOTTLE, "Vodka s Bromhexinom", ItemCategory.PICKABLE, ItemState.DISABLED, null));
        items.Add(interactObjectNames.PHONE, new InteractItem(interactObjectNames.PHONE, "Dezinformačný mobil", ItemCategory.PICKABLE, ItemState.DISABLED, null));

        // Oltars
        items.Add(interactObjectNames.SyringeOltar, new InteractItem(interactObjectNames.SyringeOltar, "Desturktor vakciny", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.SYRINGE));
        items.Add(interactObjectNames.BottleOltar, new InteractItem(interactObjectNames.BottleOltar, "Destruktor vodky s bromhexinom", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.BOTTLE));
        items.Add(interactObjectNames.PhoneOltar, new InteractItem(interactObjectNames.PhoneOltar, "Destruktor mobilu", ItemCategory.OLTAR, ItemState.ENABLED, interactObjectNames.PHONE));

        // Enablers
        items.Add(interactObjectNames.SyringeEnabler, new InteractItem(interactObjectNames.SyringeEnabler, "Deaktivovat lasery", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.SyringeCard));
        items.Add(interactObjectNames.BottleEnabler, new InteractItem(interactObjectNames.BottleEnabler, "Deaktivovat lasery", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.BottleCard));
        items.Add(interactObjectNames.PhoneEnabler, new InteractItem(interactObjectNames.PhoneEnabler, "Deaktivovat lasery", ItemCategory.ENABLER, ItemState.ENABLED, interactObjectNames.PhoneCard));

        // Cards
        items.Add(interactObjectNames.SyringeCard, new InteractItem(interactObjectNames.SyringeCard, "Karta Dr. Dutoschwartza", ItemCategory.PICKABLE, ItemState.ENABLED, null));
        items.Add(interactObjectNames.BottleCard, new InteractItem(interactObjectNames.BottleCard, "Karta Dr. Nefaria", ItemCategory.PICKABLE, ItemState.ENABLED, null));
        items.Add(interactObjectNames.PhoneCard, new InteractItem(interactObjectNames.PhoneCard, "Karta Dr. Selviga", ItemCategory.PICKABLE, ItemState.ENABLED, null));

        // Notes
        items.Add(interactObjectNames.NOTENakabot, new InteractItem(interactObjectNames.NOTENakabot, "Nakabot 6.4.9"));
        items.Add(interactObjectNames.NOTEMission, new InteractItem(interactObjectNames.NOTEMission, "Tvoja uloha"));
        items.Add(interactObjectNames.NOTESyringe, new InteractItem(interactObjectNames.NOTESyringe, "Zahodena vakcina"));
        items.Add(interactObjectNames.NOTEPhone, new InteractItem(interactObjectNames.NOTEPhone, "Dezinformacny mobil"));
        items.Add(interactObjectNames.NOTEBottle, new InteractItem(interactObjectNames.NOTEBottle, "Vodka s bromhexinom"));
    }

    public InteractItem GetItem(string name)
    {
       if (items.TryGetValue(name, out InteractItem item))
        {
            return item;

        }

        return null;
    }
    
    public void InteractWithItem(InteractItem interactItem)
    {
        if (!IsDemandItemOwned(interactItem.demandItem)) // required demand item item is now owned
        {
            return;
        }

        playerSoundManager.PlayClip(SoundName.Pickup);

        switch (interactItem.category)
        {
            case ItemCategory.OLTAR:
                InteractWithOltar(interactItem.objectName);
                break;

            case ItemCategory.PICKABLE:
                PickItem(interactItem.objectName);       
                break;

            case ItemCategory.ENABLER:
                EnableItem(interactItem.objectName);
                break;
        }

        messageManager.hideMessage(); // remove info about interaction when interaction is over
    }

    /// <summary>
    /// Checks if demand item is in the inventory. Return true if item is owned or if there is no item to be demanded. False otherwise.
    /// </summary>
    /// <param name="demandItemName"></param>
    /// <returns></returns>
    public bool IsDemandItemOwned(string demandItemName)
    {
        return demandItemName != null && items.ContainsKey(demandItemName) ? items[demandItemName].state == ItemState.USABLE : true;
    }

  
    private void InteractWithOltar(string objectName)
    {
        Help.ActivateOltarChild(objectName); //place item on pillar. Spawn items to the game
        items[objectName].state = ItemState.REMOVED;

        if (HaveAllOltarsBeenActivated()) { artefact.TriggerDestruction(); } // end the game
    }

    private void PickItem(string objectName)
    {
        items[objectName].state = ItemState.USABLE;
        Destroy(GameObject.Find(objectName));
        
        if (objectName.Contains(nameSpacesStrings.Card)) // is object Card
        {
            inventoryHUD.UnlockIcon(objectName.Replace(nameSpacesStrings.Card, "")); // Remove substring Card from object name and remove lock
        } 
        else // object is item of power
        {
            inventoryHUD.MakeOwned(objectName);
            respawnController.setCheckPoint(objectName);

            if (!IsNakabotActivated()) { ActivateNakabot(); } // active nakabot after first object of power is picked
        }
    }

    private void EnableItem(string enablerObjectName) // makes some object enabled, item used to do this is removed
    {
        var itemToEnable = enablerObjectName.Replace(nameSpacesStrings.Enabler, "");
        items[itemToEnable].state = ItemState.ENABLED; // enable item to pickup
        items[enablerObjectName].state = ItemState.REMOVED; // prevent from using again

        DisableLasers(itemToEnable);
    }

    private bool HaveAllOltarsBeenActivated() // when this function returns true, game is over
    {
        return items[interactObjectNames.SyringeOltar].state == ItemState.REMOVED &&
               items[interactObjectNames.BottleOltar].state == ItemState.REMOVED &&
               items[interactObjectNames.PhoneOltar].state == ItemState.REMOVED;
    }

    private void DisableLasers(string primaryItemName) // Called after enabler is used
    {
       var lasers = GameObject.Find(primaryItemName + nameSpacesStrings.Lasers); // getLasers which are paired to object of power
        
       if(lasers == null) { print("no such lasers");  return; }

       lasers.SetActive(false);
    }
}
