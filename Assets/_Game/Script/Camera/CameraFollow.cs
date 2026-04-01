using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Vector3 offset;
    public float speed = 20;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position + offset,
            Time.unscaledDeltaTime * speed 
        );
    }
}