using UnityEngine;
using UnityEngine.XR;

public class ShieldHandler : MonoBehaviour
{
    public Player player;
    public XRNode input;
    public PlayerHealth playerHealth;

    void Update()
    {
        if(player.InBattle && !player.CanAttack())
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.gripButton, out bool pressed);

            if (pressed)
            {
                playerHealth.SetUpBlock();
            }
        }
    }
}
