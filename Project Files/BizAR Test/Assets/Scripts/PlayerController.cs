using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("New Input System")]
    [SerializeField] PlayerInputActions _playerControls;
    private InputAction _moveInput;
    private InputAction _fire;
    [Header("Movement Constants")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _moveSpeedMultiplier = 10f;

    [Header("View Orientation")]
    [SerializeField] Transform _orientation;
    private Vector3 _moveDirection;
    private Rigidbody _rigidBody;
    private Vector3 _startPosition;

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        //Initialize the new input system for movement (WASD)
        _moveInput = _playerControls.Player.Move;
        _moveInput.Enable();

        //Initialize the input action for fire (left click)
        _fire = _playerControls.Player.Fire;
        _fire.Enable();
        _fire.performed += Fire;
    }
    private void OnDisable()
    {
        //disable the input actions to prevent memory leaks
        _moveInput.Disable();
        _fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialise and freeze the rigidbody to prevent unwanted rotation due to physics
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;
        _startPosition = transform.position;

    }


    // Update is called once per frame
    private void Update()
    {
        ReadMovementInput();
    }
    private void FixedUpdate()
    {
        //Check to see if the player is registering any movement input.
        //If there is movemnt input, call the move() function to apply force.
        bool isRegisteringMovementInput = !_moveDirection.Equals(Vector3.zero);
        if (isRegisteringMovementInput)
        {
            Move();
        }
        



    }

    private void ReadMovementInput()
    {
        //Read the players current input direction for movement from the _moveInput input action.
        //Set the _moveDirection varible to match the inputMagnitude in magnitude but maintain it's relative direction  
        Vector2 inputMagnitude = _moveInput.ReadValue<Vector2>();
        _moveDirection = (_orientation.transform.forward * inputMagnitude.y) + (_orientation.transform.right * inputMagnitude.x);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        //Draw a raycast from the center of camera in a forward direction.
        //If the raycast registers a hit, see if the hit collider contains the TextureSwitcher script.
        bool hitTextureSwitcherObject = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit) && hit.collider.GetComponent<TextureSwitcher>() != null;
        if (hitTextureSwitcherObject)
        {
            //Grab the TextureSwitcher component from the gameObject hit by the raycast.
            //Call the SwitchTexture() method which changes the albedo texture attached to the object's material.
            TextureSwitcher item = hit.collider.GetComponent<TextureSwitcher>();
            item.SwitchTexture();
        }
        //Debug.Log("Fire");
    }




    private void Move()
    {
        //Apply a force to the player rigidbody to accelarate it in the current input move direction.
        //Scale the moveDirection vector by the _moveSpeed value to control the speed.
        _rigidBody.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag){
            case "Respawn":
            //Reset the player's position if they fall off the platform.
                transform.position = _startPosition;
            break;
        }
    }



}
