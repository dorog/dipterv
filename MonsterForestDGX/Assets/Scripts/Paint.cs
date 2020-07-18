using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpellManager SpellManager;
    public Player player;

    public float scale = 0.1f;

    public MagicCircleHandler magicCircleHandler;

    public XRNode input;

    public Transform hand;
    public Text text;

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(input);
        device.TryGetFeatureValue(CommonUsages.trigger, out float inputDirection);

        if (inputDirection > 0.2)
        {
            text.text = "Pressing...";

            lineRenderer.positionCount += 1;

            try
            {
                //Vector3 position = ScreenWithoutRay();
                /*Vector3 position = transform.position + magicCircleHandler.GetPosition();
                Debug.Log("Pos: " + position);
                position.z = transform.position.z;

                Vector3 planeNormal = magicCircleHandler.GetNormal();

                Vector3 flattenedVector = Vector3.ProjectOnPlane(magicCircleHandler.GetPosition(), planeNormal);

                float up_angle = Vector3.SignedAngle(Vector3.forward, player.transform.forward, Vector3.up);
                float left_angle = Vector3.SignedAngle(player.transform.forward, transform.forward, Vector3.left);

                //Vector3 rotatedVector = Quaternion.Euler(-left_angle, up_angle, 0) * new Vector3(position.x * scale, position.y * scale, 0);
                Vector3 rotatedVector = Quaternion.Euler(-left_angle, up_angle, 0) * new Vector3(position.x, position.y, 0);
                Vector3 playerPoint = transform.position + rotatedVector;

                //text.text += "\n" + (playerPoint);
                //text.text += "\n" + (hand.position);
                text.text += "\nFlattered: " + (flattenedVector);
                text.text += "\nNormal: " + (planeNormal);
                //lineRenderer.SetPosition(lineRenderer.positionCount - 1, hand.position);*/


                Transform flat = magicCircleHandler.GetTransform();

                Vector3 relativPosition = magicCircleHandler.GetPosition() - flat.position;
                Vector3 projected = Vector3.ProjectOnPlane(relativPosition, flat.forward);
                Vector3 flattenedVector = flat.position + projected;

                //Vector3 flattenedVector = transform.position + Vector3.ProjectOnPlane(magicCircleHandler.GetPosition(), flat.forward);

                /*float up_angle = Vector3.SignedAngle(Vector3.forward, flat.forward, Vector3.up);
                //float left_angle = Vector3.SignedAngle(flat.forward, transform.forward, Vector3.left);
                float left_angle = Vector3.SignedAngle(Vector3.forward, flat.forward, Vector3.left);
                float forward_angle = Vector3.SignedAngle(Vector3.forward, flat.right, Vector3.forward);

                Vector3 rotatedVector = Quaternion.Euler(-left_angle, up_angle, forward_angle) * new Vector3(flattenedVector.x, flattenedVector.y, flattenedVector.z);
                Vector3 playerPoint = flat.position + rotatedVector;*/

                Vector2 guess = GetGuess(flattenedVector, flat) * 200;

                text.text += "\nGuess: " + (guess);
                text.text += "\nCan: " + (player.CanAttack());
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, flattenedVector);
                SpellManager.Guess(guess, player.CanAttack());

            }
            catch (Exception) { }
        }
        else
        {
            CheckResult();

            text.text = "Nothing...";

        }
    }

    private Vector2 GetGuess(Vector3 point, Transform plane)
    {
        Vector3 relativ = point - plane.position;

        float angle = Vector3.SignedAngle(relativ, plane.up, plane.forward);
        int signalX = 1;
        if (angle < 0)
        {
            signalX = -1;
        }

        int signalY = 1;
        if (Mathf.Abs(angle) > 90)
        {
            signalY = -1;
        }

        Vector3 up = Vector3.ProjectOnPlane(relativ, plane.up);
        float x = up.magnitude * signalX;

        Vector3 right = Vector3.ProjectOnPlane(relativ, plane.right);
        float y = right.magnitude * signalY;

        return new Vector2(x, y);
    }

    private Vector3 ScreenWithoutRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        return mousePos;
    }

    private void CheckResult()
    {
        if (lineRenderer.positionCount != 0)
        {
            lineRenderer.positionCount = 0;
            SpellResult spellResult = SpellManager.GetSpell(player.CanAttack());
            if (spellResult == null)
            {
                Debug.Log("Null");
                text.text = "\nNull";
            }
            else
            {
                text.text = "\nHit";
                player.CastSpell(spellResult);
            }
            SpellManager.ResetSpells();
        }
    }

    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
        SpellManager.ResetSpells();
    }


}
