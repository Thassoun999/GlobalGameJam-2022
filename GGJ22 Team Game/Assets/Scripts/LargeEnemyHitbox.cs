using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyHitbox : Collidable
{
    // Damage
    public int damagePoint = 2;
    public float pushForce = 4.0f;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            // Create a new damage object, before sending it to the player
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce,
                color = 0
            };
            AkSoundEngine.PostEvent("Play_Take_Damage_Player", gameObject);
            coll.SendMessage("ReceiveDamage", dmg, 0); // Is to be sent to the player / enemy class objects (the method ReceiveDamage needs to be implemented for this to work)
        }
    }
}
