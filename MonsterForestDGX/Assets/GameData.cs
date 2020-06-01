using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public PatternSpell[] BasePatternSpells;
    public AliveMonsters AliveMonsters;
    public bool[] availablePets;
    public float exp;
    public GameObject lastLocation = null;

    public GameData() { }

    public GameData(GameConfig gameConfig, float playerExp)
    {
        AliveMonsters = new AliveMonsters(gameConfig.aliveMonsters);
        BasePatternSpells = gameConfig.GetBasePatternSpells();
        availablePets = gameConfig.GetAvailablePets();
        exp = playerExp;
    }
}
