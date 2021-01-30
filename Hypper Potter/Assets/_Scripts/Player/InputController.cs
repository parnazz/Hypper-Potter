using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private FixedJoystick joystick;

    public InputController(FixedJoystick joystick)
    {
        this.joystick = joystick;
    }

    public Vector3 HandleInput()
    {
        var horizontalInput = joystick.Horizontal;
        var verticalInput = joystick.Vertical;

        return new Vector3(horizontalInput, verticalInput, 0);
    }
}
