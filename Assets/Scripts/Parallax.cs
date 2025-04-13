using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    public Camera targetCamera;
    public Vector2 parallaxMultiplier = new Vector2(0.05f, 0f);

    private RawImage rawImage;
    private Vector3 lastCameraPosition;
    private Vector2 currentUV;

    void Start()
    {
        rawImage = GetComponent<RawImage>();

        if (targetCamera == null)
            targetCamera = Camera.main;

        lastCameraPosition = targetCamera.transform.position;
        currentUV = rawImage.uvRect.position;
    }

    void Update()
    {
        Vector3 camDelta = targetCamera.transform.position - lastCameraPosition;

        currentUV += new Vector2(
            camDelta.x * parallaxMultiplier.x,
            camDelta.y * parallaxMultiplier.y
        );

        rawImage.uvRect = new Rect(currentUV, rawImage.uvRect.size);
        lastCameraPosition = targetCamera.transform.position;
    }
}
