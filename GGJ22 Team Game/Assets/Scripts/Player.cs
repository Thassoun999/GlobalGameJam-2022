using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    public int directionFacing;
    public bool holdingCrate;

    protected override void Start()
    {
        base.Start();
        directionFacing = -1;
        holdingCrate = false;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // idle = 0, right = 1, left = -1
        float y = Input.GetAxisRaw("Vertical"); // idle = 0, up = 1, down = -1

        if (x == -1)
        {
            directionFacing = -1;
        }
        else if (x == 1)
        {
            directionFacing = 1;
        }

        UpdateMotor(new Vector3(x, y, 0));
    }
}
