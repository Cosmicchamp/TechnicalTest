using UnityEngine;

public class TapWaterController : MonoBehaviour
{
    public ParticleSystem waterParticles;

    public float turnOnAngle = 45f;

    private bool waterOn = false;

    void Start()
    {
        waterParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        float angle = transform.localEulerAngles.y;

        // Convert 315-360 into negative angles
        if (angle > 180)
            angle -= 360;

        if (angle >= turnOnAngle && !waterOn)
        {
            waterParticles.Play();
            waterOn = true;
        }
        else if (angle < turnOnAngle && waterOn)
        {
            waterParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            waterOn = false;
        }
    }
}