using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    bool wasReached = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG) && !wasReached)
        {
            wasReached = true;
            other.GetComponent<PlayerController>().Kill();
        }
    }
}
