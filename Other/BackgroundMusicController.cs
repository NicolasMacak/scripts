using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundClip[] soundClips;
    private BackGroundMusic actualClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.25f;
        PlayClip(BackGroundMusic.AmbientMusic);
        //StartCoroutine(FadeIntoAmbientMusic());
    }

    [System.Serializable]
    public class SoundClip
    {
        public BackGroundMusic soundName;
        public AudioClip audioClip;
    }

    public enum BackGroundMusic
    {
        ChaseMusic,
        AmbientMusic
    }

    private AudioClip FindClip(BackGroundMusic soundName)
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

    public void PlayClip(BackGroundMusic soundName)
    {
        if (soundName == actualClip) { return; }

        AudioClip audioClip = FindClip(soundName);

        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            actualClip = soundName;
            audioSource.Play();
        }
    }

    public IEnumerator FadeIntoAmbientMusic()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(StartFade(1, 0));

        while (audioSource.volume > 0)
        {
           yield return null;
        }

        audioSource.clip = FindClip(BackGroundMusic.AmbientMusic);
        actualClip = BackGroundMusic.AmbientMusic;
        audioSource.Play();

        StartCoroutine(StartFade(5, 0.25f));
    }

    private IEnumerator StartFade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
