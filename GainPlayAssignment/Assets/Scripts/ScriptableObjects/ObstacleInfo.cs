using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleInfo", menuName = "GPA/ObstacleInfo")]
public class ObstacleInfo : ScriptableObject
{
    public ObstacleType ObstacleType;
    public Material ColorMaterial;
    public bool PushableByPlayer;
    public bool PushableByObstacles;
}
