using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundClip[] soundClips;

    public enum SoundName
    {
        Pickup,
        Read
    }

    public enum AudioState
    {
        Play,
        Pause
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    [System.Serializable]
    public class SoundClip
    {
        public SoundName soundName;
        public AudioClip audioClip;
    }

    private AudioClip FindClip(SoundName soundName)
    {
        foreach(SoundClip soundClip in soundClips)
        {
            if(soundClip.soundName == soundName)
            {
                return soundClip.audioClip;
            }
        }
        print("No such clip");
        return null;
    }

    public void PlayClip(SoundName soundName)
    {
        AudioClip audioClip = FindClip(soundName);

        if(audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }




}
