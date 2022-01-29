using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonDoors : Collidable
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
            doorGroup.SetActive(false);
        }
        else
        {
            doorGroup.SetActive(true);
        }
    }
}
