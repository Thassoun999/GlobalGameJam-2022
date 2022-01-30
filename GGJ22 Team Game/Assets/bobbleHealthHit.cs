using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbleHealthHit : Fighter
{
    // Hitbox (enemy weapon)
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    // transport logic
    public Vector3 transportVec;
    private SpriteRenderer spriteRenderer;
    private bool amDead;
    public Sprite deadPlant;
    public bobbleHealthPickup bobbleHealth;



    private void Start()
    {
        amDead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Death()
    {
        if(!amDead){
            amDead = true;
            spriteRenderer.sprite = deadPlant;
            Instantiate(bobbleHealth, transform.position + transportVec, Quaternion.identity);
        }

    }


}
