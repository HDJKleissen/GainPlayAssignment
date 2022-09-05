using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Rigidbody body;
    public Collider ObstacleCollider { get; private set; }
       
    [HideInInspector]
    public ObstacleInfo ObstacleInfo;
        
    // Start is called before the first frame update
    void Start()
    {
        SetupObstacle(ObstacleInfo.ObstacleType);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            body.isKinematic = !ObstacleInfo.PushableByPlayer;
        }
        else if (collision.gameObject.CompareTag(Constants.OBSTACLE_TAG))
        {
            body.isKinematic = !ObstacleInfo.PushableByObstacles;
        }
    }

    private void OnEnable()
    {
        ObstacleManager.Instance.RegisterObstacle(this);
    }
    private void OnDisable()
    {
        ObstacleManager.Instance.DeregisterObstacle(this);
    }

    void OnValidate()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        if (body == null)
        {
            body = GetComponent<Rigidbody>();
        }
        if (ObstacleCollider == null)
        {
            ObstacleCollider = GetComponent<Collider>();
        }
        if (ObstacleInfo != null)
        {
            SetupObstacle(ObstacleInfo.ObstacleType);
        }
    }

    public void SetupObstacle(ObstacleType newType)
    {
        ObstacleInfo = ResourceLoader.GetResource<ObstacleInfo>(newType.ToString());
        meshRenderer.material = ObstacleInfo.ColorMaterial;
        tag = Constants.OBSTACLE_TAG;
        body.isKinematic = true;
    }
}

public enum ObstacleType
{
    Positive,
    Neutral,
    Negative,
    Fixed
}