using UnityEngine;

public class WaterScale : MonoBehaviour
{
    public Transform tap;

    [Header("Tap Settings")]
    public float turnOnAngle = 45f;
    public float maxOpenAngle = 90f;

    [Header("Scale Settings")]
    public float minZScale = 0.2f;
    public float maxZScale = 1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float angle = tap.localEulerAngles.y;

        if (angle > 180)
            angle -= 360;

        // Below the opening angle, keep the stream thin
        if (angle <= turnOnAngle)
        {
            transform.localScale = new Vector3(
                originalScale.x,
                originalScale.y,
                minZScale
            );
            return;
        }

        // Convert angle to a value between 0 and 1
        float t = Mathf.InverseLerp(turnOnAngle, maxOpenAngle, angle);

        float z = Mathf.Lerp(minZScale, maxZScale, t);

        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y,
            z
        );
    }
}