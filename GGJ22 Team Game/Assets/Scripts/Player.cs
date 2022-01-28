using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private float moveSpeed = 1;

    // track time to play step sound
    private Vector3 lastPos;
    private float moveTimer = 0.0f;
    private float moveInterval = 0.175f;

    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta -- Difference between current position and where I'm going to be
        moveDelta = new Vector3(x, y, 0);
        lastPos = transform.position;

        bool step = false;

        // Swap sprite direction, whether you're going left or right
        if (moveDelta.x < 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // If position is changing, we're taking a step
        if (moveDelta.x != 0 || moveDelta.y != 0)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer > moveInterval)
            {
                moveTimer = moveTimer - moveInterval;
                step = true;
            }
        }
    
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

        // step has been taken, play step sound
        if(transform.position != lastPos && step)
        {
            AkSoundEngine.PostEvent("Footsteps_Player", gameObject);
        }
        
    }
}
