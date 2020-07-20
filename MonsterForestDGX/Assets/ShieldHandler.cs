using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class ShieldHandler : MonoBehaviour
{
    public Player player;
    public XRNode input;
    public PlayerHealth playerHealth;

    public Text feedback;

    public Transform hand;

    public float continuesDamageShieldMinAngle = 0;
    public float continuesDamageShieldMaxAngle = 25;
    public TimeDamageBlock continouesTimeDamageBlock;

    public float simpleDamageShieldMinAngle = 65;
    public float simpleDamageShieldMaxAngle = 90;
    public TimeDamageBlock simpleTimeDamageBlock;

    void Update()
    {
        if(player.InBattle && !player.CanAttack())
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.gripButton, out bool pressed);

            float angle = Vector3.Angle(hand.forward, Vector3.up);
            //feedback.text = "" + angle;
            //feedback.text = "" + angle;

            if (pressed)
            {
                if(angle <= simpleDamageShieldMaxAngle && angle >= simpleDamageShieldMinAngle)
                {
                    playerHealth.timeDamageBlock = simpleTimeDamageBlock;
                    playerHealth.SetDamageBlock();
                }
                else if(angle <= continuesDamageShieldMaxAngle && angle >= continuesDamageShieldMinAngle)
                {
                    playerHealth.timeDamageBlock = continouesTimeDamageBlock;
                    playerHealth.SetDamageBlock();
                }
            }
        }
    }
}
