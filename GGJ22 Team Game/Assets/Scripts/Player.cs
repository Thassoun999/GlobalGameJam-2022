using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    public int directionFacing;
    public bool holdingCrate = false;
    private float worldSwapTimer;
    private bool inLightWorld;
    private Vector3 savedPositionLight;
    private Vector3 savedPositionDark;

    protected override void Start()
    {
        base.Start();
        directionFacing = -1;
        holdingCrate = false;
        worldSwapTimer = 0.0f;
        inLightWorld = true;
        savedPositionLight = transform.position;
        savedPositionDark = transform.position + new Vector3(10.0f, 0.0f, 0.0f);
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


    private void Update()
    {
        worldSwapTimer += Time.deltaTime;

        if(worldSwapTimer > 5.0f)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(inLightWorld)
                {
                    savedPositionLight = transform.position;
                    transform.position = savedPositionDark;
                    inLightWorld = false;
                }
                else
                {
                    savedPositionDark = transform.position;
                    transform.position = savedPositionLight;
                    inLightWorld = true;
                }

                worldSwapTimer = 0.0f;
            }
        }
    }
    
}
