﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Help 
{

    public static string getOltarChildName(string oltarName)
    {
        return Constants.nameSpacesStrings.OLTAR + oltarName.Replace(Constants.nameSpacesStrings.OLTAR, "");
    }

    public static GameObject FindChild(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static void activateOltarChild(string oltarName)
    {
        GameObject oltarChild = Help.FindChild(GameObject.Find(oltarName), Help.getOltarChildName(oltarName));
        oltarChild.SetActive(true);
    }
}