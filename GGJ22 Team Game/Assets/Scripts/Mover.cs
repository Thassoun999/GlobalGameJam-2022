using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected float moveSpeed = 1;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    private float playerSfxTimeDelta;
    private float playerSfxWait = 0.175f;
    private bool playerIsMoving;

    private RaycastHit2D hit;

    // Animator Controller
    private Animator animController; 


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animController = GetComponent<Animator>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta -- Difference between current position and where I'm going to be
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction, whether you're going left or right (IF NOT HOLDING A CRATE)
        if (GameManager.instance.player.holdingCrate == false)
        {
            if (moveDelta.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (moveDelta.x > 0)
                transform.localScale = Vector3.one;
        }

        if(-moveDelta.x != 0 || moveDelta.y != 0)
            animController.SetBool("Walking", true);
        else
            animController.SetBool("Walking", false);


        // Trigger player footsteps if this is player movement
        if(transform.name == "Player")
        {
            if ((moveDelta.x != 0 || moveDelta.y != 0) && playerSfxTimeDelta > playerSfxWait)
            {
                playerSfxTimeDelta = playerSfxTimeDelta - playerSfxWait;

                AkSoundEngine.PostEvent("Play_Footsteps_Player", gameObject);
            }
            else if (playerSfxTimeDelta > playerSfxWait)
            {
                playerSfxTimeDelta = 0.0f;
            }
            else
            {
                playerSfxTimeDelta += Time.deltaTime;
            }
        }

        // Add push vector, if any.
        moveDelta += pushDirection;

        // Reduce the push force every frame with linear interpolation and recovery speed.
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoveryspeed); // what are we affecting, to what end point, with what modifiable value using a LERP formula
    
        // Make sure we can move in this direction by casting a box there first, if the box returns null we are free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Now we are making it move
            transform.Translate(0, moveDelta.y * Time.deltaTime * moveSpeed, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Now we are making it move

            transform.Translate(moveDelta.x * Time.deltaTime * moveSpeed, 0, 0);
        }
    }

}
