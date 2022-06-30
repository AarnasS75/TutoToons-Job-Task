using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;

    [SerializeField]
    private GameObject pointPrefab;

    [SerializeField]
    CameraController cameraController;

    [SerializeField]
    private JSONREader jsonReader;

    private int count = 1;

    
    public List<GameObject> points = new List<GameObject>();
    private void Awake()
    {
        
    }
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
            if (sceneLoader.GetSceneIndex() == Array.IndexOf(jsonReader.levelList.levels, item))      // Tašk? pozicijos iš JSON failo paimamos eil?s tvarka, pagal lygio indeksa
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
        var newPos = cameraController.ConvertToWorldSpace(position);

        GameObject point = Instantiate(pointPrefab, newPos, Quaternion.identity, transform);
        
        point.GetComponent<PointScript>().AssignValue(count);
        points.Add(point);

        count++;
    }
}
