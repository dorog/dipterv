using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public PatternSpell[] BasePatternSpells;
    public AliveMonsters AliveMonsters;

    public GameData() { }

    public GameData(GameConfig gameConfig)
    {
        AliveMonsters = new AliveMonsters(gameConfig.aliveMonsters);
        BasePatternSpells = gameConfig.GetBasePatternSpells();
    }
}
