using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicState : MonoBehaviour
{
    public AK.Wwise.State OnTriggerEnterState;
    public AK.Wwise.State OnTriggerExitState;

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.name == "Player")
        {
            OnTriggerEnterState.SetValue();
            Debug.Log("Player entered dark");
            //AkSoundEngine.SetState("Music", "Zero_Dark");
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if(coll.name == "Player")
        {
            Debug.Log("Player exited dark");
            OnTriggerExitState.SetValue();
        }
    }
}
