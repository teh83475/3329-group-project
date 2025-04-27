using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
 
    float xRotation = 0f;
    float YRotation = 0f;
    private Transform cameraTransform;
 
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
      cameraTransform = Camera.main.transform;
    }
 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
        xRotation -= mouseY;
 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
        YRotation += mouseX;
 
        transform.localRotation = Quaternion.Euler(0f, YRotation, 0f);              //horizontal camera movement applied to whole player
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);        //vertical camera movement applied to camera only
    }
}