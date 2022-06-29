using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action<GameObject> OnObjectSelect;

    private void Update()
    {
        CheckClickDownEvent();
    }
    private GameObject RaycastObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Point"))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var selectedObject = RaycastObject();
            
            if(selectedObject != null)
            {
                OnObjectSelect?.Invoke(selectedObject);
            }
        }
    }
}
