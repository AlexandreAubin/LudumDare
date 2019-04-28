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

    // Start is called before the first frame update
    void Start()
    {
        musicManager = new MusicManager();
        musicManager.AudioSources =  GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
