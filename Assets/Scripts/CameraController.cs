using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform bgImage;

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

        bgImage.localScale *= (size / startOtrho);    // Scale Background
    }
    /// <summary>
    /// JSON failo koordina?i? konvertavimas ? vietin? koordina?i? sistem?
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 ConvertToWorldSpace(Vector3 position)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(position);
        newPos.z = 1;
        return newPos;
    }
    /// <summary>
    /// M?stelis skai?iuojamas pagal fono paveiksliuko dyd?
    /// </summary>
    /// 
    
    public float PercentageIncreaseScale()
    {
        return bgImage.localScale.x / 2f;
    }
    /// <summary>
    /// Kameros m?stelio skai?iavimas
    /// Apgaubiami sukurti ta�kai ir pagal �ios ribos ilg? ir plot? yra pritaikoma kamera
    /// </summary>
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
