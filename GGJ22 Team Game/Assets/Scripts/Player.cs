using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Mover
{

    // Movement References for holding / interacting with objects
    public int directionFacing;
    public bool holdingCrate = false;

    // Timers and position references to player ability of flipping between worlds (as well as bools)
    private float worldSwapTimer; 
    private float darkWorldDamageTimer;
    private bool inLightWorld;
    private Vector3 savedPositionLight;
    private Vector3 savedPositionDark;
    public Vector3 sceneStartingPosition;

    // UI References, with relation to healthbar and locking player movement when in conversation
    public HealthBar healthBar;
    public bool inCoversation;

    private string musicAreaName;
    static uint musicID = 0;

    // transport logic -- Varies from scene to scene and marks how far a player should be moving
    public Vector3 transportVec;
    
    protected override void Start()
    {
        base.Start();
        directionFacing = 1;
        holdingCrate = false;
        worldSwapTimer = 0.0f;
        darkWorldDamageTimer = 0.0f;
        inLightWorld = true;
        inCoversation = false;

        // These are important!!!! Ensure that sceneStartingPosition NEVER changes!!!
        // savedPositionLight and savedPositionDark need to be reset upon player death to that starting position!
        sceneStartingPosition = transform.position;

        savedPositionLight = sceneStartingPosition;
        savedPositionDark = sceneStartingPosition + transportVec;


        Scene scene = SceneManager.GetActiveScene();
        setPlayerArea(scene.name);
        if(musicID == 0)
            musicID = AkSoundEngine.PostEvent("Music", gameObject);

    }

    private void FixedUpdate()
    {
        // If player's in a conversation, player should not move. This turns back off on the NPC side of code!
        if (!inCoversation){
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

    private void Update()
    {
        // WorldswapTimer and darkWorldDamageTimer increment by fp/s
        worldSwapTimer += Time.deltaTime;
        darkWorldDamageTimer += Time.deltaTime;

        // After 3 seconds, you can go ahead and switch worlds (granted you're not in a conversation)
        if(worldSwapTimer > 3.0f)
        {
            if(!inCoversation){
                // When you swap:
                // 1) Your previous position gets saved (whether in light or dark)
                // 2) You get transported to the saved position of the opposite world
                // 3) The boolean for where you are updates accordingly (and if going from light to dark, dark world damage timer resets!)
                // 4) Music swap also happens
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
        }

        // If in dark world, manage timer and keep track of the damage being sent over!
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

    // Music management!
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
        // We have died! We need to reset a lot of variables!

        hitpoint = maxHitpoint; // Health back to full

        if(!inLightWorld)
        {
            // Change the player sound state! We are in the light world!
            AkSoundEngine.SetState("Music", musicAreaName + "_Light");
            inLightWorld = true; // We are in the light world when we die! SO IMPORTANT IF YOU DIE IN DARK WORLD!!! DON'T TAKE DAMGE WHEN YOU RESPAWN!
        }

        savedPositionLight = sceneStartingPosition; // Reset Q swap saved position light
        savedPositionDark = sceneStartingPosition + transportVec; // Reset Q swap saved position dark
        worldSwapTimer = 0.0f; // reset the world swap timer
        darkWorldDamageTimer = 0.0f; // reset the dark world damage timer!

        AkSoundEngine.PostEvent("Play_Portal_To_Light_Player", gameObject); // This sound effect should be a player "revive" effect, just using the portal_to_light sound as a stand-in

        directionFacing = 1; // player is back to facing in the starting position! Just as GOD INTENDED!!!

        // MINOR: CREATE A CONDITION WHERE THE PLAYER DOESN'T TAKE THE CRATE WITH THEM WHEN THEY DIE!

        transform.position = sceneStartingPosition; // Go back to scene start position -- DO THIS LAST!!! DON'T BRING CRATES WITH YOU!!! 



    }

}
