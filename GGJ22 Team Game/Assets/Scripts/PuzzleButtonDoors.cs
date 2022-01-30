using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonDoors : Collidable
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
        base.Update();
        Activate();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                pressed = !pressed;
            }
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
}
