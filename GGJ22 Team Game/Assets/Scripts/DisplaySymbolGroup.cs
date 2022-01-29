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
        if(symbolList[0].currCycle == cycleCode[0] && symbolList[1].currCycle == cycleCode[1] && symbolList[2].currCycle == cycleCode[2])
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
