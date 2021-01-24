using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundController;
public static class Help
{
    

    public static string GetOltarChildName(string oltarName)
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

        if (parent == null) return null;


        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            positionsToReturn.Add(t.position);
        }

        return positionsToReturn;
    }

    // public static pickRandomElement() { }

    public static void ActivateOltarChild(string oltarName)
    {
        GameObject oltarChild = FindChild(GameObject.Find(oltarName), Help.GetOltarChildName(oltarName));

        if (oltarChild == null) { return; }
        oltarChild.SetActive(true);
    }

    public static void ChangeAudioState(AudioState audioState)
    {
        var allAudioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource audioSource in allAudioSources)
        {
            if(audioState == AudioState.Pause)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }
        }
    }
    /// <summary>
    /// Stops time
    /// </summary>
    public static void StarPlatinumZaWarudo()
    {
        Time.timeScale = 0; // stop game while reading
        ChangeAudioState(AudioState.Pause);
    }

    public static void TimeHasBegunToMoveAgain()
    {
        Time.timeScale = 1;
        ChangeAudioState(AudioState.Play);
    }

    public static void ActivateNakabot()
    {
        var nakabot = FindChild(GameObject.Find("Enemy"), "Nakabot");
        nakabot.SetActive(true);

        var nakabotToRemove = GameObject.Find("NakabotToRemove");
        nakabotToRemove.SetActive(false);
    }

    public static bool isNakabotActivated()
    {
        var nakabot = FindChild(GameObject.Find("Enemy"), "Nakabot");
        return nakabot.activeSelf;
    }
}
