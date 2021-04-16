using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Joystick leftJoystick;
    public Joystick rightJoystick;

    void OnEnable()
    {
        rightJoystick.joystickDragged += LookJoystickUsed;
        leftJoystick.joystickDragged += MoveJoystickUsed;
    }

    public void OnJumpButtonPressed()
    {
       OnControlUsed.InvokeControlPressed("Jump");
    }

    public void LookJoystickUsed()
    {
        OnControlUsed.InvokeControlPressed("Look");
    }

    public void MoveJoystickUsed()
    {
        OnControlUsed.InvokeControlPressed("Move");
    }
}
