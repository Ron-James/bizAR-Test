using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    PlayerInputActions _playerControls;
    InputAction _mouseDelta;
    
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

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        //Initialize the new input system for the mouse delta from the Look Action
        _mouseDelta = _playerControls.Player.Look;
        _mouseDelta.Enable();
        
    }
    private void OnDisable()
    {
        //disable the input actions to prevent memory leaks
        _mouseDelta.Disable();
    }
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
        
        //Get the delta values from the mouse delta Vector given by the input system.
        Vector2 mouseDelta = _mouseDelta.ReadValue<Vector2>();

        _mouseX = mouseDelta.x;
        _mouseY = mouseDelta.y;

        //Increment the yRotation variable by the horizontal mouse position multiplied by the sensitivity and the time between frames. 
        _yRotation += _mouseX * _horizontalSensitivity * Time.deltaTime;
        _xRotation -= _mouseY * _verticalSensitivity * Time.deltaTime;

        //Clamp the xRotation so it does not exceed 90 degrees. 
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //Rotate the camera to match the xRotation and Y Rotation.
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

}
