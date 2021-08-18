using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotateSpeed;
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
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            AppliedRotation(rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            AppliedRotation(-rotateSpeed);
        }
    }

    private void AppliedRotation(float speedOfRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * speedOfRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
