using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;


public class ControllerChecker : MonoBehaviour
{
    private string currentControlScheme = "None";

    void Start()
    {
        // Ba�lant� de�i�imlerini dinle
        InputSystem.onDeviceChange += OnDeviceChange;
        DetectCurrentControlScheme();
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
                currentControlScheme = "Gamepad";
                Debug.Log("Xbox");
            }
            else if ((firstGamepad is DualSenseGamepadHID) || (firstGamepad is DualShockGamepad))
            {
                currentControlScheme = "Gamepad";
                Debug.Log("PS");
            }

        }
        else if (Keyboard.current != null)
        {
            currentControlScheme = "Keyboard";
            Debug.Log("Sadece klavye ba�l�.");
        }
        else
        {
            currentControlScheme = "None";
            Debug.Log("Hi�bir cihaz ba�l� de�il.");
        }
    }

    private void DetectInputActivity()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (currentControlScheme != "Keyboard")
            {
                currentControlScheme = "Keyboard";
                Debug.Log("Klavye kullan�lmaya ba�land�.");
            }
        }

        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            if (currentControlScheme != "Gamepad")
            {
                currentControlScheme = "Gamepad";
                Debug.Log("Gamepad kullan�lmaya ba�land�.");
            }
        }
    }

    public string GetCurrentControlScheme()
    {
        return currentControlScheme;
    }
}
