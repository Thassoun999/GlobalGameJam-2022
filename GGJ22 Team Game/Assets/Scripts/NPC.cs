using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    /*
    All Dialogue
        dialogueNPC[0] = "A rift has been torn between worlds! Go WEST and find a way to heal the tear!";
        // dialogueNPC[1] = "What you have will help you survive the poisonous swamp to the EAST!! The artifact to close the rift is there!";
        dialogueNPC[1] = "You have it! Good! Now go to the rift and restore balance! But BEWARE of what may stop you!!!";

        signDialogue[0] = "Press / Hold R to interact with crates and levers!" ;
        signDialogue[1] = "Press SPACE to swing your sword! When <RED> press SPACE to release energy!";
        signDialogue[2] = "Press Q to cross rifts at your own risk, you'll be able to cross back after some time! Your locations will be saved!";
        signDialogue[3] = "Everything you kill crosses to the other side... Either vengeful or grateful...";
        signDialogue[4] = "A symbol... Maybe a good idea to remember it for something?";
        signDialogue[5] = "The door on this side does not work, you must cross over.";
        signDialogue[6] = "Hold R to grab the crate, there's a chance you can take it with you...";
        signDialogue[7] = "With this key you can seal the rift! Go back! The lever opened the door at the start!";
    */
    public string[] dialogueNPC;

    // Message buffer
    private float messageTimer;

    protected override void Start()
    {
        base.Start();
        messageTimer = 7.0f;
    }

    protected override void Update()
    {
        base.Update();
        messageTimer += Time.deltaTime;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if(messageTimer > 7.0f)
            {
                messageTimer = 0.0f;
                // We don't do this anymore!!!! NPCs now use the dialogue system box and each NPC will have a list of dialogue statements!
                //GameManager.instance.ShowText(dialogueNPC[progress], 35, Color.white, transform.position + (new Vector3(0.0f, 0.2f, 0.0f)), Vector3.zero, 6.5f, 0);
            }
        }
    }
}
