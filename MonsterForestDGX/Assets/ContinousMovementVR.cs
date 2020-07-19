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
    public XRRig xrRig;

    public BoxCollider playerCollider;

    private readonly float additionalHeight = 0.01f;

    void Update()
    {
        if (player.CanMove())
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputDirection);
        }
    }

    private void ColliderFollowHeadset()
    {
        playerCollider.size = new Vector3(playerCollider.size.x, xrRig.cameraInRigSpaceHeight, playerCollider.size.z);
        Vector3 center = transform.InverseTransformPoint(xrRig.cameraGameObject.transform.position);
        playerCollider.center = new Vector3(center.x, playerCollider.size.y / 2 + additionalHeight, center.z);
    }

    private void FixedUpdate()
    {
        ColliderFollowHeadset();

        Quaternion rotation = Quaternion.Euler(0, xrRig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 position = transform.position + (rotation * new Vector3(inputDirection.x, 0, inputDirection.y) * Time.fixedDeltaTime * speed);
        rigid.MovePosition(position);

        inputDirection = Vector2.zero;
    }
}
