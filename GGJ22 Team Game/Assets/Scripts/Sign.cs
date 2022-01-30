using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Collidable
{
    private string signDialogue;

    // Message buffer
    private float messageTimer;

    protected override void Start()
    {
        messageTimer = 15.0f;
        base.Start();
        signDialogue = "Press SPACE to swing your sword! When <RED> press SPACE to release energy! \n Press / Hold R to interact with crates and levers! \n Press Q to cross rifts at your own risk!";
    }
    protected override void Update()
    {
        base.Update();
        messageTimer += Time.deltaTime;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(messageTimer > 15.0f)
        {
            messageTimer = 0.0f;
            GameManager.instance.ShowText(signDialogue, 35, Color.grey, transform.position + (new Vector3(0.0f, 0.4f, 0.0f)), Vector3.zero, 14.5f, 0);
        }
        
    }
}
