using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private float moveSpeed = 1;

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

        // Swap sprite direction, whether you're going left or right
        if (moveDelta.x < 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    
        Debug.Log(x);

        // Now we are making it move
        transform.Translate(moveDelta * Time.deltaTime * moveSpeed);
        
    }
}
