using System.Collections.Generic;
using UnityEngine;

public class SpellGuideDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float minDistance = 20f;

    private void Start()
    {
        /*List<SpellPatternPoint> spellPatternPoints = new List<SpellPatternPoint>() 
        {
            new SpellPatternPoint(0, new Vector2(0, 0)),
            new SpellPatternPoint(0, new Vector2(0, 100)),
            new SpellPatternPoint(0, new Vector2(100, 0)),
            new SpellPatternPoint(0, new Vector2(0, -100)),
            new SpellPatternPoint(0, new Vector2(-100, 0)),
            new SpellPatternPoint(0, new Vector2(0, 0)),
        };

        DrawGuide(spellPatternPoints, 10, 0.005f);*/
    }

    public void DrawGuide(List<SpellPatternPoint> spellPatternPoints, float width = 10, float scale = 0.005f)
    {
        lineRenderer.startWidth = width * scale;
        lineRenderer.endWidth = width * scale;

        lineRenderer.positionCount = spellPatternPoints.Count;

        Vector3 previousPoint = Vector3.zero;
        int extraPoint = 0;
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            Vector3 point = transform.position + new Vector3(spellPatternPoints[i].Point.x, spellPatternPoints[i].Point.y) * scale;
            if (i != 0)
            {
                float distance = (point - previousPoint).magnitude;

                int extra = Mathf.FloorToInt(distance / minDistance * scale);

                Vector3 directionVector = (point - previousPoint).normalized * minDistance * scale;
                for (int j = 0; j < extra; j++)
                {
                    lineRenderer.positionCount++;
                    Vector3 exP = previousPoint + directionVector * (j + 1);
                    lineRenderer.SetPosition(i + extraPoint, exP);
                    extraPoint++;
                }
            }
            lineRenderer.SetPosition(i + extraPoint, point);
            previousPoint = point;
        }
    }

    public void ClearGuide()
    {
        lineRenderer.positionCount = 0;
    }
}
