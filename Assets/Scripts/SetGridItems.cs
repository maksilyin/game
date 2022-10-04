using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGridItems : MonoBehaviour
{
    private List<Sprite> spriteList;
    int cellSetted = 0;
    void Start()
    {
        spriteList = GameManager.instance.Images;
        int maxCount = GameManager.instance.GetMaxSpriteCount();
        while (cellSetted < transform.childCount)
        {

            for (int i = 0; i < maxCount; i++)
            {
                SetType(i, transform.childCount, 1);
                if (cellSetted == transform.childCount) break;
            }
        }
        GameManager.instance.CeilCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetType (int type, int ceilCount, int num)
    {
        int ceilNum = Random.Range(0, ceilCount);
        GridItem cell = transform.GetChild(ceilNum).GetComponent<GridItem>();
        if (cell.Type == -1)
        {
            cell.Type = type;
            cell.transform.GetChild(0).GetComponent<Image>().sprite = spriteList[type];
            cellSetted++;
            if (num<2)
            {
                SetType(type, ceilCount, 2);
            }
        }
        else SetType(type, ceilCount,num);
        
    }
}
