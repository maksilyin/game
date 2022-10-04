using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text timerText;
    public int _timer = 120;
    private int lastSec = 0;
    private bool stopTime = false;
    public string time = "00:01";
    int timeSeconds = 0;
    public static Timer timer;
    void Awake()
    {
        timer = this;
        timeSeconds = _timer;
        timerText = transform.GetChild(0).GetComponent<Text>();
    }

    public bool IsTimeOver
    {
        get { return stopTime; }
    }

    public int Seconds
    {
        get { return timeSeconds; }
    }
    void Update()
    {
        SetTime();
    }

    void SetTime()
    {
        int sec;
        int min;
        sec = TimeSpan.FromSeconds(timeSeconds).Seconds;
        min = TimeSpan.FromSeconds(timeSeconds).Minutes;
        int secCur = DateTime.Now.Second;

        if ((lastSec != secCur) && !stopTime)
        {
            timeSeconds--;
            string secStr = sec.ToString();
            string minStr = min.ToString();
            if (sec < 10) { secStr = "0" + sec; }
            time = minStr + ":" + secStr;
            timerText.text = time;
            lastSec = secCur;
            if (timeSeconds < 0) stopTimer();
        }
    }
    public void stopTimer()
    {
        stopTime = true;
    }

}
