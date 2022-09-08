using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    [SerializeField] Rigidbody body;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed;

    float inputX, inputY;

    private void Awake()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody>();
        }
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
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
        body.AddForce(new Vector3(inputX, 0, inputY) * moveForce);
        body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
    }

    public void Kill(Vector3 deathImpulse)
    {
        enabled = false;

        GameObject deadCopy = Instantiate(gameObject, transform.position, transform.rotation);

        Collider deadCopyCollider = deadCopy.GetComponent<Collider>();

        deadCopyCollider.enabled = false;
        StartCoroutine(CoroutineHelper.DelayOneFixedFrame(() => deadCopyCollider.enabled = true));

        deadCopy.GetComponent<Rigidbody>().AddForce(deathImpulse, ForceMode.Impulse);

        meshRenderer.enabled = false;
        body.isKinematic = true;

        OnPlayerDeath?.Invoke();
    }
}