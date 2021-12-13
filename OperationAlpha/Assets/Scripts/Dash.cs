using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 moveDirection;

    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
    CharacterController controller;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentDashTime = 0;
        }*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddForce(transform.right * -30, ForceMode.Impulse);
            currentDashTime = 0;
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }


    }
}
