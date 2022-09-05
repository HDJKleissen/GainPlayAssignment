using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeYPosOverFloor : MonoBehaviour
{
    [SerializeField] Rigidbody body;

    [SerializeField] float floorDistance = 1.5f;

    RigidbodyConstraints baseConstraints;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask(Constants.FLOOR_LAYER_NAME);
        baseConstraints = body.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        bool aboveFloor = Physics.Raycast(transform.position, Vector3.down, floorDistance, layerMask);
        body.constraints = aboveFloor ? baseConstraints | RigidbodyConstraints.FreezePositionY : RigidbodyConstraints.None;
    }

    void OnValidate()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody>();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.down * floorDistance);
    }
}
