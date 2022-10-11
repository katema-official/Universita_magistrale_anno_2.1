using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource BackgroundMusic;

    public AudioSource SoundEffects;

    public AudioClip[] BackgroundMusicClips;

    public AudioClip[] WinningSounds;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            BackgroundMusic.clip = BackgroundMusicClips[0];
            BackgroundMusic.Play();
        } else if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            PlayGameplayBackgroundMusic();
        }
    }

    public void PlayGameplayBackgroundMusic()
    {
        BackgroundMusic.clip = BackgroundMusicClips[1];
        BackgroundMusic.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
