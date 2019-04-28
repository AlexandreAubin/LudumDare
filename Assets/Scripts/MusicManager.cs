using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager musicManager;

    public static MusicManager GetMusicManager()
    {
        return musicManager;
    }

    public List<AudioSource> hitWallSounds;
    private int lastPlayedHitWallSounds = 0;


    public AudioSource[] AudioSources;


    public void PlaySound(string soundName)
    {
        foreach(var source in AudioSources)
        {
            if(!source.isPlaying && source.clip.name == soundName)
            {
                source.Play();
                return;
            }
        }
    }

    public void PlayHitWallSound()
    {
        hitWallSounds[lastPlayedHitWallSounds++].Play();
        if(lastPlayedHitWallSounds == hitWallSounds.Count)
        {
            lastPlayedHitWallSounds = 0;
        }
    }

    private void SetAudioSources()
    {
        musicManager.AudioSources = GetComponentsInChildren<AudioSource>();

        musicManager.hitWallSounds = new List<AudioSource>();
        foreach (var source in musicManager.AudioSources)
        {
            if (source.clip.name.Contains("Impact_mur"))
            {
                hitWallSounds.Add(source);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        musicManager = this;
        musicManager.SetAudioSources();
        SetAudioSources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
