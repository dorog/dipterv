using System;

[Serializable]
public class SpellTreeLineData
{
    public TreeLineData[] attackTreeLine;
    public TreeLineData[] defenseTreeLine;

    public SpellTreeLineData(int attackSpells, int defenseSpells)
    {
        Init(attackSpells, defenseSpells);
    }

    public SpellTreeLineData(TreeLine[] attackSpells, TreeLine[] defenseSpells)
    {
        Init(attackSpells.Length, defenseSpells.Length);

        for (int i = 0; i < attackTreeLine.Length; i++)
        {
            attackTreeLine[i].xp = attackSpells[i].xp;
            attackTreeLine[i].lvl = attackSpells[i].lvl;
        }
        for (int i = 0; i < defenseTreeLine.Length; i++)
        {
            defenseTreeLine[i].xp = defenseSpells[i].xp;
            defenseTreeLine[i].lvl = defenseSpells[i].lvl;
        }
    }

    private void Init(int attackSpells, int defenseSpells)
    {
        attackTreeLine = new TreeLineData[attackSpells];
        defenseTreeLine = new TreeLineData[defenseSpells];

        for (int i = 0; i < attackSpells; i++)
        {
            attackTreeLine[i] = new TreeLineData();
        }
        for (int i = 0; i < defenseSpells; i++)
        {
            defenseTreeLine[i] = new TreeLineData();
        }
    }
}
