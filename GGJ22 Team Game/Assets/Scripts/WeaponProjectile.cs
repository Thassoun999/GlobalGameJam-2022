using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 0f;
    public float projectileSpeed = 1.0f;

    // Weapon upgrade + Transform
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    private Transform projectileTransform;
    private int playerDirectionFacing;

    // Cooldowns + list of individuals hit
    private float lifeCycle = 2.0f;
    private float lifeTimer;

    // Animator Controller
    private Animator animController;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animController = GetComponent<Animator>();
        projectileTransform = GetComponent<Transform>();
        playerDirectionFacing = GameManager.instance.player.directionFacing;
        lifeTimer = 0.0f;
    }

    protected override void Update()
    {
        base.Update();
        lifeTimer += Time.deltaTime;

        if (lifeTimer < lifeCycle)
        {
            projectileTransform.Translate(projectileSpeed * Time.deltaTime * playerDirectionFacing, 0, 0);
        }
        else
        {
            ProjectileDeath();
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
                pushForce = pushForce,
                color = 1
            };

            coll.SendMessage("ReceiveDamage", dmg); // Is to be sent to the player / enemy class objects (the method ReceiveDamage needs to be implemented for this to work)
            
        }
    }
    

    private void ProjectileDeath()
    {
        Destroy(gameObject);
    }
}
