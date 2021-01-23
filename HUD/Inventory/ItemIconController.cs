using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconController : MonoBehaviour
{
    //private Image icon;
    //private Image locka;

    // Start is called before the first frame update
    void Start()
    {
        //icon = GetImage("Icon");
        //locka = GetImage("Lock");
        //print(icon.color);
    }

    private Image GetImage(string name)
    {
        Transform[] childrens = this.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in childrens)
        {
            if (child.name == name)
            {
                return child.gameObject.GetComponent<Image>();
            }
        }
        return null;
    }

    public void RemoveLock()
    {
        var lockIcon = GetImage("Lock");
        var tmpColor = lockIcon.color;
        tmpColor.a = 0;

        lockIcon.color = tmpColor;
    }

    public void setMaxAlpha()
    {
        var icon = GetImage("Icon");

        var tmpColor = icon.color;
        tmpColor.a = 1f;

        icon.color = tmpColor;
    }

}
