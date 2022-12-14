using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static event Action OnCheckpointReached;

    bool wasReached = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG) && !wasReached)
        {
            wasReached = true;
            OnCheckpointReached?.Invoke();
        }
    }
}
