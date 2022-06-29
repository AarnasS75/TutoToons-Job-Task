using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private float animationDuration = 3f;

    private LineRenderer lineRenderer;

    private Vector3[] linePoints;
    private int pointsCount;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = spawner.points.Count;
        pointsCount = lineRenderer.positionCount;
        linePoints = new Vector3[pointsCount];

        GameObject[] pointsArray = spawner.points.ToArray();

        for (int i = 0; i < pointsCount; i++)
        {
            linePoints[i] = pointsArray[i].transform.position;
            
        }
        lineRenderer.SetPositions(linePoints);
    }
    public IEnumerator AnimateLine()
    {
        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;

            Vector3 startPos = linePoints[i];
            Vector3 endPos = linePoints[i+1];

            Vector3 pos = startPos;
            while (pos != endPos)
            {
                float t = (Time.time - startTime) / animationDuration;
                pos = Vector3.Lerp(startPos, endPos, t);
                for (int j = i+1; j < pointsCount; j++)
                {
                    lineRenderer.SetPosition(j, pos);
                }
                yield return null;
            }
        }
    }

    //public Transform lastPoints;
    //List<Transform> points = new List<Transform>();    
    //void MakeLine(Transform finalPoint)
    //{
    //    if (lastPoints == null)
    //    {
    //        lastPoints = finalPoint;
    //        points.Add(lastPoints);
    //    }
    //    else
    //    {
    //        points.Add(finalPoint);
    //        lineRenderer.enabled = true;
    //        SetupLine();
    //    }
    //}

    //private void SetupLine()
    //{
    //    int pointLength = points.Count;
    //    lineRenderer.positionCount = pointLength;

    //    for (int i = 0; i < pointLength; i++)
    //    {
    //        lineRenderer.SetPosition(i, points[i].position);
    //    }
    //}
}
