using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; // we need to lookAt the player
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    // Needs to be called after Update and FixedUpdate or else we get stutter
    private void LateUpdate() 
    {
        Vector3 delta = Vector3.zero;

        // If player outside camera x-bounds, have camera start following player on the x axis!
        float camDeltaX = lookAt.position.x - transform.position.x; // transform referring to the camera
        if (camDeltaX > boundX || camDeltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = camDeltaX - boundX;
            }
            else 
            {
                delta.x = camDeltaX + boundX;
            }
        }

        // If player outside camera y-bounds, have camera start following player on the y axis!
        float camDeltaY = lookAt.position.y - transform.position.y; // transform referring to the camera
        if (camDeltaY > boundY || camDeltaY < -boundY)
        {
            if(transform.position.y < lookAt.position.y)
            {
                delta.y = camDeltaY - boundY;
            }
            else 
            {
                delta.y = camDeltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
