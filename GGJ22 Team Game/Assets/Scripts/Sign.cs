using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Collidable
{
    private string[] signDialogue = new string[10];
    public int signSay;

    // Message buffer
    private float messageTimer;

    protected override void Start()
    {
        messageTimer = 15.0f;
        base.Start();
        signDialogue[0] = "Press / Hold R to interact with crates and levers!" ;
        signDialogue[1] = "Press SPACE to swing your sword! When <RED> press SPACE to release energy!";
        signDialogue[2] = "Press Q to cross rifts at your own risk, you'll be able to cross back after some time! Your locations will be saved!";
        signDialogue[3] = "Everything you kill crosses to the other side... Either vengeful or grateful...";
        signDialogue[4] = "A symbol... Maybe a good idea to remember it for something?";
        signDialogue[5] = "The door on this side does not work, you must cross over.";
        signDialogue[6] = "Hold R to grab the crate, there's a chance you can take it with you...";
        signDialogue[7] = "With this key you can seal the rift! Go back! The lever opened the door at the start!";
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
            GameManager.instance.ShowText(signDialogue[signSay], 35, Color.white, transform.position + (new Vector3(0.0f, 0.15f, 0.0f)), Vector3.zero, 14.5f, 0);
        }
        
    }
}
