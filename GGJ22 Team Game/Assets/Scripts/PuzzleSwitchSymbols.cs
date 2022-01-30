using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchSymbols : Collidable
{
    // Connection to DoorGroup
    public DisplaySymbolSingular symbolDisplay;
    private bool pressed;
    private float symbolChangeTime;

    // For changing the pressureplate art depending on "pressed" bool
    public Sprite on;
    public Sprite off;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        pressed = false;
        symbolChangeTime = 0.0f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = off;
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
            spriteRenderer.sprite = on;
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
