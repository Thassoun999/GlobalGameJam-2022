using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Mover
{


    public int directionFacing;
    public bool holdingCrate = false;
    private float worldSwapTimer;
    private float darkWorldDamageTimer;
    private bool inLightWorld;
    private Vector3 savedPositionLight;
    private Vector3 savedPositionDark;
    public Vector3 sceneStartingPosition;
    public HealthBar healthBar;

    private string musicAreaName;
    static uint musicID = 0;

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

        Scene scene = SceneManager.GetActiveScene();
        setPlayerArea(scene.name);
        if(musicID == 0)
            musicID = AkSoundEngine.PostEvent("Music", gameObject);

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

                    AkSoundEngine.SetState("Music", musicAreaName + "_Dark");
                    AkSoundEngine.PostEvent("Play_Portal_To_Dark_Player", gameObject);
                }
                else
                {
                    savedPositionDark = transform.position;
                    transform.position = savedPositionLight;
                    inLightWorld = true;
                    AkSoundEngine.SetState("Music", musicAreaName + "_Light");
                    AkSoundEngine.PostEvent("Play_Portal_To_Light_Player", gameObject);
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

    public void setPlayerArea(string area)
    {
        if (area == "Area 0")
        {
            musicAreaName = "Zero";
            AkSoundEngine.SetState("Music", "Zero_Light");
        }
        else if (area == "Area 1")
        {
            musicAreaName = "Forest";
            AkSoundEngine.SetState("Music", "Forest_Light");
        }
        else
        {
            musicAreaName = "None";
            musicID = 0;
        }
    }

    protected override void Death()
    {
        hitpoint = maxHitpoint;
        transform.position = sceneStartingPosition;

    }

}
