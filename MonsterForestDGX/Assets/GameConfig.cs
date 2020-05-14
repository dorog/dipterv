using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Monster Settings")]
    public bool[] aliveMonsters;

    [Header("Attack Spells Setting")]
    public BasePaternSpell[] baseSpells;

    public PatternSpell[] GetBasePatternSpells()
    {
        PatternSpell[] basePatternSpells = new PatternSpell[baseSpells.Length];
        for(int i = 0; i < baseSpells.Length; i++)
        {
            basePatternSpells[i] = baseSpells[i];
        }

        return basePatternSpells;
    }

    public List<SpellPatternPoints> GetBasePatternSpellsPoints()
    {
        List<SpellPatternPoints> basePatternSpellsPoints = new List<SpellPatternPoints>();

        for (int i = 0; i < baseSpells.Length; i++)
        {
            basePatternSpellsPoints.Add(baseSpells[i].SpellPatternPoints);
        }

        return basePatternSpellsPoints;
    }
}
