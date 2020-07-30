using System.Collections.Generic;
using UnityEngine;

public class PatternFormula : ISpellPattern
{
    public readonly List<Rectangle> rectangles = new List<Rectangle>();
    private readonly float step = 10;

    private int lastId = int.MinValue;

    public Sprite icon;
    public int level = 0;

    public PlayerSpell[] Spells;
    public int[] RequiredExps;
    public ElementType ElementType;

    public PatternFormula(List<Vector2> points, Sprite icon, float width = 10)
    {
        this.icon = icon;
        int id = 0;
        for(int i = 0; i < points.Count - 1; i++)
        {
            int maxHit = Mathf.CeilToInt((points[i + 1] - points[i]).magnitude / step);
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
        }
    }

    public float GetResult()
    {
        if(level == 0)
        {
            return -1;
        }

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

    public int GetLevelValue()
    {
        return level;
    }

    public float GetMinCoverage()
    {
        if(level == 0)
        {
            return 2;
        }
        return Spells[level - 1].coverage;
    }

    //UI:
    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetSpellTypeUI()
    {
        if (level != 0)
        {
            return Spells[level - 1].GetSpellType().ToString();
        }
        else
        {
            return Spells[level].GetSpellType().ToString();
        }
    }

    public string[] GetLevelUI()
    {
        if (IsMaxed())
        {
            return new string[] { "Max" };
        }
        else if (level == 0)
        {
            return new string[] { level.ToString() };
        }
        else
        {
            return new string[] { level.ToString(), (level + 1).ToString() };
        }
    }

    public string[] GetTypeValueUI()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].GetSpellTypeValue().ToString() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].GetSpellTypeValue().ToString() };
        }
        else
        {
            return new string[] { Spells[level - 1].GetSpellTypeValue().ToString(), Spells[level].GetSpellTypeValue().ToString() };
        }
    }

    public float GetCooldown()
    {
        return Spells[level - 1].cd;
    }

    public string[] GetDifficulty()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].GetDifficulty() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].GetDifficulty() };
        }
        else
        {
            return new string[] { Spells[level - 1].GetDifficulty(), Spells[level].GetDifficulty() };
        }
    }

    public Color[] GetDifficultyColor()
    {
        if (IsMaxed())
        {
            return new Color[] { Spells[level - 1].GetDifficultyColor() };
        }
        else if (level == 0)
        {
            return new Color[] { Spells[level].GetDifficultyColor() };
        }
        else
        {
            return new Color[] { Spells[level - 1].GetDifficultyColor(), Spells[level].GetDifficultyColor() };
        }
    }

    public string GetRequiredExp()
    {
        if(IsMaxed())
        {
            return "---";
        }
        else
        {
            return RequiredExps[level].ToString();
        }
    }

    public string[] GetCooldownUI()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].cd.ToString() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].cd.ToString() };
        }
        else
        {
            return new string[] { Spells[level - 1].cd.ToString(), Spells[level].cd.ToString() };
        }
    }

    public int GetRequiredExpValue()
    {
        if(IsMaxed())
        {
            return int.MaxValue;
        }
        return RequiredExps[level];
    }

    public bool IsMaxed()
    {
        return level == Spells.Length;
    }

    public void LevelUp()
    {
        level++;
    }
}
