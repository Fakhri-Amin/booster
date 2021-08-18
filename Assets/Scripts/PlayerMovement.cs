using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustSpeed;
    [SerializeField] float rotateSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
