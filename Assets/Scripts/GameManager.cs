using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level = 1;
    public List<Sprite> Images;
    public int _score = 20; 
    public GameObject GameOver;
    public GameObject Win;
    public static GameManager instance;
    public static int score = 0;
    private int openned = 0;
    private int gridItemCount = 28;
    List<GameObject> openObjects = new List<GameObject>();
    int state = 0;
    new Audio audio;
    GameObject tmp;
    Timer timer;

    public Timer Timer
    {
        get { return timer; }
    }
    public int CeilCount
    {
        set { gridItemCount = value; }
    }
    public int State
    {
        get { return state; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Score
    {
        get { return score; }
    }
    public int Opened
    {
        get { return openObjects.Count; }
    }

    private void Awake()
    {
        instance = this;
        timer = Timer.timer;
    }

    void Start()
    {
        audio = Audio.audio;
    }
    void Update()
    {
        if (timer.IsTimeOver && state < 2 && tmp==null)
        {
            state = 2;
            audio.StopMusic();
            audio.Play(audio.gameOver);
            tmp=Instantiate(GameOver);
        }

        if (openned==gridItemCount && state < 2 && tmp==null)
        {
            state = 2;
            audio.StopMusic();
            audio.Play(audio.levelCompleted);
            tmp=Instantiate(Win);
        }
    }

    public int GetMaxSpriteCount ()
    {
        int result;

        switch (level)
        {
            case 1:
                result = 5;
                break;
            case 2:
                result = 7;
                break;
            case 3:
                result = 7;
                break;
            case 4:
                result = 9;
                break;
            default:
                result = 12;
                break;
        }
        return result;
    }

    public void SetOpenObject(GameObject gm)
    {
        if (openObjects.Count<2)
        {
            openObjects.Add(gm); 
            if (openObjects.Count==2)
            {
                state = 1;
                Invoke("CheckOpenItems", 1);
            }
        }
    }

    public void SetScore (int count)
    {
        score += count;
    }

    private void CheckOpenItems ()
    {
        GridItem open1 = openObjects[0].GetComponent<GridItem>();
        GridItem open2 = openObjects[1].GetComponent<GridItem>();
        if (open1.Type==open2.Type)
        {
            open1.PlayEffect();
            open2.PlayEffect();
            Success();
        }
        else
        {
            open1.Close();
            open2.Close();
            ResetOpenned(); ;
        }
    }

    private void Success()
    {
        state = 0;
        audio.Play(audio.guess);
        openObjects.Clear();
        openned += 2;
        SetScore(_score);
    }

    private void ResetOpenned ()
    {
        state = 0;
        openObjects.Clear();
    }

    public void SetRecord()
    {
        Record record;
        record = (Record)WriteFile.Deserialize();
        if (record == null)
        {
            record = new Record();
        }
        System.Object[,] data = new System.Object[1, 2];
        System.DateTime date = System.DateTime.Now;
        data[0, 0] = (System.Object)score;
        data[0, 1] =(System.Object)date;
        record.score.Add(data);
        WriteFile.SerializeAsync(record);
    }

    public void ResetAll()
    {
        score = 0;
        level = 1;
    }
}
