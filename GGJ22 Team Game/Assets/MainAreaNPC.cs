using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAreaNPC : Collidable
{
    public string[] dialogueNPCPre;
    public string[] dialogueNPCPost;

    public Dialogue dialogueManager;

    // This gets updated the more items that our player gets!
    private int progress;

    protected override void Start()
    {
        base.Start();
        progress = GameManager2.instance.numArtifacts;
    }

       protected override void Update()
    {
        base.Update();
    }

     protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if(progress == 0)
            {
                if(!GameManager.instance.player.inCoversation && Input.GetKeyDown(KeyCode.R))
                {
                    // We don't do this anymore!!!! NPCs now use the dialogue system box and each NPC will have a list of dialogue statements!
                    //GameManager.instance.ShowText(dialogueNPC[progress], 35, Color.white, transform.position + (new Vector3(0.0f, 0.2f, 0.0f)), Vector3.zero, 6.5f, 0);
                    dialogueManager.sentences = dialogueNPCPre;
                    dialogueManager.StartTaling();
                }
            }
            else if (progress > 0)
            {
                if(!GameManager.instance.player.inCoversation && Input.GetKeyDown(KeyCode.R))
                {
                    // We don't do this anymore!!!! NPCs now use the dialogue system box and each NPC will have a list of dialogue statements!
                    //GameManager.instance.ShowText(dialogueNPC[progress], 35, Color.white, transform.position + (new Vector3(0.0f, 0.2f, 0.0f)), Vector3.zero, 6.5f, 0);
                    dialogueManager.sentences = dialogueNPCPost;
                    dialogueManager.StartTaling();
                }
            }
        }
    }

}
