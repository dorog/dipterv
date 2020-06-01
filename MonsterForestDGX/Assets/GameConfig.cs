using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Monster Settings")]
    public int aliveMonsters;

    [Header("Attack Spells Settings")]
    public BasePaternSpell[] baseSpells;

    [Header("Pet Settings")]
    public Pet[] pets;

    public BasePaternSpell[] GetBasePatternSpells()
    {
        BasePaternSpell[] basePatternSpells = new BasePaternSpell[baseSpells.Length];
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
