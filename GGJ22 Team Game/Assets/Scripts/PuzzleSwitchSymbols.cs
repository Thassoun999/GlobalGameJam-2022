using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchSymbols : Collidable
{
    // Connection to DoorGroup
    public DisplaySymbolSingular symbolDisplay;
    private bool pressed;
    private float symbolChangeTime;

    protected override void Start()
    {
        base.Start();
        pressed = false;
        symbolChangeTime = 0.0f;
    }

    protected override void Update()
    {
        symbolChangeTime += Time.deltaTime;
        base.Update();

        Activate();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && symbolChangeTime > 1.0f)
        {
            symbolChangeTime = 0.0f;
            pressed = true;
        }
    }

    private void Activate()
    {
        if (pressed)
        {
            symbolDisplay.Cycle();
            pressed = false;
        }
    }

}
