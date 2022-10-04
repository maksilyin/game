using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip guess;
    public AudioClip click;
    public AudioClip gameOver;
    public AudioClip open;
    public AudioClip levelCompleted;
    public new static Audio audio;

    public void Play (AudioClip sound)
    {
        if (sound!=null && PlayerPrefs.GetInt("sound")>0)
        {
            audioSource.PlayOneShot(sound);
        }
    }
    void Start()
    {
        audio = this;
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("sound")==1)
        {
            RestartMusic();
        }
    }

    public void RestartMusic () 
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
