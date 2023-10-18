using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalPresed();
        JumpPresed();
    }

    private void HorizontalPresed()
    {
        _playerController._moveX = Input.GetAxis("Horizontal");
    }


    private void JumpPresed()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerController._isJamp = true;
            return;
        }
        _playerController._isJamp = false;
    }
}
