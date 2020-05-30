using System;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpellManager SpellManager;
    public Player player;

    public float scale = 0.1f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            lineRenderer.positionCount += 1;

            try
            {
                Vector3 position = ScreenWithoutRay();
                position.z = transform.position.z;

                float up_angle = Vector3.SignedAngle(Vector3.forward, player.transform.forward, Vector3.up);
                float left_angle = Vector3.SignedAngle(player.transform.forward, transform.forward, Vector3.left);

                Vector3 rotatedVector = Quaternion.Euler(-left_angle, up_angle, 0) * new Vector3(position.x * scale, position.y * scale, 0);
                Vector3 playerPoint = transform.position + rotatedVector;


                lineRenderer.SetPosition(lineRenderer.positionCount - 1, playerPoint);
                SpellManager.Guess(position * 0.00225f * 100, player.canAttack);
            }
            catch (Exception) { }
        }
        else
        {
            CheckResult();
        }
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
            SpellResult spellResult = SpellManager.GetSpell(player.canAttack);
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
