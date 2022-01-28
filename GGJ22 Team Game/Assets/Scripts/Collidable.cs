using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An object you can collide with creating an effect, not necessarily a wall, more like a switch
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; // filter for knowing what exactly you should be colliding with
    private BoxCollider2D boxCollider; // This should just be on the assigned object to detect said collisions
    private Collider2D[] hits = new Collider2D[10]; // Array containing data of what exactly did you hit during this frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits); // Get a list of results that overlap this collider (looking for other colliders beneath or above it)
        for (int i = 0; i < hits.Length; i++)
        {
            // we hit nothing
            if(hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // The array is not cleaned up every time so we have to go ahead and do it ourselves
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }
}
