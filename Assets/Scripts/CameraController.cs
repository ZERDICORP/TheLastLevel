using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float cameraSpeed = 200f;
    [SerializeField] private BoxCollider2D cameraBounds;

    private Camera cam;
    private float camHeight;
    private float camWidth;

    private void Awake()
    {
        if (!playerRigidbody)
        {
            var hero = FindFirstObjectByType<Hero>();
            if (hero) playerRigidbody = hero.GetComponent<Rigidbody2D>();
        }

        cam = GetComponent<Camera>();
        cam.orthographicSize = 3f;

        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    private void Update()
    {
        if (!playerRigidbody || !cameraBounds) return;

        Vector3 targetPos = playerRigidbody.position;
        targetPos.z = -10f;
        targetPos.y += 1f;

        Bounds bounds = cameraBounds.bounds;

        float minX = bounds.min.x + camWidth;
        float maxX = bounds.max.x - camWidth;
        float minY = bounds.min.y + camHeight;
        float maxY = bounds.max.y - camHeight;

        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);

        Vector3 clampedTarget = new Vector3(clampedX, clampedY, targetPos.z);

        transform.position = Vector3.Lerp(transform.position, clampedTarget, cameraSpeed * Time.deltaTime);
    }
}
