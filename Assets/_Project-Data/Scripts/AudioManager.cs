using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sfxLibrary; 
    public Sound[] musicLibrary;
    AudioSource musicSource;

    [Range(0, 1)]
    public float sfxVolume;

    [Range (0, 1)]
    public float musicVolume;

    [Header("Mixers")]
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup musicMixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sfxLibrary)
        {
            s.source = gameObject.AddComponent<AudioSource>(); //Add an audio source to our audio manager per sound.
            //If you want to have spatial sounds, we would handle this with a new function. 

            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.maxVolume * sfxVolume;

            s.source.playOnAwake = false;
            s.source.outputAudioMixerGroup = sfxMixer;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.outputAudioMixerGroup = musicMixer;
    }

    public void PlaySFX(string name) //Can add a 'spatial' parameter, with a gameobject, if you want spatial audio.
    {
        Sound s = FindSound(sfxLibrary, name);

        if (s.source.isPlaying)
            return;

        s.source.volume = s.maxVolume * sfxVolume;
        s.source.Play();
    }

    Sound currentMusicTrack;
    public void PlayMusic(string name, bool fadeIn = false) //Different from SFX, since we only have 1 music audio source.
    {
        Sound s = FindSound(musicLibrary, name);

        s.source = musicSource;
        s.source.clip = s.clip;
        s.source.loop = s.loop;
        s.source.volume = s.maxVolume * musicVolume;

        if (fadeIn)
        {
            //Fade in logic - use a coroutine.
        }
        else
        {
            s.source.Play();
        }

        currentMusicTrack = s;
    }

    //Access this from a slider!
    public void AdjustMasterVolume(bool sfx, float volume)
    {
        if (sfx)
        {
            sfxVolume = volume;
            sfxMixer.audioMixer.SetFloat("sfxVolume", volume);
        }
        else
        {
            musicVolume = volume;
            musicMixer.audioMixer.SetFloat("musicVolume", volume);
            musicSource.volume = volume * currentMusicTrack.maxVolume;
        }
    }

    Sound FindSound(Sound[] library, string name) //Because we use this logic more than once, we can put it in its own return function.
    {
        Sound s = Array.Find(library, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("No sound of that name found!");
        }

        return s;
    }
}

[System.Serializable]
public class Sound
{
    public string name; //ID - How we reference our sound
    public AudioClip clip; //You can use an array for this if you have a bunch of sounds you want to randomly choose from.

    [Range(0, 1)]
    public float maxVolume; //For balancing your audio in Unity

    public bool loop;
    [HideInInspector] public AudioSource source;
}