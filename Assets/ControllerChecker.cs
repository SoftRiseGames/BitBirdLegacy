using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using System;

public class ControllerChecker : MonoBehaviour
{
    private string currentControlScheme = "None";
    public static bool isPS;
    public static bool isXbox;
    public static bool isKB;

    bool isKBActive;
    bool isGamepadActive;
    void Start()
    {
      
        // Ba�lant� de�i�imlerini dinle
        InputSystem.onDeviceChange += OnDeviceChange;
        DetectCurrentControlScheme();
        DetectInputActivity();
        
       
    }

    void Update()
    {
        DetectInputActivity();
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            DetectCurrentControlScheme();
        }
    }

    private void DetectCurrentControlScheme()
    {
        var gamepads = Gamepad.all;
       
        if (gamepads.Count > 0)
        {
            var firstGamepad = gamepads[gamepads.Count-1];

            if (firstGamepad is XInputController)
            {
                isXbox = true;
                isPS = false;
                isKB = false;
                currentControlScheme = "Gamepad";
                Debug.Log("Xbox");
            }
            else if ((firstGamepad is DualSenseGamepadHID) || (firstGamepad is DualShockGamepad))
            {
                isXbox = false;
                isPS = true;
                isKB = false;
                currentControlScheme = "Gamepad";
                Debug.Log("PS");
            }
        }
        else if (Keyboard.current != null)
        {
            isXbox = false;
            isPS = false;
            isKB = true;
            currentControlScheme = "Keyboard";
            Debug.Log("Sadece klavye ba�l�.");
        }
        else
        {
            currentControlScheme = "None";
            Debug.Log("Hi�bir cihaz ba�l� de�il.");
            return;
        }
    }

    private void DetectInputActivity()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (currentControlScheme != "Keyboard")
            {
                isXbox = false;
                isPS = false;
                isKB = true;
                currentControlScheme = "Keyboard";
                Debug.Log("Klavye kullan�lmaya ba�land�.");
            }
        }

        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            var gamepads = Gamepad.all;
            if (gamepads.Count > 0)
            {
                var firstGamepad = gamepads[gamepads.Count - 1];

                if (firstGamepad is XInputController)
                {
                    isXbox = true;
                    isPS = false;
                    isKB = false;
                    currentControlScheme = "Gamepad";
                    Debug.Log("Xbox kullan�lmaya ba�land�");
                }
                else if ((firstGamepad is DualSenseGamepadHID) || (firstGamepad is DualShockGamepad))
                {
                    isXbox = false;
                    isPS = true;
                    isKB = false;
                    currentControlScheme = "Gamepad";
                    Debug.Log("PS kullan�lmaya ba�land�");
                }
            }
        }
    }

    public string GetCurrentControlScheme()
    {
        return currentControlScheme;
    }
}
