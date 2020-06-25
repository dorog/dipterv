using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ContinousRotationVR : MonoBehaviour
{
    public XRNode input;
    private Vector2 inputDirection;
    public Rigidbody rigid;
    public float speed = 3;

    public GameObject body;

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(input);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputDirection);
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0, body.transform.eulerAngles.y, 0);
        Vector3 angle = rotation * new Vector3(0, inputDirection.x, 0) * Time.fixedDeltaTime * speed;
        rigid.MoveRotation(Quaternion.Euler(angle));
    }
}
