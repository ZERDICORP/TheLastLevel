using UnityEngine;

public class PatrolEngine : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private float speed = 2f;

    private Transform targetPoint;
    private BoxCollider2D boxCollider;

    void Start()
    {
        targetPoint = start;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Vector3 colliderOffset = (Vector3)boxCollider.offset;
        Vector3 centerPosition = transform.position + colliderOffset;

        Vector3 targetPosition = targetPoint.position - colliderOffset;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(centerPosition, targetPoint.position) < 0.01f)
            targetPoint = targetPoint == start ? end : start;
    }
}
