using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSegmentInfo", menuName = "GPA/LevelSegmentInfo")]
public class RandomizedLevelSegmentInfo : ScriptableObject
{
    public List<GameObject> ObstaclePrefabs;
}
