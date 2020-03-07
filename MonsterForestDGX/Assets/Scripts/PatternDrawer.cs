using System;
using UnityEngine;

public class PatternDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float scale = 1;
    private bool canEdit = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canEdit)
        {
            try
            {
                Vector3 position = ScreenWithoutRay();
                position *= scale;
                position.z = transform.position.z;

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
            }
            catch (Exception) { }
        }
        if(Input.GetMouseButtonDown(1))
        {
            canEdit = !canEdit;
        }
    }

    public Vector3 Stoled()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point + Vector3.up * 0.1f;
        }

        throw new Exception();
    }

    private Vector3 ScreenWithoutRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        return mousePos;
    }
}
