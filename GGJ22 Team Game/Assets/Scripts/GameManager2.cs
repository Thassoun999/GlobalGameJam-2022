using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    public int numArtifacts; // starts at 0! Goes to 1!

    private void Awake()
    {
         if (GameManager2.instance != null) // ensuring that this remains a singleton
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        numArtifacts = 0;
    }
}
