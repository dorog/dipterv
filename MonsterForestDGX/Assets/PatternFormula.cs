using System.Collections.Generic;
using UnityEngine;

public class PatternFormula : ISpellPattern
{
    public readonly List<Rectangle> rectangles = new List<Rectangle>();
    private readonly float step = 10;

    private int lastId = int.MinValue;

    public Sprite icon;
    public int level = 0;
    //public float xp = 0;

    public PlayerSpell[] Spells;
    public ElementType ElementType;

    public PatternFormula(List<Vector2> points, Sprite icon, float width = 10)
    {
        this.icon = icon;
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

    public GameObject GetSpell()
    {
        return Spells[level - 1].gameObject;
    }

    public ElementType GetElementType()
    {
        return ElementType;
    }

    public string GetSpellType()
    {
        return Spells[level - 1].GetSpellType();
    }

    public string GetLevel()
    {
        if(level == Spells.Length - 1)
        {
            return "Max";
        }
        return level.ToString();
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public float GetTypeValue()
    {
        return Spells[level - 1].GetSpellTypeValue();
    }

    public float GetCooldown()
    {
        return Spells[level - 1].cd;
    }

    public int GetLevelValue()
    {
        return level;
    }
}
