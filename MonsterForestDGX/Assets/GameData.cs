using System;

[Serializable]
public class GameData
{
    public PatternSpell[] BasePatternSpells;
    public AliveMonsters AliveMonsters;
    public bool[] availablePets;
    public float exp;

    public GameData() { }

    public GameData(GameConfig gameConfig, float playerExp)
    {
        AliveMonsters = new AliveMonsters(gameConfig.aliveMonsters);
        BasePatternSpells = gameConfig.GetBasePatternSpells();
        availablePets = gameConfig.GetAvailablePets();
        exp = playerExp;
    }
}
