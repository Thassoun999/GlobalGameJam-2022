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
