using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;
    protected override void onCollide(Collider2D coll)
    {
        if (coll.name == "Player"){
            // Teleport the player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[0];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

    }
}
