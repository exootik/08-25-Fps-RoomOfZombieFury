using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Références")]
    public Transform playerTransform;

    [Header("Sensibilité et limites")]
    public float sensibility = 1f;
    public float minXAngle = -90f;
    public float maxXAngle = 90f;

    public float smoothSpeed = 10f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadSettings();
    }

    void Update()
    {
        

        float mouseX = Input.GetAxis("Mouse X") * sensibility;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility;

        rotationY += mouseX;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minXAngle, maxXAngle);

        playerTransform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SmoothSpeed"))
        {
            smoothSpeed = PlayerPrefs.GetFloat("SmoothSpeed");
        }
        if (PlayerPrefs.HasKey("Sensibility"))
        {
            sensibility = PlayerPrefs.GetFloat("Sensibility");
        }
    }
}
