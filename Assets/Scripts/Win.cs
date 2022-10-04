using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    Text scoreText;
    Text timeText;
    int seconds;
    bool play = false;
    void Start()
    {
        scoreText = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        timeText = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        scoreText.text = GameManager.instance.Score.ToString();
        timeText.text = GameManager.instance.Timer.time;
        seconds = GameManager.instance.Timer.Seconds;
        Invoke("Play", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds > 0 && play)
        {
            seconds--;
            int s = 50;
            int sec = TimeSpan.FromSeconds(seconds).Seconds;
            int min = TimeSpan.FromSeconds(seconds).Minutes;
            string secStr = sec.ToString();
            string minStr = min.ToString();
            if (sec < 10) { secStr = "0" + sec; }
            timeText.text = minStr + ":" + secStr;
            GameManager.instance.SetScore(s);
            scoreText.text = GameManager.instance.Score.ToString();
            if (seconds == 0)
            {
                if (GameManager.instance.Level == 5)
                {
                    GameManager.instance.SetRecord();
                }
                else
                {
                    GameManager.instance.Level++;
                }
            }
        }

    }

    public void Play ()
    {
        play = true;
    }
    public void NextLevel()
    {
        if (seconds == 0)
        {
            if (GameManager.instance.Level < 5)
            {
                SceneManager.LoadScene("game");
            }
            else
            {
                MainMenu(); 
            }
        }
    }

    public void MainMenu()
    {
        if (seconds == 0)
        {
            if (GameManager.instance.Level==5)
            {
                GameManager.instance.ResetAll();
            }
            SceneManager.LoadScene("mainMenu");
        }
    }
}
