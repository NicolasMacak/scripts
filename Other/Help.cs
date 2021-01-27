using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundController;
public static class Help
{
    

    public static string GetOltarChildName(string oltarName) // Based on oltar name, return oltard children. (Smoke to be activated)
    {
        return Constants.nameSpacesStrings.Oltar + oltarName.Replace(Constants.nameSpacesStrings.Oltar, "");
    }

    public static GameObject FindChild(this GameObject parent, string name) // Find child of game object. Used to activate object which are not activated
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

    public static List<Vector3> ReturnPositionsOfChildrens(string parentName) // used to get Wandering points
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

    public static void ActivateOltarChild(string oltarName) // used when player puts object of power to oltar
    {
        GameObject oltarChild = FindChild(GameObject.Find(oltarName), Help.GetOltarChildName(oltarName));

        if (oltarChild == null) { return; }
        oltarChild.SetActive(true);
    }

    public static void ChangeAudioState(AudioState audioState) // paused and unpaused audio
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
    public static void StarPlatinumZaWarudo() // Stops time and paused audio
    {
        Time.timeScale = 0; // stop game while reading
        ChangeAudioState(AudioState.Pause);
    }

    public static void TimeHasBegunToMoveAgain() // time flows again and audio is unpaused
    {
        Time.timeScale = 1;
        ChangeAudioState(AudioState.Play);
    }

    public static void ActivateNakabot() // removes nakabot object from the table and activates the real nakabot
    {
        var nakabot = FindChild(GameObject.Find("Enemy"), "Nakabot");
        nakabot.SetActive(true);

        var nakabotToRemove = GameObject.Find("NakabotToRemove");
        nakabotToRemove.SetActive(false);
    }

    public static bool IsNakabotActivated() // checks if nakabot is activated. Used to not activate him again
    {
        var nakabot = FindChild(GameObject.Find("Enemy"), "Nakabot");
        return nakabot.activeSelf;
    }
}
