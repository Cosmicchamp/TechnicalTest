using UnityEngine;
using System.Collections;

public class TapAudio : MonoBehaviour
{
    [Header("References")]
    public Transform tap;
    public AudioSource waterAudio;

    [Header("Tap Settings")]
    public float turnOnAngle = 45f;
    public float maxOpenAngle = 90f;
    public float startDelay = 1f;

    [Header("Volume")]
    public float minVolume = 0.1f;
    public float maxVolume = 1f;

    private bool waiting = false;

    void Start()
    {
        waterAudio.playOnAwake = false;
        waterAudio.loop = true;
        waterAudio.Stop();
    }

    void Update()
    {
        float angle = tap.localEulerAngles.y;

        if (angle > 180)
            angle -= 360;

        // Tap closed
        if (angle < turnOnAngle)
        {
            StopAllCoroutines();
            waiting = false;

            if (waterAudio.isPlaying)
                waterAudio.Stop();

            return;
        }

        // Tap opened
        if (!waterAudio.isPlaying && !waiting)
        {
            StartCoroutine(StartAudio());
        }

        // Change volume continuously
        if (waterAudio.isPlaying)
        {
            float t = Mathf.InverseLerp(turnOnAngle, maxOpenAngle, angle);
            waterAudio.volume = Mathf.Lerp(minVolume, maxVolume, t);
        }
    }

    IEnumerator StartAudio()
    {
        waiting = true;

        yield return new WaitForSeconds(startDelay);

        waiting = false;

        if (!waterAudio.isPlaying)
            waterAudio.Play();
    }
}