using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameCredits : MonoBehaviour
{
    private float idleCounter = 0.0f;
    private float idleTimeout = 60.0f; // minute idle timer


    private void Update()
    {

        // idle timer control
        idleCounter += Time.deltaTime;

        if (idleCounter >= idleTimeout || Input.GetKeyDown(KeyCode.Escape))
        {
            // reset game
            ResetTheGame();
        }

    }

    private void ResetTheGame()
    {
        //GameManager2.instance.numArtifacts = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
