using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : Pickup
{
    public override void OnPickup()
    {
        foreach(Obstacle obstacle in ObstacleManager.Instance.GetObstaclesInView())
        {
            obstacle.SetupObstacle(ObstacleType.Positive);
        }
    }
}
