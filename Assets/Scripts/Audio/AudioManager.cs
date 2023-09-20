using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null) Debug.Log("Sound not found");
        else sfxSource.PlayOneShot(sound.clip);
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null) Debug.Log("Sound not found");
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
}
