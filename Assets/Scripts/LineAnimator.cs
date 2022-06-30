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

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 1;
    }
    Transform lastPoints;
    List<Transform> points = new List<Transform>();
    public void MakeLine(Transform finalPoint)
    {
        if (lastPoints == null)
        {
            lastPoints = finalPoint;
            points.Add(lastPoints);
        }
        else
        {
            points.Add(finalPoint);
            lineRenderer.enabled = true;
            StartCoroutine(SetupLine());
        }
    }

    int clickedPointIndex = 0;
    Vector3 startPos, endPos, pos;
    bool lineIsDrawing = false;
    float startTime;
    private IEnumerator SetupLine()
    {
        int pointListLength = points.Count;
        
        if (!lineIsDrawing)
        {
            clickedPointIndex++;
            lineRenderer.positionCount++;

            startTime = Time.time;

            startPos = points[clickedPointIndex - 1].position;
            endPos = points[clickedPointIndex].position;

            pos = startPos;

            lineRenderer.SetPosition(clickedPointIndex - 1, pos);
        }
        while (pos != endPos)
        {
            lineIsDrawing = true;

            float t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPos, endPos, t);

            lineRenderer.SetPosition(clickedPointIndex, pos);

            if (pos == endPos && clickedPointIndex + 1 != pointListLength)
            {
                startTime = Time.time;

                lineRenderer.positionCount++;

                clickedPointIndex++;

                endPos = points[clickedPointIndex].position;

               lineRenderer.SetPosition(clickedPointIndex, pos);
            }

            yield return null;
        }
        lineIsDrawing = false;
    }
}
