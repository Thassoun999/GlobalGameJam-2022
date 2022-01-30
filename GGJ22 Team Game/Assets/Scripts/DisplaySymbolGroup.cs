using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySymbolGroup : MonoBehaviour
{
    public DisplaySymbolSingular[] symbolList = new DisplaySymbolSingular[3];
    public int[] cycleCode = new int[3]; // KEEP THE CODE BETWEEN 000 AND 333 INCLUDED!!!!!!!
    public GameObject doorGroup;
    private bool doorOpened;

    private void Start()
    {
        doorOpened = false;
    }

    private void Update()
    {
        bool isCorrect = true;

        for (int i = 0; i < symbolList.Length; i++)
        {
            if(symbolList[i].currCycle != cycleCode[i])
                isCorrect = false;
        }
        
        if(isCorrect)
        {
            doorOpened = true;
        }
        else
        {
            doorOpened = false;
        }

        Activate();
    }

    private void Activate()
    {
        if (doorOpened)
        {
            doorGroup.SetActive(false);
        }
        else
        {
            doorGroup.SetActive(true);
        }
    }
}
