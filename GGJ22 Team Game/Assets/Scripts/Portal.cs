using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    public int sceneNumber;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player"){
            // Teleport the player
            // GameManager.instance.SaveState();
            string sceneName = sceneNames[sceneNumber];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            GameManager.instance.player.setPlayerArea(sceneName);
            Debug.Log("Scene to: " + sceneName);
        }

    }
}
