using System.Collections.Generic;
using UnityEngine;

public class SpellPattern : MonoBehaviour
{
    public bool isAttack = true;
    public int treeLine = 0;

    public float maxPoints = 0;
    public float correct = 0;
    public int lastId = int.MinValue;
    public ElementType Type;
    public SpellPatternPoints SpellPatternPoints;
    public GameObject Point;
    public List<SpellPoint> points = new List<SpellPoint>();

    private bool stackable = false;
    private int stacks = 0;
    private int step = 10;

    public void DrawPoints()
    {
        int extraId = 0;
        stackable = SpellPatternPoints.stackable;
        List<SpellPatternPoint> spellPoints = SpellPatternPoints.GetPoints();

        Instantiate(spellPoints[0].Id, spellPoints[0].Point.x, spellPoints[0].Point.y, stacks);
        for (int i = 1; i < spellPoints.Count; i++)
        {
            Vector3 direction = (spellPoints[i].Point - spellPoints[i - 1].Point).normalized;
            int extraPointsCount = Mathf.FloorToInt((spellPoints[i].Point - spellPoints[i - 1].Point).magnitude / step);

            for (int j = 0; j < extraPointsCount; j++)
            {
                extraId++;
                Instantiate(spellPoints[i - 1].Id + extraId, spellPoints[i - 1].Point.x + j * direction.x * step, spellPoints[i - 1].Point.y + j * direction.y * step, stacks);
            }
            Instantiate(spellPoints[i].Id + extraId, spellPoints[i].Point.x, spellPoints[i].Point.y, stacks);
            if (stackable)
            {
                stacks++;
            }
        }
    }

    private void Instantiate(int id, float x, float y, int stackNumber)
    {
        GameObject gameObject = Instantiate(Point, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
        SpellPoint spellPoint = gameObject.GetComponent<SpellPoint>();
        spellPoint.SpellPattern = this;
        spellPoint.Id = id;
        spellPoint.StackNumber = stackNumber;
        points.Add(spellPoint);
        maxPoints += 1;
    }

    public GameObject GetSpell()
    {
        return SpellPatternPoints.Spell;
    }

    public float GetResult()
    {
        if (stackable)
        {
            return GetBestMatch();
        }

        return correct / maxPoints;
    }

    private float GetBestMatch()
    {
        float max = 0;
        int actualStack = 0;
        float correct = 0;
        for(int i = 0; i < points.Count; i++)
        {
            if (points[i].done)
            {
                correct++;
            }
            if(points[i].StackNumber > actualStack)
            {
                float result = correct / (i + 1);
                if (max <= result)
                {
                    max = result;
                }
                actualStack++;
                Debug.Log("Last: " + i + ", " + correct + " / " + (i + 1) + " = " + result);
            }
        }

        return max;
    }

    public void ResetPoints()
    {
        correct = 0;
        lastId = int.MinValue;
        foreach(var point in points)
        {
            point.ResetHit();
        }
    }

    public void Guess(Vector3 point)
    {
        Vector3 attack = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z + point.z + 30);
        Ray position = new Ray(attack, new Vector3(0, 0, 1));
        RaycastHit hit;
        if (Physics.Raycast(position, out hit))
        {
            SpellPoint spellPoint = hit.transform.GetComponent<SpellPoint>();
            if (spellPoint == null)
            {
                return;
            }

            spellPoint.Hit(lastId);
        }
    }

    public void HitOne(int id)
    {
        correct++;
        lastId = id;
    }
}
