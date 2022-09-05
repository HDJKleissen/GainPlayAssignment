using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleManager : UnitySingleton<ObstacleManager>
{
    HashSet<Obstacle> obstacles = new HashSet<Obstacle>();

    public void RegisterObstacle(Obstacle obstacle)
    {
        if (!obstacles.Contains(obstacle))
        {
            obstacles.Add(obstacle);
        }
    }
    public void DeregisterObstacle(Obstacle obstacle)
    {
        if (obstacles.Contains(obstacle))
        {
            obstacles.Remove(obstacle);
        }
    }

    public HashSet<Obstacle> GetObstaclesInView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        HashSet<Obstacle> obstaclesInView = new HashSet<Obstacle>();

        foreach (Obstacle obstacle in obstacles)
        {
            if (GeometryUtility.TestPlanesAABB(planes, obstacle.ObstacleCollider.bounds))
            {
                obstaclesInView.Add(obstacle);
            }
        }

        return obstaclesInView;
    }
}
