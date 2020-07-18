using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovementVR : MonoBehaviour
{
    public Player player;
    public XRNode input;
    private Vector2 inputDirection;
    public Rigidbody rigid;
    public float speed = 3;

    public GameObject body;

    void Update()
    {
        if (player.CanMove())
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputDirection);
        }
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0, body.transform.eulerAngles.y, 0);
        Vector3 position = transform.position + (rotation * new Vector3(inputDirection.x, 0, inputDirection.y) * Time.fixedDeltaTime * speed);
        rigid.MovePosition(position);

        inputDirection = Vector2.zero;
    }
}
