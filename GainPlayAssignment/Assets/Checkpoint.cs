using System;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static event Action OnCheckpointReached;

    [SerializeField] Collider checkpointCollider;

    // Start is called before the first frame update
    void Start()
    {
        if(checkpointCollider == null)
        {
            checkpointCollider = GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            OnCheckpointReached?.Invoke();
            checkpointCollider.enabled = false;
        }
    }
}
