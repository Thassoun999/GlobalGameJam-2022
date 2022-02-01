using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchDoors : Collidable
{
    // Connection to DoorGroup
    public GameObject doorGroup;
    private bool pressed;

    // For changing the lever art depending on "pressed" bool
    public Sprite on;
    public Sprite off;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        pressed = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = off;
    }

    protected override void Update()
    {
        pressed = false;

        base.Update();
        Activate();
        pressed = false;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" || coll.tag == "PuzzleCrate")
        {
            pressed = true;
        }
    }

    private void Activate()
    {
        if (pressed)
        {
            spriteRenderer.sprite = on;
            doorGroup.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = off;
            doorGroup.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.name == "Player" || coll.collider.tag == "PuzzleCrate")
        {
            AkSoundEngine.PostEvent("Play_Switch_Flip_Env", gameObject);
        }
    }
}
