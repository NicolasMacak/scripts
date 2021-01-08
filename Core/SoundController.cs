using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{


    public Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();

    private AudioSource audioSource;
    private AudioClip audioClip;
    private string soundPath;

    public enum SoundName
    {
        Pickup,
        put
    }

    [System.Serializable]
    public class SoundClip
    {
        public SoundName soundName;
        public AudioClip audioClip;
    }

    
    public SoundClip[] soundClips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        soundPath = "file://" + Application.streamingAssetsPath + "/Sounds/";
        StartCoroutine(LoadAudio());
    }

    // Update is called once per frame
    private IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile(soundPath, "spaceDoor.wav");
        yield return request;

        audioClip = request.GetAudioClip();

        PlayAudioFile();
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

}
