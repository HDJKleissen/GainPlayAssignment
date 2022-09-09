using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.enabled)
            {
                other.GetComponent<PlayerController>().Kill();
            }
        }
    }
}
