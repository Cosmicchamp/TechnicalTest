using UnityEngine;
using System.Collections;

public class TapAudioIncrease : MonoBehaviour
{
    public Transform tap;
    public AudioSource waterAudio;

    [Header("Tap Settings")]
    public float turnOnAngle = 45f;

    [Header("Timing")]
    public float startDelay = 1f;
    public float fadeDuration = 2f;

    private bool waiting = false;
    private bool playing = false;
    private Coroutine fadeRoutine;

    void Start()
    {
        waterAudio.playOnAwake = false;
        waterAudio.loop = true;
        waterAudio.volume = 0f;
    }

    void Update()
    {
        float angle = tap.localEulerAngles.y;

        if (angle > 180)
            angle -= 360;

        if (angle >= turnOnAngle)
        {
            if (!playing && !waiting)
            {
                fadeRoutine = StartCoroutine(StartAudio());
            }
        }
        else
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            waiting = false;
            playing = false;

            waterAudio.Stop();
            waterAudio.volume = 0f;
        }
    }

    IEnumerator StartAudio()
    {
        waiting = true;

        yield return new WaitForSeconds(startDelay);

        waiting = false;
        playing = true;

        waterAudio.volume = 0f;
        waterAudio.Play();

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            waterAudio.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        waterAudio.volume = 1f;
    }
}