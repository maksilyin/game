using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public GameObject effect;
    private int typeItem = -1;
    Animator anim;
    bool isOpen = false;

    public int Type
    {
        set { typeItem = value; }
        get { return typeItem; }
    }

    public bool IsOpen
    {
        set { isOpen = value; }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open ()
    {
        if (GameManager.instance.Opened < 2 && !isOpen && GameManager.instance.State==0)
        {
            Audio.audio.Play(Audio.audio.open);
            anim.Play("rotate");
            isOpen = true;
            GameManager.instance.SetOpenObject(gameObject);
        }
    }

    public void PlayEffect()
    {
        if (effect!=null)
        {
            effect = Instantiate(effect, transform.position, Quaternion.identity);
            effect.transform.SetParent(transform,false);
            Invoke("DestroyEffect", 1);
        }
    }

    public void DestroyEffect()
    {
        Destroy(effect);
    }

    public void Close()
    {
        isOpen = false;
        anim.Play("BackRotate");
    }
}
