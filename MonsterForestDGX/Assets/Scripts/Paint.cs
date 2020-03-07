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
        if (Input.GetMouseButton(0))
        {
            lineRenderer.positionCount += 1;

            try
            {
                Vector3 position = ScreenWithoutRay();
                position.z = transform.position.z;

                float angle = Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up);
                Vector3 rotatedVector = Quaternion.Euler(0, angle, 0) * new Vector3(position.x * scale, position.y * scale, 0);
                Vector3 playerPoint = transform.position + rotatedVector;

               lineRenderer.SetPosition(lineRenderer.positionCount - 1, playerPoint);
               SpellManager.Guess(position * 0.00225f * 100, player.canAttack);
            }
            catch (Exception) { }
        }
        else
        {
            if(lineRenderer.positionCount != 0)
            {
                lineRenderer.positionCount = 0;
                GameObject gameObject = SpellManager.GetSpell(player.canAttack);
                if (gameObject == null)
                {
                    Debug.Log("Null");
                }
                else
                {
                    Debug.Log(gameObject.name);
                    if (player.canAttack)
                    {
                        player.Attack(10);
                    }
                    else
                    {
                        player.Def();
                    }
                }
                SpellManager.ResetSpells();
            }
        }
    }

    private Vector3 ScreenWithoutRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        return mousePos;
    }

    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
        SpellManager.ResetSpells();
    }
}
