using UnityEngine;

public abstract class PlayerSpell : MonoBehaviour
{
    public ElementType elementType = ElementType.TrueDamage;
    [Range(0, 1)]
    public float coverage;
    public float cd;

    public abstract string GetSpellType();
    public abstract float GetSpellTypeValue();
    public string GetDifficulty()
    {
        if (coverage < 0.6)
        {
            return SpellDifficulty.Easy.ToString();
        }
        else if (coverage < 0.8)
        {
            return SpellDifficulty.Normal.ToString();
        }
        else
        {
            return SpellDifficulty.Hard.ToString();
        }
    }

    public Color GetDifficultyColor()
    {
        if (coverage < 0.6)
        {
            return Color.green;
        }
        else if (coverage < 0.8)
        {
            return Color.yellow;
        }
        else
        {
            return Color.red;
        }
    }
}
