using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundClip[] soundClips;

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

    public enum SoundName
    {
        ChaseMusic,
        AmbientMusic
    }
}
