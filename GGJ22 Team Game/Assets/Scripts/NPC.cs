using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
// Chat icon references
    private SpriteRenderer iconSprite;
    private bool collidingPlayer;
    
    // Dialogue references
    public string[] dialogueNPC;

    public Dialogue dialogueManager;

    public ContactFilter2D filter; // filter for knowing what exactly you should be colliding with
    private BoxCollider2D boxCollider; // This should just be on the assigned object to detect said collisions
    private Collider2D[] hits = new Collider2D[10]; // Array containing data of what exactly did you hit during this frame

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        iconSprite = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        iconSprite.enabled = false;
        collidingPlayer = false;
        
    }

    private void Update()
    {
        collidingPlayer = false;
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

        if (collidingPlayer)
            iconSprite.enabled = true;
        else   
            iconSprite.enabled = false;

    }

    private void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            collidingPlayer = true;
            iconSprite.enabled = true;
            if(!GameManager.instance.player.inCoversation && Input.GetKeyDown(KeyCode.R))
            {
                // We don't do this anymore!!!! NPCs now use the dialogue system box and each NPC will have a list of dialogue statements!
                //GameManager.instance.ShowText(dialogueNPC[progress], 35, Color.white, transform.position + (new Vector3(0.0f, 0.2f, 0.0f)), Vector3.zero, 6.5f, 0);
                dialogueManager.sentences = dialogueNPC;
                dialogueManager.StartTaling();
            }
        }
    }
}
