using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbleHealthPickup : Collectable
{
    protected override void OnCollect()
    {
        base.OnCollect();
        GameManager.instance.player.hitpoint = GameManager.instance.player.maxHitpoint;
        GameManager.instance.ShowText("Max HP!", 40, Color.green, transform.position, Vector3.up * 25, 0.5f, 1);
        AkSoundEngine.PostEvent("Play_Pickup_Health_Player", gameObject);
        Destroy(gameObject);
    }
}
