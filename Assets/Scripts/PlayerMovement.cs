using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;

    private Rigidbody rb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void StopRotating()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    private void RotateRight()
    {
        AppliedRotation(rotateSpeed);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }
    private void RotateLeft()
    {
        AppliedRotation(-rotateSpeed);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    public void AppliedRotation(float speedOfRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * speedOfRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
