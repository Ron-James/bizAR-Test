using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadGame(){
        SceneManager.LoadScene(1);
    }
    public void LoadMenu(){
        SceneManager.LoadScene(0);
    }
    public void QuiteGame(){
        Application.Quit();
    }
}
