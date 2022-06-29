using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pointPrefab;

    [SerializeField]
    CameraController cameraController;

    [SerializeField]
    private JSONREader jsonReader;

    private int count = 1;

    [HideInInspector]
    public List<GameObject> points = new List<GameObject>();

    private void Start()
    {
        SpawnPoints();
        cameraController.SetCameraAspect();
        AdaptPointSizesToCamera();
    }
    void AdaptPointSizesToCamera()
    {
        foreach (var point in points)
        {
            point.transform.localScale = cameraController.AdaptedScale();
        }
    }
    void SpawnPoints()
    {
        foreach (var item in jsonReader.levelList.levels)
        {
            if (SceneLoader.GetSceneIndex() == Array.IndexOf(jsonReader.levelList.levels, item))
            {
                for (int i = 0; i < item.level_data.Length; i += 2)
                {
                    SpawnPoint(new Vector2(float.Parse(item.level_data[i]), float.Parse(item.level_data[i + 1])));
                }
            }
        }
    }
    void SpawnPoint(Vector2 position)
    {
        var newPos = ConvertToWorldSpace(position);

        GameObject point = Instantiate(pointPrefab, newPos, Quaternion.identity, transform);
        
        point.GetComponent<PointScript>().AssignValue(count);
        points.Add(point);
        count++;
    }
    Vector3 ConvertToWorldSpace(Vector3 position)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(position);
        newPos.z = 1;
        return newPos;
    }
}
