using UnityEngine;
using System.Collections;

public class SinkFill : MonoBehaviour
{
    public Transform tap;

    [Header("Tap Settings")]
    public float turnOnAngle = 45f;
    public float delayBeforeRise = 2f;

    [Header("Water Settings")]
    public float riseSpeed = 0.03f;
    public float riseAmount = 0.2f;

    private Vector3 startPos;
    private MeshRenderer meshRenderer;

    private bool sequenceStarted = false;
    private bool rising = false;

    void Start()
    {
        startPos = transform.position;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;   // Hide water at the start
    }

    void Update()
    {
        float angle = tap.localEulerAngles.y;

        if (angle > 180)
            angle -= 360;

        // Start the sequence only once
        if (!sequenceStarted && angle >= turnOnAngle)
        {
            sequenceStarted = true;
            StartCoroutine(StartRising());
        }

        // Move upward until the max height
        if (rising && transform.position.y < startPos.y + riseAmount)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            // Clamp so it never goes above the max height
            if (transform.position.y > startPos.y + riseAmount)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    startPos.y + riseAmount,
                    transform.position.z
                );
            }
        }
    }

    IEnumerator StartRising()
    {
        yield return new WaitForSeconds(delayBeforeRise);

        meshRenderer.enabled = true;
        rising = true;
    }
}