using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconController : MonoBehaviour
{

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

    public void SetMaxAlpha()
    {
        var icon = GetImage("Icon");

        var tmpColor = icon.color;
        tmpColor.a = 1f;

        icon.color = tmpColor;
    }

}
