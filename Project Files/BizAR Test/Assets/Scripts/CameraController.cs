using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Sensitivity Values")]
    [SerializeField] float _horizontalSensitivity;
    [SerializeField] float _verticalSensitivity;
    [Header("Player Orientation")]
    [SerializeField] Transform _orientation;
    private float _mouseX;
    private float _mouseY;
    private float _xRotation;
    private float _yRotation;
    private bool _cameraEnabled;

    public bool CameraEnabled { get => _cameraEnabled; set => _cameraEnabled = value; }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //If the cameraEnabled is true, allow the player to look using the mouse.
        if (_cameraEnabled)
        {
            ReadPlayerInput();
        }
    }

    private void ReadPlayerInput()
    {
        //Get the current coordinates of the mouse cursor.
        _mouseX = Input.GetAxisRaw("Mouse X");
        _mouseY = Input.GetAxisRaw("Mouse Y");

        //Increment the yRotation variable by the horizontal mouse position multiplied by the sensitivity and the time elapsed. 
        _yRotation += _mouseX * _horizontalSensitivity * 0.1f;
        _xRotation -= _mouseY * _verticalSensitivity * 0.1f;

        //Clamp the xRotation so it does not exceed 90 degrees. 
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //Rotate the camera to match the xRotation and Y Rotation.
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

}
