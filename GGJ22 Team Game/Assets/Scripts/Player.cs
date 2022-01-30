using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mover
{

    public int directionFacing;
    public bool holdingCrate = false;
    private float worldSwapTimer;
    private float darkWorldDamageTimer;
    private bool inLightWorld;
    private Vector3 savedPositionLight;
    private Vector3 savedPositionDark;
    private Vector3 sceneStartingPosition;
    public HealthBar healthBar;

    // transport logic
    public Vector3 transportVec;
    

    protected override void Start()
    {
        base.Start();
        directionFacing = 1;
        holdingCrate = false;
        worldSwapTimer = 0.0f;
        darkWorldDamageTimer = 0.0f;
        inLightWorld = true;
        savedPositionLight = transform.position;
        savedPositionDark = transform.position + transportVec;

        sceneStartingPosition = transform.position;
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
        darkWorldDamageTimer += Time.deltaTime;

        if(worldSwapTimer > 3.0f)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(inLightWorld)
                {
                    savedPositionLight = transform.position;
                    transform.position = savedPositionDark;
                    inLightWorld = false;
                    darkWorldDamageTimer = 0.0f;
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

        if(!inLightWorld)
        {
            // When in the dark world the player will progressively take damage, around 1 damage per 10 seconds of time elapsed
            if (darkWorldDamageTimer > 10.0f)
            {
                darkWorldDamageTimer = 0.0f;
                Damage dmg = new Damage
                {
                    damageAmount = 1,
                    origin = this.transform.position,
                    pushForce = 0.0f,
                };
                this.SendMessage("ReceiveDamage", dmg, 0);
            }
        }

        // Update Healthbar
        healthBar.SetHealth(hitpoint);

    }

    protected override void Death()
    {
        hitpoint = maxHitpoint;
        transform.position = sceneStartingPosition;

    }


}
