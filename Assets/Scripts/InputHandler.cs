using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action<GameObject> OnObjectSelect;

    private void Update()
    {
        CheckClickDownEvent();
    }
    /// <summary>
    /// Gaunamas taško objektas
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Tikrinama ar objektas buvo pasirinktas
    /// </summary>
    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var selectedObject = RaycastObject();

            if (selectedObject != null)
            {
                OnObjectSelect?.Invoke(selectedObject);
            }
        }
    }
}
