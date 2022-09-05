using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    public abstract void OnPickup();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
