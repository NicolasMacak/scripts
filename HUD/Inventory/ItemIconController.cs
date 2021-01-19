using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconController : MonoBehaviour
{
    private Image icon;


    // Start is called before the first frame update
    void Start()
    {
        icon = GetImage("Icon");

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

    }

    public void setMaxAlpha()
    {
        var tmpColor = icon.color;
        tmpColor.a = 1f;

        icon.color = tmpColor;
    }

}
