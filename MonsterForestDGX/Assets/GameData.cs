using System;

[Serializable]
public class GameData
{
    public PatternSpell[] BasePatternSpells;
    public AliveMonsters AliveMonsters;
    public bool[] availablePets;
    public float xp;

    public GameData() { }

    public GameData(GameConfig gameConfig)
    {
        AliveMonsters = new AliveMonsters(gameConfig.aliveMonsters);
        BasePatternSpells = gameConfig.GetBasePatternSpells();
        availablePets = gameConfig.GetAvailablePets();
        xp = gameConfig.xp;
    }
}
