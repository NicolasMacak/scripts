using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem
{
    // Objekty v inventary sa budu snapovat s tymi v realite pomocou nazvu

    public string objectName { get; } // identifier of object in game
    public ItemCategory category { get; set; }
    public ItemState state { get; set; }
    public string title { get; }
    public string demandItem { get; } // item demanded to switch state from disbled to enabled

    public enum ItemCategory
    {
        OLTAR = 0,
        PICKABLE = 1,
        ENABLER = 2,
        READABLE = 3
    }

    public enum ItemState
    {
        DISABLED = 0, // item cant be interacted with
        ENABLED = 1, // item can be interacted with
        USABLE = 2, // item can be used lepsie by bolo owned
        REMOVED = 3 // item is removed
    }

    public InteractItem(string objectName, string title, ItemCategory type, ItemState state, string demandItem)
    {
        this.objectName = objectName;
        this.title = title;
        this.category = type;
        this.state = state;
        this.demandItem = demandItem;
    }
    // READABLE ITEM CONSTRUCTOR
    public InteractItem(string objectName, string title) 
    {
        this.objectName = objectName;
        this.title = title;

        this.category = ItemCategory.READABLE;
        this.state = ItemState.ENABLED;
        this.demandItem = null;
    }


}
