using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        HorizontalPresed();
        JumpPresed();
    }

    private void HorizontalPresed()
    {
        PlayerController.I._moveX = Input.GetAxis("Horizontal");
    }


    private void JumpPresed()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            PlayerController.I.IsJamp = true;
            return;
        }
        PlayerController.I.IsJamp = false;
    }

    private void ShootPresed()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            PlayerController.I.IsSoot = true;
            return;
        }
        PlayerController.I.IsSoot = false;
    }
}
