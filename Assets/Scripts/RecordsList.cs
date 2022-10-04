using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsList : MonoBehaviour
{
    public GameObject row;
    private Record record;
    List<int[,]> list = new List<int[,]>();
    void Start()
    {
        record = (Record) WriteFile.Deserialize();
        if (record!=null)
        {
            for (int i=record.score.Count-1; i>=0; i--)
            {
                if (row != null)
                {
                    GameObject gm = Instantiate(row, transform);
                    int score = (int) record.score[i][0, 0];
                    print(score);
                    DateTime date = (DateTime) record.score[i][0, 1];
                    print(record.score[i][0, 1]);
                    DateTime d = Convert.ToDateTime(date.ToString());
                    String day;
                    String month;

                    if (d.Day < 10) day = "0" + d.Day;
                    else day = d.Day.ToString();
                    if (d.Month < 10) month = "0" + d.Month;
                    else month = d.Month.ToString();

                    gm.transform.GetChild(0).GetComponent<Text>().text = day + "." + month + "." + d.Year;
                    gm.transform.GetChild(1).GetComponent<Text>().text = score.ToString();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
