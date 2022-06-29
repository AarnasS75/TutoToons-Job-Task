using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputHandler inputManager;

    private void Start()
    {
        inputManager.OnObjectSelect += HandlePointSelect;
    }
    private void HandlePointSelect(GameObject selectedPoint)
    {
        selectedPoint.GetComponent<PointScript>().SelectPoint();
    }
}
