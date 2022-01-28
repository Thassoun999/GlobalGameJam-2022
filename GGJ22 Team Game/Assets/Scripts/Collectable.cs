using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object that you collide with that can also be added to an inventory, a collectable like a coin
public class Collectable : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
