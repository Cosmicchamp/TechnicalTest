using UnityEngine;

public class SinkWater : MonoBehaviour
{
    public ParticleSystem tapWater;

    public float fillSpeed = 0.05f;

    public float maxHeight = 0.3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (tapWater.isPlaying)
        {
            if (transform.position.y < startPos.y + maxHeight)
            {
                transform.position += Vector3.up * fillSpeed * Time.deltaTime;
            }
        }
    }
}