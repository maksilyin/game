using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.level==1)
        {
            GameManager.score = 0;
        }

        if (!PlayerPrefs.HasKey("sound"))
            PlayerPrefs.SetInt("sound", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
