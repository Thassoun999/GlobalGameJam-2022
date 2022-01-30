using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    private string[] dialogueNPC = new string[3];

    // This gets updated the more items that our player gets!
    private int progress;

    // Message buffer
    private float messageTimer;

    protected override void Start()
    {
        base.Start();
        messageTimer = 7.0f;
        progress = GameManager.instance.numArtifacts;
        dialogueNPC[0] = "A rift has been torn between worlds! Go WEST and find a way to heal the tear!";
        // dialogueNPC[1] = "What you have will help you survive the poisonous swamp to the EAST!! The artifact to close the rift is there!";
        dialogueNPC[1] = "You have it! Good! Now go to the rift and restore balance! But BEWARE of what may stop you!!!";
    }

    protected override void Update()
    {
        base.Update();
        messageTimer += Time.deltaTime;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(messageTimer > 7.0f)
        {
            messageTimer = 0.0f;
            GameManager.instance.ShowText(dialogueNPC[1], 35, Color.white, transform.position + (new Vector3(0.0f, 0.2f, 0.0f)), Vector3.zero, 6.5f, 0);
        }
    }
}
