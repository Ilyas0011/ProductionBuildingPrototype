using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioService : MonoBehaviour, IInitializable
{
    private List<AudioSource> _audioSources = new List<AudioSource>();
    private Dictionary<AudioClip, AudioSource> _activeSounds = new Dictionary<AudioClip, AudioSource>();
    private AudioSource _musicSource;

    private Config _config;
    public bool IsReady { get; set; }
    public bool DontAutoInit { get; }

    public Task Init()
    {
        _config = ServiceLocator.Get<Config>(); 

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;

        PlayMusic(AudioIdentifier.Music);

        return Task.CompletedTask;
    }

    public void PlaySounds(AudioIdentifier audioIdentifier, float volume = 1f, bool loop = false)
    {
        AudioClip clip = _config.GetAudioClip(audioIdentifier);
        AudioSource source = GetAvailableAudioSource();

        source.volume = volume;
        source.clip = clip;
        source.loop = loop;
        source.Play();

        _activeSounds[clip] = source;
    }

    public void StopSound(AudioClip clip)
    {
        if(clip == null || _activeSounds.ContainsKey(clip)) return;

        AudioSource source = _activeSounds[clip];
        source.Stop();

        _activeSounds.Remove(clip);
    }

    public AudioSource GetAvailableAudioSource()
    {
        foreach (var source in _audioSources)
        {
            if (!source.isPlaying) return source;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        _audioSources.Add(newSource);
        return newSource;
    }

    public void PlayMusic(AudioIdentifier audioIdentifier, float volume = 1f, bool loop = true)
    {
        AudioClip clip = _config.GetAudioClip(audioIdentifier);

        _musicSource.clip = clip;
        _musicSource.volume = volume;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}
