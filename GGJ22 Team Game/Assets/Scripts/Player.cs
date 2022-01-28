using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // idle = 0, right = 1, left = -1
        float y = Input.GetAxisRaw("Vertical"); // idle = 0, up = 1, down = -1

        if (x != 0 || y != 0)
        {
            //make sound happen because our person is moving
        }

        UpdateMotor(new Vector3(x, y, 0));
    }
}
