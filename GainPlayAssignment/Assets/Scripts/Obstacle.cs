using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    Rigidbody body;

    [HideInInspector]
    public ObstacleInfo ObstacleInfo;

    void Awake()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        if (body == null)
        {
            body = GetComponent<Rigidbody>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupObstacle(ObstacleInfo.ObstacleType);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (body.mass != 100 && collision.gameObject.CompareTag("Player") && !ObstacleInfo.PushableByPlayer || collision.gameObject.CompareTag("Obstacle") && !ObstacleInfo.PushableByObstacles)
        {
            body.mass = 100;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (body.mass != 0.2f && collision.gameObject.CompareTag("Player") && ObstacleInfo.PushableByPlayer || collision.gameObject.CompareTag("Obstacle") && ObstacleInfo.PushableByObstacles)
        {
            body.mass = 0.2f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            body.mass = 100;
        }
    }
    
    void OnValidate()
    {
        if (ObstacleInfo != null)
        {
            SetupObstacle(ObstacleInfo.ObstacleType);
        }
    }

    public void SetupObstacle(ObstacleType newType)
    {
        ObstacleInfo = ResourceLoader.GetResource<ObstacleInfo>(newType.ToString());
        meshRenderer.material = ObstacleInfo.ColorMaterial;
        body.mass = 100;
    }
}

public enum ObstacleType
{
    Positive,
    Neutral,
    Negative,
    Fixed
}