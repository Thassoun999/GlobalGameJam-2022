using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullPuzzleBoxes : Collidable
{
    private bool isHeld;

    protected override void Start()
    {
        base.Start();
        isHeld = false;
    }

    protected override void Update()
    {
        base.Update();

        if (isHeld)
        {
            GameManager.instance.player.holdingCrate = true;
            transform.parent = GameManager.instance.player.transform;
        }
        else 
        {
            GameManager.instance.player.holdingCrate = false;
            transform.parent = null;
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            if(Input.GetKey(KeyCode.R))
            {
                isHeld = true;
            }
            else
            {
                isHeld = false;
            }
        }

    }
}
