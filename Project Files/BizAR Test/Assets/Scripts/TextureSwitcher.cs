using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSwitcher : MonoBehaviour
{
    [Header("Textures To Switch Through")]
    [SerializeField] Texture [] _textures;
    private int _currentTexture = 0;

    MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _currentTexture = 0;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SwitchTexture(){
        //If the currentTexture value is at the end of the textures array, cycle it back to 0.
        //Else, increment the value by one.
        if(_currentTexture >= _textures.Length - 1){
            _currentTexture = 0;
        }
        else{
            _currentTexture++;
        }

        //set the mesh renderer material's albedo to the texture at position currentTexture in the textures array
        _meshRenderer.material.SetTexture("_MainTex", _textures[_currentTexture]);



    }
}
