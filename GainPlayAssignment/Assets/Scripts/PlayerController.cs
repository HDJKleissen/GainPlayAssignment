using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] float moveSpeed;

    float inputX, inputY;

    private void Awake()
    {
        if(body == null)
        {
            body = GetComponent<Rigidbody>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector3(inputX, 0, inputY) * moveSpeed);
    }
}
