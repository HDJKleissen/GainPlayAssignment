using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public int SegmentAmount = 10;
    public GameObject FloorSegmentPrefab;
    public GameObject CheckpointPrefab; 

    List<FixedLevelSegmentInfo> fixedLevelSegments = new List<FixedLevelSegmentInfo>();
    List<RandomizedLevelSegmentInfo> randomizedLevelSegments= new List<RandomizedLevelSegmentInfo>();

    int highestDifficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetAllLevelSegments();
        Debug.Log(fixedLevelSegments.Count);
        GameObject levelParent = new GameObject("Level Parent");

        for(int i = 0; i < SegmentAmount; i++)
        {
            GameObject floor = Instantiate(FloorSegmentPrefab, new Vector3(0,0, (i+1)*10), Quaternion.identity, levelParent.transform);
            FixedLevelSegmentInfo segmentToSpawn = SelectFixedLevelSegment(i, i > highestDifficulty);
            Instantiate(segmentToSpawn.SegmentPrefab, floor.transform);
            Instantiate(CheckpointPrefab, new Vector3(0, 0, (i + 1) * 10 + 5), Quaternion.identity, levelParent.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    FixedLevelSegmentInfo SelectFixedLevelSegment(int difficulty, bool includeLower = false)
    {
        List<FixedLevelSegmentInfo> eligibleSegments = new List<FixedLevelSegmentInfo>();

        foreach(FixedLevelSegmentInfo info in fixedLevelSegments)
        {
            if (includeLower)
            {
                if (info.Difficulty <= difficulty)
                {
                    eligibleSegments.Add(info);
                }
            }
            else
            {
                if(info.Difficulty == difficulty)
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
