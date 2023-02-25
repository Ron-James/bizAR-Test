using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Pause Menu Object")]
    [SerializeField] GameObject _pauseMenu;

    private PlayerInputActions _playerControls;
    private InputAction _pauseButton;
    private CameraController _playerCameraController;
    public static bool isPaused;



    private void Awake()
    {
        _playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        //Initialize the new input system for the pause button (Escape)
        _pauseButton = _playerControls.UI.Cancel;
        _pauseButton.Enable();
        _pauseButton.performed += PauseGame;
    }
    private void OnDisable()
    {
        //disable the input actions to prevent memory leaks
        _pauseButton.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerCameraController = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraController>();
        ResumeGame();
    }


    public void ResumeGame(){
        //Resume gameplay by setting timeScale to 1 and deactivating the pause menu.
        //Re-enable camera movement so the player can look.
        //Relock the cursor to the center of the screen.
        Time.timeScale = 1;
        isPaused = false;
        _pauseMenu.SetActive(false);
        _playerCameraController.CameraEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void PauseGame(InputAction.CallbackContext context){
        //If the game is already paused, resume gameplay.
        //Else, re-enable the cursor and set the timeScale to 0 so gameplay is frozen.
        //Enable the pause menu and disable the camera movement on the player's camera controller.
        if(isPaused){
            ResumeGame();
        }
        else{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _playerCameraController.CameraEnabled = false;
            isPaused = true;
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
    }
}
