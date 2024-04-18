using System.Collections;
using System.Collections.Generic;
using FsLib;
using UnityEngine;

public class InputEnvironment : Environment.IInput 
{
    public bool Up()
    {
        return UnityEngine.Input.GetKey(KeyCode.W);
    }
    public bool Down()
    {
        return UnityEngine.Input.GetKey(KeyCode.S);
    }
    public bool Left()
    {
        return UnityEngine.Input.GetKey(KeyCode.A);
    }
    public bool Right()
    {
        return UnityEngine.Input.GetKey(KeyCode.D);
    }
    public bool Attack()
    {
        return UnityEngine.Input.GetKey(KeyCode.Space);
    }
}
