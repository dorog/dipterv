using System.Collections.Generic;
using UnityEngine;

public class PatternFormula : ISpellPattern
{
    public readonly List<Rectangle> rectangles = new List<Rectangle>();
    private readonly float step = 10;

    private int lastId = int.MinValue;

    public int level = 0;
    public float xp = 0;

    public GameObject[] Spells;
    public ElementType ElementType;

    public PatternFormula(List<Vector2> points, float width = 10)
    {
        int id = 0;
        for(int i = 0; i < points.Count - 1; i++)
        {
            int maxHit = Mathf.CeilToInt((points[i + 1] - points[i]).magnitude / step);
            //int maxHit = 1;
            rectangles.Add(new Rectangle(id, step, maxHit, points[i], points[i + 1], width));
            id += maxHit;
        }
    }

    public void Guess(Vector2 point)
    {
        int minId = int.MaxValue;
        bool hit = false;
        for(int i = 0; i < rectangles.Count; i++)
        {
            int resultId = rectangles[i].Guess(point, lastId);
            if(resultId != -1 && minId > resultId)
            {
                minId = resultId;
                hit = true;
            }
        }

        if (hit)
        {
            lastId = minId;
            //Debug.Log("New ID: " + lastId);
        }
    }

    public float GetResult()
    {
        int correct = 0;
        int max = 0;
        for(int i = 0; i < rectangles.Count; i++)
        {
            correct += rectangles[i].GetHitNumber();
            max += rectangles[i].GetMaxHitNumber();
        }

        return ((float)correct) / max;
    }

    public void Reset()
    {
        lastId = int.MinValue;
        for (int i = 0; i < rectangles.Count; i++)
        {
            rectangles[i].Reset();
        }
    }

    public void AddXp(XpType xpType, float coverage)
    {
        xp += xpType.GetXp() * coverage;
        LevelSet();
    }

    private void LevelSet()
    {
        if (xp > 10)
        {
            level = 2;
        }
        else if (xp > 50)
        {
            level = 3;
        }
        else if (xp > 100)
        {
            level = 4;
        }
        else
        {
            level = 1;
        }
    }

    public GameObject GetSpell()
    {
        return Spells[level - 1];
    }

    public ElementType GetElementType()
    {
        return ElementType;
    }
}
