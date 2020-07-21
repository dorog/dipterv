using UnityEngine;
using UnityEngine.XR;

public class EnableShopUI : MonoBehaviour
{
    public UiShower uiShower;
    public XRNode input = XRNode.LeftHand;
    private bool inArea = false;
    private bool inShop = false;

    public GameObject notification;

    void Update()
    {
        if (inArea && !inShop)
        {

            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed);

            if (pressed)
            {
                uiShower.ShowUI(this);
                inShop = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = true;
            notification.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = false;
            notification.SetActive(false);
        }
    }

    public void ClosedUI()
    {
        inShop = false;
    }
}
