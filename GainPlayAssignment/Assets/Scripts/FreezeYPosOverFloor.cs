using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeYPosOverFloor : MonoBehaviour
{
    [SerializeField] Rigidbody body;

    [SerializeField] float floorDistance = 1.5f;

    RigidbodyConstraints baseConstraints;

    // Start is called before the first frame update
    void Start()
    {
        baseConstraints = body.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = LayerMask.GetMask(Constants.FLOOR_LAYER_NAME);

        bool aboveFloor = Physics.Raycast(transform.position, Vector3.down, floorDistance, layerMask);
        Debug.Log(name + " " + aboveFloor);
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
