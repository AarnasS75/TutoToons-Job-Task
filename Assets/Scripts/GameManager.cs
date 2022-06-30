using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputHandler inputManager;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    LineAnimator lineAnimator;

    private void Start()
    {
        inputManager.OnObjectSelect += HandlePointSelect;
    }
    private void HandlePointSelect(GameObject selectedPoint)
    {
        if (spawner.points.IndexOf(selectedPoint) == 0)
        {
            selectedPoint.GetComponent<PointScript>().SelectPoint();
            spawner.points.Remove(selectedPoint);
            //lineAnimator.GetComponent<LineRenderer>().enabled = true;
            lineAnimator.MakeLine(selectedPoint.transform);
        }
    }
}
