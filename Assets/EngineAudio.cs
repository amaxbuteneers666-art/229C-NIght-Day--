using System.Collections;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    public AudioSource idleSound;
    public AudioSource runningSound;
    public AudioSource reverseSound;
    public AudioSource startingSound;

    public float idleMaxVolume = 0.4f;

    public float runningMaxVolume = 1f;
    public float runningMaxPitch = 2f;

    public float reverseMaxVolume = 0.7f;
    public float reverseMaxPitch = 1.5f;

    public float limiterEngage = 0.8f;
    public float limiterStrength = 0.1f;
    public float limiterFrequency = 6f;

    private float speedRatio;
    private float revLimiter;

    private CarController carController;
    private Rigidbody rb;

    public bool isEngineRunning = false;

    void Start()
    {
        carController = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();

        idleSound.volume = 0;
        runningSound.volume = 0;
        reverseSound.volume = 0;

        idleSound.loop = true;
        runningSound.loop = true;
        reverseSound.loop = true;
    }

    void Update()
    {
        if (!carController) return;

        speedRatio = Mathf.Abs(carController.GetSpeedRatio());

        UpdateLimiter();

        float speedDirection = GetMovementDirection();

        if (isEngineRunning)
        {
            UpdateEngineSounds(speedDirection);
        }
        else
        {
            idleSound.volume = 0;
            runningSound.volume = 0;
            reverseSound.volume = 0;
        }
    }

    float GetMovementDirection()
    {
        float direction = Vector3.Dot(transform.forward, rb.linearVelocity);

        if (direction > 1f) return 1f;
        if (direction < -1f) return -1f;

        return 0f;
    }

    void UpdateLimiter()
    {
        if (speedRatio > limiterEngage)
        {
            revLimiter = Mathf.Sin(Time.time * limiterFrequency) *
                         limiterStrength *
                         (speedRatio - limiterEngage);
        }
        else
        {
            revLimiter = 0;
        }
    }

    void UpdateEngineSounds(float speedSign)
    {
        float idleTarget = Mathf.Lerp(0.1f, idleMaxVolume, speedRatio);

        idleSound.volume = Mathf.Lerp(idleSound.volume, idleTarget, 3f * Time.deltaTime);

        if (speedSign >= 0)
        {
            reverseSound.volume = 0;

            float volumeTarget = Mathf.Lerp(0.3f, runningMaxVolume, speedRatio);
            runningSound.volume = Mathf.Lerp(runningSound.volume, volumeTarget, 3f * Time.deltaTime);

            float pitchTarget = Mathf.Lerp(0.6f, runningMaxPitch, speedRatio) + revLimiter;

            runningSound.pitch = Mathf.Lerp(runningSound.pitch, pitchTarget, 5f * Time.deltaTime);
        }
        else
        {
            runningSound.volume = 0;

            float volumeTarget = Mathf.Lerp(0.2f, reverseMaxVolume, speedRatio);
            reverseSound.volume = Mathf.Lerp(reverseSound.volume, volumeTarget, 3f * Time.deltaTime);

            float pitchTarget = Mathf.Lerp(0.6f, reverseMaxPitch, speedRatio) + revLimiter;

            reverseSound.pitch = Mathf.Lerp(reverseSound.pitch, pitchTarget, 5f * Time.deltaTime);
        }
    }

    public IEnumerator StartEngine()
    {
        startingSound.Play();

        carController.isEngineRunning = 1;

        yield return new WaitForSeconds(0.8f);

        isEngineRunning = true;

        carController.isEngineRunning = 2;
    }
}