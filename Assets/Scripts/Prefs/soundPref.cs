using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundPref : MonoBehaviour
{
    public Sprite offSound;
    Sprite onSound;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        onSound = image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("sound")<1)
        {
            image.sprite = offSound;
        }
        else image.sprite = onSound;
    }

    public void SetSoundPref()
    {
        if (PlayerPrefs.GetInt("sound") < 1)
        {
            PlayerPrefs.SetInt("sound", 1);
            if (Audio.audio != null)
            {
                Audio.audio.RestartMusic();
            }
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
            if (Audio.audio != null) 
            { 
                Audio.audio.StopMusic();
            }
        }
    }
}
