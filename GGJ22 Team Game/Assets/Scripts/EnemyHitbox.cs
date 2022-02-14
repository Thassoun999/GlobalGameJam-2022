using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damagePoint;
    public float pushForce;

    // player being hit sfx
    private float playerSfxTimeDelta;
    private float playerSfxWait = 1.0f;

    protected override void Start()
    {
        base.Start();
        playerSfxTimeDelta = 0.0f;
    }

    protected override void Update()
    {
        base.Update();
        playerSfxTimeDelta += Time.deltaTime;

    }
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

            // TEMP Fix! See if Akwise has a "don't play this sound again until first instance finishes" thing
            if (playerSfxTimeDelta > playerSfxWait){
                AkSoundEngine.PostEvent("Play_Take_Damage_Player", gameObject);
                playerSfxTimeDelta = 0.0f;
            }
            coll.SendMessage("ReceiveDamage", dmg); // Is to be sent to the player / enemy class objects (the method ReceiveDamage needs to be implemented for this to work)
        }
    }
}
