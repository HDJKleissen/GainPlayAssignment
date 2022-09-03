using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacle)), CanEditMultipleObjects]
public class ObstacleEditor : Editor
{
    //ObstacleType selectedType;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        Obstacle[] obstacles = Array.ConvertAll(targets, target => (Obstacle)target);
        ObstacleType selectedType;

        if (obstacles.Length == 1)
        {
            selectedType = (ObstacleType)EditorGUILayout.EnumPopup("Obstacle Type", obstacles[0].ObstacleInfo.ObstacleType);
            if (obstacles[0].ObstacleInfo == null || selectedType != obstacles[0].ObstacleInfo.ObstacleType)
            {
                Undo.RecordObject(obstacles[0], "Changed ObstacleType");
                obstacles[0].SetupObstacle(selectedType);
            }
        }
        else
        {
            bool allTypesEqual = true;
            ObstacleType compareType = obstacles[0].ObstacleInfo.ObstacleType;
            foreach (Obstacle obstacle in obstacles)
            {
                if (obstacle.ObstacleInfo.ObstacleType != compareType)
                {
                    allTypesEqual = false;
                    break;
                }
            }


            if (allTypesEqual)
            {
                selectedType = (ObstacleType)EditorGUILayout.EnumPopup("Obstacle Type", compareType);
            }
            else
            {
                ObstacleType[] obstacleTypes = (ObstacleType[])Enum.GetValues(typeof(ObstacleType));
                string[] options = Array.ConvertAll(obstacleTypes, value => value.ToString()).Concat(new string[] { "---" }).ToArray<string>();

                selectedType = (ObstacleType)EditorGUILayout.Popup("Obstacle Type", options.Length - 1, options);
                if ((int)selectedType >= options.Length - 1)
                {
                    return;
                }
            }

            foreach (Obstacle obstacle in obstacles)
            {
                if (obstacle.ObstacleInfo == null || selectedType != obstacle.ObstacleInfo.ObstacleType)
                {
                    Undo.RecordObject(obstacle, "Changed ObstacleType");
                    obstacle.SetupObstacle(selectedType);
                }
            }
        }
    }
}