using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Weapon upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Cooldowns - both are tied to the same sword, will be setting priority to the projectile cooldown if it is up. Otherwise we are prioritizing the swing cooldown!
    private float swingCooldown = 0.5f;
    private float lastSwing; // Has push force and more damage
    private float projectileCooldown = 6.0f; // Has reduced damage, no push force, piercing, and range
    private float lastProjectile;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            lastSwing = Time.time;
            Swing();
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }

            // Create a new damage object that we will then send over to the fighter 
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg); // Is to be sent to the player / enemy class objects (the method ReceiveDamage needs to be implemented for this to work)
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }
}
