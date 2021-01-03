using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
         items.Add(Constants.interactObjectNames.SYRINGE, new InteractItem(Constants.interactObjectNames.SYRINGE, "Zahodená vakcína", InteractItem.Types.PRIMARY, InteractItem.States.LOCKED));

        // Secondary items
        // items.Add("Synerge", new InteractItem("Zahodená vakcína", InteractItem.Types.PRIMARY, InteractItem.States.LOCKED));

        // Readable items
        // items.Add("Synerge", new InteractItem("Zahodená vakcína", InteractItem.Types.PRIMARY, InteractItem.States.LOCKED));

    }

    public void addToInventory(string objectName)
    {
        items[objectName].state = InteractItem.States.USABLE;
        Destroy(GameObject.Find(objectName));
    }

    public InteractItem getItem(string name)
    {
       return items.ContainsKey(name) ? items[name]: null;
    }

    public void isOwned(GameObject itemToAdd)
    {
        // inventory.Add(itemToAdd.name)
    }
}
