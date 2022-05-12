using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{

    private bool resetInitiated = false;
    private bool escPressedMidDialog = false;
    private bool keyHeld = false; // prevent esc from causing immediate restart
    private float idleCounter = 0.0f;
    private float idleTimeout = 60.0f;// 120.0f; // minute idle timer
    private float autoCounter = 0.0f;
    private float autoTimeout = 10.0f; // automatically restart game after 10 seconds of dialog being displayed

    // Dialogue references
    public string[] dialogueNPC;

    public Dialogue dialogueManager;

    private void Update()
    {
        // idle timer control
        idleCounter += Time.deltaTime;
        if (Input.anyKeyDown)
        {
            idleCounter = 0.0f; // Reset idle counter
        }

        if(!resetInitiated)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || idleCounter >= idleTimeout || escPressedMidDialog)
            {
                keyHeld = true;
                // Wait for dialog "abort" hack to make the previous dialog go away.
                if (GameManager.instance.player.inCoversation)
                {
                    escPressedMidDialog = true;
                }
                else
                {
                    escPressedMidDialog = false;
                    ShowResetDialog();
                }
            }
        }
        if(resetInitiated && GameManager.instance.player.inCoversation)
        {
            // Escape pressed while reset dialog already open
            // or the timer expired for auto-restart
            if (autoCounter >= autoTimeout || (!keyHeld && autoCounter >= 0.6f && Input.GetKeyDown(KeyCode.Escape)))
            {
                // reset game
                ResetTheGame();
            }
            else
            {
                autoCounter += Time.deltaTime;
            }
        }
        else
        {
            autoCounter = 0.0f;
            resetInitiated = false;
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            keyHeld = false; // Esc button was released
        }

    }

    private void ShowResetDialog()
    {
        resetInitiated = true;
        autoCounter = 0.0f;
        dialogueManager.sentences = dialogueNPC;
        dialogueManager.StartTaling();
    }

    private void ResetTheGame()
    {
        GameManager2.instance.numArtifacts = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
