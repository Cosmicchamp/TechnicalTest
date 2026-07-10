using UnityEngine;
using System.Collections;

public class RippleController : MonoBehaviour
{
    public Transform tap;
    public ParticleSystem ripple;

    [Header("Tap Settings")]
    public float turnOnAngle = 45f;
    public float delayBeforeStart = 2f;

    [Header("Rise Settings")]
    public float riseSpeed = 0.03f;
    public float riseAmount = 0.2f;

    private Vector3 startPosition;
    private bool waiting = false;
    private bool rising = false;

    void Start()
    {
        startPosition = transform.position;
        ripple.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        float angle = tap.localEulerAngles.y;

        if (angle > 180)
            angle -= 360;

        // Tap turned on
        if (angle >= turnOnAngle)
        {
            if (!waiting && !ripple.isPlaying)
            {
                StartCoroutine(StartRipple());
            }
        }
        // Tap turned off
        else
        {
            waiting = false;
            StopAllCoroutines();

            ripple.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        // Move upwards until max height
        if (rising)
        {
            float targetY = startPosition.y + riseAmount;

            if (transform.position.y < targetY)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime;

                if (transform.position.y >= targetY)
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        targetY,
                        transform.position.z
                    );

                    rising = false;
                }
            }
        }
    }

    IEnumerator StartRipple()
    {
        waiting = true;

        yield return new WaitForSeconds(delayBeforeStart);

        waiting = false;

        ripple.Play();
        rising = true;
    }
}