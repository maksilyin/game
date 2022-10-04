using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = "LEVEL " + GameManager.instance.Level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
