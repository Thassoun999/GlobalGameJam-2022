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
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            if(Input.GetKey(KeyCode.R))
            {
                if(!isHeld)
                {
                    AkSoundEngine.PostEvent("Play_Grab_Box_Player", gameObject);
                }
                isHeld = true;
            }
            else
            {
                isHeld = false;
            }
        }

        // Player shouldn't be holding on to crate when "dying" (AKA getting teleported to start)
        // Crate should drop where it is! -> See Death() function in Player.cs -> Currently leaving this bug to be solved for later!
        if (isHeld)
        {
            GameManager.instance.player.holdingCrate = true;
            transform.parent = GameManager.instance.player.transform;
        }
        else if (!isHeld)
        {
            GameManager.instance.player.holdingCrate = false;
            transform.parent = null;
        }

    }
}
