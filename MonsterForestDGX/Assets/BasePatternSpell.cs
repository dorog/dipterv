using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = nameof(BasePatternSpell), menuName = nameof(BasePatternSpell))]
public class BasePatternSpell : PatternSpell
{
    public SpellPatternPoints SpellPatternPoints;
    public BasePatternLevel[] levelsSpell;

    public PlayerSpell[] GetSpells()
    {
        PlayerSpell[] spells = new PlayerSpell[levelsSpell.Length];

        for (int i = 0; i < levelsSpell.Length; i++)
        {
            spells[i] = levelsSpell[i].playerSpell;
        }

        return spells;
    }

    public int[] GetRequiredExps()
    {
        int[] exps = new int[levelsSpell.Length];

        for (int i = 0; i < levelsSpell.Length; i++)
        {
            exps[i] = levelsSpell[i].exp;
        }

        return exps;
    }
}
