using System.Collections.Generic;
using UnityEngine;

public class SpellPattern : MonoBehaviour
{
    public float maxPoints = 0;
    public float correct = 0;
    public SpellPatternPoints SpellPatternPoints;
    public GameObject Point;
    public List<SpellPoint> points = new List<SpellPoint>();

    private int step = 100;

    public void DrawPoints()
    {
        List<Vector3> spellPoints = SpellPatternPoints.GetPoints();

        Instantiate(spellPoints[0].x, spellPoints[0].y);
        for (int i = 1; i < spellPoints.Count; i++)
        {
            Vector3 direction = (spellPoints[i] - spellPoints[i - 1]).normalized;
            int extraPointsCount = Mathf.FloorToInt((spellPoints[i] - spellPoints[i - 1]).magnitude / step);

            for (int j = 0; j < extraPointsCount; j++)
            {
                Instantiate(spellPoints[i - 1].x + j * direction.x * step, spellPoints[i - 1].y + j * direction.y * step);
            }
            Instantiate(spellPoints[i].x, spellPoints[i].y);
        }
    }

    private void Instantiate(float x, float y)
    {
        GameObject gameObject = Instantiate(Point, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
        SpellPoint spellPoint = gameObject.GetComponent<SpellPoint>();
        spellPoint.SpellPattern = this;
        points.Add(spellPoint);
        maxPoints += 1;
    }

    public GameObject GetSpell()
    {
        return SpellPatternPoints.Spell;
    }

    public float GetResult()
    {
        return correct / maxPoints;
    }

    public void ResetPoints()
    {
        correct = 0;
        foreach(var point in points)
        {
            point.ResetHit();
        }
    }

    public void Guess(Vector3 point)
    {
        Vector3 attack = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z + point.z + 30);
        Ray position = new Ray(attack, new Vector3(0, 0, -1));
        RaycastHit hit;
        if (Physics.Raycast(position, out hit))
        {
            
            SpellPoint spellPoint = hit.transform.GetComponent<SpellPoint>();
            if (spellPoint == null)
            {
                return;
            }

            spellPoint.Hit();
        }
    }

    public void HitOne()
    {
        correct++;
    }
}
