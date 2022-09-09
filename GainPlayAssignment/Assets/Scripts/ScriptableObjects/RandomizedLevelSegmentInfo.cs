using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomizedLevelSegmentInfo", menuName = "GPA/RandomizedLevelSegmentInfo")]
public class RandomizedLevelSegmentInfo : LevelSegmentInfo
{
    public List<GameObject> ObstaclePrefabs;
}
