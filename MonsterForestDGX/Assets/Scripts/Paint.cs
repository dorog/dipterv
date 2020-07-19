using System;
using UnityEngine;
using UnityEngine.XR;

public class Paint : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpellManager SpellManager;
    public Player player;

    public float scale = 0.1f;

    public MagicCircleHandler magicCircleHandler;

    public XRNode input;

    public Transform hand;

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(input);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out bool inputDirection);

        if (inputDirection)
        {
            try
            {
                Transform flat = magicCircleHandler.GetTransform();

                Vector3 relativPosition = magicCircleHandler.GetPosition() - flat.position;
                Vector3 projected = Vector3.ProjectOnPlane(relativPosition, flat.forward);
                Vector3 flattenedVector = flat.position + projected;

                Vector2 guess = GetGuess(flattenedVector, flat) * 200;

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, flattenedVector);
                SpellManager.Guess(guess, player.CanAttack());

            }
            catch (Exception) { }
        }
        else
        {
            CheckResult();
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

    private void CheckResult()
    {
        if (lineRenderer.positionCount != 0)
        {
            lineRenderer.positionCount = 0;
            SpellResult spellResult = SpellManager.GetSpell(player.CanAttack());
            if (spellResult == null)
            {
                Debug.Log("Null");
            }
            else
            {
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
