using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundClip[] soundClips;
    private EnemySoundName currentClip;
    
    public enum EnemySoundName
    {
        Wandering,
        Running,
        LookingForPlayer
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PlayClipLoop(EnemySoundName.Wandering);
        
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

        if (audioClip == null) { return; }

        currentClip = soundName;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayClipLoop(EnemySoundName soundName)
    {
        audioSource.loop = true;
        PlayClip(soundName);
    }

    public void PlayClipOnce(EnemySoundName soundName)
    {
        if (soundName == currentClip) { return; } // if song has not been changed, return. Issue of calling this function could be resolved with coroutine. Did not know at the time

        audioSource.loop = false;
        PlayClip(EnemySoundName.Running);      
    }

}

