using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{
    [SerializeField]
    private float animationDuration = 3f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    IEnumerator AnimateLine()
    {
        float startTime = Time.time;

        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 endPos = lineRenderer.GetPosition(1);

        Vector3 pos = startPos;
        while (pos != endPos)
        {
            float t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPos, endPos, t);
            lineRenderer.SetPosition(1, pos);
            yield return null;
        }
    }
}
