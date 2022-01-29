using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitchDoors : Collidable
{
    // Connection to DoorGroup
    public GameObject doorGroup;
    private bool pressed;

    protected override void Start()
    {
        base.Start();
        pressed = false;
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
            doorGroup.SetActive(false);
        }
        else
        {
            doorGroup.SetActive(true);
        }
    }
}
