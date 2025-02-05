using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setcam : MonoBehaviour
{
    public float farClipInsideTrigger = 50f; // Far clip value when inside the trigger
    public float farClipOutsideTrigger = 120f; // Far clip value when outside the trigger

    [SerializeField] Camera mainCamera;

   

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the camera (or its parent object)
        if (other.CompareTag("Player"))
        {
            // Change the camera's far clipping plane
            mainCamera.farClipPlane = farClipInsideTrigger;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the camera (or its parent object)
        if (other.CompareTag("Player"))
        {
            // Change the camera's far clipping plane back to the original value
            mainCamera.farClipPlane = farClipOutsideTrigger;
        }
    }
}
