using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePrefab;

    [Header("Audio Library")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    private List<AudioSource> sfxPool = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicDict = new Dictionary<string, AudioClip>();
        sfxDict = new Dictionary<string, AudioClip>();

        foreach (var clip in musicClips)
            musicDict[clip.name] = clip;

        foreach (var clip in sfxClips)
            sfxDict[clip.name] = clip;
    }

    // -------------------------------
    //            MUSIC
    // -------------------------------
    public void PlayMusic(string name, bool loop = true)
    {
        if (musicDict.TryGetValue(name, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
            Debug.LogWarning("Music not found: " + name);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // -------------------------------
    //            SFX
    // -------------------------------
    public void PlaySFX(string name, float volume = 1f)
    {
        if (!sfxDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }

        AudioSource src = GetAvailableSFXSource();
        src.volume = volume;
        src.clip = clip;
        src.Play();
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var src in sfxPool)
        {
            if (!src.isPlaying)
                return src;
        }

        AudioSource newSource = Instantiate(sfxSourcePrefab, transform);
        sfxPool.Add(newSource);
        return newSource;
    }

    // -------------------------------
    //            VOLUME
    // -------------------------------
    public void SetMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }

    public void SetSFXVolume(float vol)
    {
        foreach (var src in sfxPool)
            src.volume = vol;
    }
}
