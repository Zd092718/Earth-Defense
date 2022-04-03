using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based on player input")]
    [SerializeField] float controlSpeed = 20f;
    [Tooltip("Range of ship movement along X axis")]
    [SerializeField] float xRange = 15f;
    [Tooltip("Range of ship movement along Y axis")]
    [SerializeField] float yRange = 10f;


    [Header("Screen position based tuning")]
    [Tooltip("Factor for pitch position")]
    [SerializeField] float positionPitchFactor = -3f;
    [Tooltip("Factor for yaw position")]
    [SerializeField] float positionYawFactor = 2f; 
    
    [Header("Player input based tuning")]
    [Tooltip("Factor for pitch control")]
    [SerializeField] float controlPitchFactor = -15f;
    [Tooltip("Factor for roll control")]
    [SerializeField] float controlRollFactor = -30f;

    [Header("Laser Array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;
    float xThrow, yThrow;
    
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring() 
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        } else 
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
