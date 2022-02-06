using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    
    protected override void OnCollect()
    {
        base.OnCollect();
        GameManager2.instance.numArtifacts = 1;
        GameManager.instance.ShowText("Key Get!", 40, Color.green, transform.position, Vector3.up * 25, 0.5f, 1);
        Destroy(gameObject);
    }
}
