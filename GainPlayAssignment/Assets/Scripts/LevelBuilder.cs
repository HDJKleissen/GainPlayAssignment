using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public int SegmentAmount = 10;
    public GameObject FloorSegmentPrefab;
    public GameObject CheckpointPrefab;
    public GameObject FinishLinePrefab;

    List<FixedLevelSegmentInfo> fixedLevelSegments = new List<FixedLevelSegmentInfo>();
    List<RandomizedLevelSegmentInfo> randomizedLevelSegments = new List<RandomizedLevelSegmentInfo>();

    int highestDifficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetAllLevelSegments();

        GameObject levelParent = new GameObject("Level Parent");
        float furthestFloorZ = 0;

        // Spawn "Tutorial"
        for(int i = 0; i <= highestDifficulty; i++)
        {
            GameObject floor = Instantiate(FloorSegmentPrefab, new Vector3(0,0, (i+1)*10), Quaternion.identity, levelParent.transform);
            FixedLevelSegmentInfo segmentToSpawn = SelectFixedLevelSegment(i);
            Instantiate(segmentToSpawn.SegmentPrefab, floor.transform);

            furthestFloorZ = floor.transform.position.z;
        }

        // Spawn randomly after that
        for (int i = 0; i < SegmentAmount; i++)
        {
            GameObject floor = Instantiate(FloorSegmentPrefab, new Vector3(0, 0, (highestDifficulty + i + 2) * 10), Quaternion.identity, levelParent.transform);
            FixedLevelSegmentInfo segmentToSpawn = SelectFixedLevelSegment(highestDifficulty, true);
            Instantiate(segmentToSpawn.SegmentPrefab, floor.transform);

            furthestFloorZ = floor.transform.position.z;
        }

        float checkPointZ = 15;
        int difficulty = 0;
        while(checkPointZ < furthestFloorZ)
        {
            Instantiate(CheckpointPrefab, new Vector3(0, 0, checkPointZ), Quaternion.identity, levelParent.transform);
            checkPointZ += 10 + difficulty;
            difficulty++;
        }

        Instantiate(FinishLinePrefab, new Vector3(0, 0, furthestFloorZ + 5), Quaternion.identity, levelParent.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    FixedLevelSegmentInfo SelectFixedLevelSegment(int difficulty, bool tutorialSegment = false)
    {
        List<FixedLevelSegmentInfo> eligibleSegments = new List<FixedLevelSegmentInfo>();

        foreach (FixedLevelSegmentInfo info in fixedLevelSegments)
        {
            if (tutorialSegment)
            {
                if (info.Difficulty <= difficulty)
                {
                    eligibleSegments.Add(info);
                }
            }
            else
            {
                if (info.Difficulty == difficulty)
                {
                    eligibleSegments.Add(info);
                }
            }
        }

        return eligibleSegments[Random.Range(0, eligibleSegments.Count)];
    }

    void GetAllLevelSegments()
    {
        fixedLevelSegments = new List<FixedLevelSegmentInfo>(ResourceLoader.GetAll<FixedLevelSegmentInfo>());
        randomizedLevelSegments = new List<RandomizedLevelSegmentInfo>(ResourceLoader.GetAll<RandomizedLevelSegmentInfo>());

        foreach (FixedLevelSegmentInfo info in fixedLevelSegments)
        {
            if (info.Difficulty > highestDifficulty)
            {
                highestDifficulty = info.Difficulty;
            }
        }
        foreach (RandomizedLevelSegmentInfo info in randomizedLevelSegments)
        {
            if (info.Difficulty > highestDifficulty)
            {
                highestDifficulty = info.Difficulty;
            }
        }
    }
}
