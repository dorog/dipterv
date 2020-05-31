using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Monster Settings")]
    public bool[] aliveMonsters;

    [Header("Attack Spells Settings")]
    public BasePaternSpell[] baseSpells;

    [Header("Pet Settings")]
    public PetData[] pets;

    [Header("Player Settings")]
    public float xp = 0;

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

    public bool[] GetAvailablePets()
    {
        if(pets == null || pets.Length == 0)
        {
            Debug.LogError(nameof(GetAvailablePets) + " Error!");
            return new bool[0];
        }
        else
        {
            bool[] availablePets = new bool[pets.Length];
            for(int i = 0; i < pets.Length; i++)
            {
                availablePets[i] = pets[i].available;
            }

            return availablePets;
        }
    }
}
