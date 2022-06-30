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

    #region Line Drawing Info

    Transform lastPoints;

    List<Transform> points = new List<Transform>();

    int clickedPointIndex = 0;

    Vector3 startPos, endPos;

    bool lineIsDrawing = false;

    float startTime;
    #endregion

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.positionCount = 1;
    }
    
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

            lineRenderer.SetPosition(clickedPointIndex - 1, startPos);
        }
        while (startPos != endPos)
        {
            lineIsDrawing = true;

            float t = (Time.time - startTime) / animationDuration;
            var newPos = Vector3.Lerp(startPos, endPos, t);

            lineRenderer.SetPosition(clickedPointIndex, newPos);

            if (newPos == endPos)
            {
                if (clickedPointIndex + 1 < pointListLength)
                {
                    clickedPointIndex++;
                    lineRenderer.positionCount++;
                    
                    startPos = newPos;
                    endPos = points[clickedPointIndex].position;
                }
                else if (clickedPointIndex + 1 == pointListLength)
                {
                    clickedPointIndex++;
                    lineRenderer.positionCount++;

                    startPos = newPos;
                    endPos = points[0].position;
                }
                else
                {
                    break;
                }

                startTime = Time.time;
                
            }

            yield return null;
        }
        lineIsDrawing = false;
    }
}
