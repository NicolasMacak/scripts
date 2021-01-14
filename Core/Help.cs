using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Help 
{

    public static string getOltarChildName(string oltarName)
    {
        return Constants.nameSpacesStrings.Oltar + oltarName.Replace(Constants.nameSpacesStrings.Oltar, "");
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

    public static List<Vector3> returnPositionsOfChildrens(string parentName)
    {
        var parent = GameObject.Find(parentName);
        var positionsToReturn = new List<Vector3>();

        if(parent == null) return null;
        

        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            positionsToReturn.Add(t.position);
        }

        return positionsToReturn;
    }

   // public static pickRandomElement() { }

    public static void activateOltarChild(string oltarName)
    {
        GameObject oltarChild = Help.FindChild(GameObject.Find(oltarName), Help.getOltarChildName(oltarName));
        oltarChild.SetActive(true);
    }
}
