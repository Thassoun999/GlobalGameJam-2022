using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySymbolSingular : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int currCycle;
    public Sprite[] spriteList = new Sprite[4];

    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        currCycle = 0;
    }
    public void Cycle()
    {
        if(currCycle < 3)
        {
            currCycle += 1;
        }
        else if(currCycle == 3)
        {
            currCycle = 0;
        }

        spriteRenderer.sprite = spriteList[currCycle];
    }
}
