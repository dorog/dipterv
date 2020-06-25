using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    private InputDevice inputDevice;
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    public Animator animator;
    
    void Start()
    {
        TryToInit();
    }

    private void TryToInit()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
        else
        {
            Debug.LogError("There is no device with this characteristics! " + inputDeviceCharacteristics);
        }
    }

    void Update()
    {
        if (!inputDevice.isValid)
        {
            TryToInit();
        }
        else
        {
            if (inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                animator.SetFloat("Trigger", triggerValue);
            }
            else
            {
                animator.SetFloat("Trigger", 0);
            }
            if (inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                animator.SetFloat("Grip", gripValue);
            }
            else
            {
                animator.SetFloat("Grip", 0);
            }
        }
    }
}
