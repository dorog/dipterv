using System;
using UnityEngine;

public class PatternDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                Vector3 position = Stoled();
                position.z = transform.position.z;

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
            }
            catch (Exception) { }
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
}
