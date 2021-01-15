using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundClip[] soundClips;
    private EnemySoundName currentClip;

   // private bool hasSoundBeenPlayed;
    
    public enum EnemySoundName
    {
        Wandering,
        Running,
        LookingForPlayer
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>(); //gameObject.AddComponent<AudioSource>();
        PlayLoopClip(EnemySoundName.Wandering);

        //hasSoundBeenPlayed = false;
        
    }

    [System.Serializable]
    public class SoundClip
    {
        public EnemySoundName soundName;
        public AudioClip audioClip;
    }

    private AudioClip FindClip(EnemySoundName soundName)
    {
        foreach (SoundClip soundClip in soundClips)
        {
            if (soundClip.soundName == soundName)
            {
                return soundClip.audioClip;
            }
        }
        print("No such clip");
        return null;
    }

    public void PlayClip(EnemySoundName soundName)
    {
        AudioClip audioClip = FindClip(soundName);

        if (audioClip == null)
        {
            print("no such enemySound");
            return;
        }
            currentClip = soundName;
            //audioSource.loop = false;
            audioSource.clip = audioClip;
            audioSource.Play();
        
    }

    public void PlayLoopClip(EnemySoundName soundName)
    {
        //currentClip = soundName;
        audioSource.loop = true;
        PlayClip(soundName);
    }

    public void PlayClipOnce(EnemySoundName soundName)
    {
        if (soundName == currentClip) { return; } // if song has not been changed, return

       // hasSoundBeenPlayed = true;
        audioSource.loop = false;
        PlayClip(EnemySoundName.Running);      
    }

    public void PlayRunningSound()
    {
        if(currentClip == EnemySoundName.Running /*&& audioSource.isPlaying*/)
        {
            return;
        }

        PlayClip(EnemySoundName.Running);
    }

    //private bool hasOneShotSoundBeenTriggered(EnemySoundName soundName)
    //{
    //    return currentClip == soundName && !hasSoundBeenPlayed; // remove isPlaying 
    //}
}

