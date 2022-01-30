using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.player.transform.position = transform.position;
        GameManager.instance.player.sceneStartingPosition = transform.position;
    }
}
