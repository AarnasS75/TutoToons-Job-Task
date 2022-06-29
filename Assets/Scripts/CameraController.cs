using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;

    [SerializeField]
    private float buffer = 2f;

    Camera cam;

    private float startOtrho;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        startOtrho = cam.orthographicSize;
    }

    public void SetCameraAspect()
    {
        var (center, size) = CalculateOrthoSize();
        cam.transform.position = center;
        cam.orthographicSize = size;

        transform.localScale *= (size / startOtrho);    // Scale Background
    }
    public Vector3 AdaptedScale()
    {
        if(transform.localScale.x <= 1)
        {
            return Vector3.one - transform.localScale;
        }
        return transform.localScale - Vector3.one;
    }
    private (Vector3 center, float size) CalculateOrthoSize()
    {
        var bounds = new Bounds();

        foreach (var collider in spawner.points) 
        {
            bounds.Encapsulate(collider.GetComponent<CircleCollider2D>().bounds);
        }

        bounds.Expand(buffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * cam.pixelHeight / cam.pixelWidth;

        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}
