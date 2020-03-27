using System;

[Serializable]
public class TreeLine : TreeLineData
{
    public SpellPatternPoints[] spellPatternPoints;
    public SpellTreeUI spellTreeUI;

    public bool AddXp(int amount)
    {
        xp += amount;
        spellTreeUI.AddXp(amount);

        return IsleveledUp();
    }

    private bool IsleveledUp()
    {
        int realLvl = GetLevel();
        if(realLvl > lvl)
        {
            lvl = realLvl;
            spellTreeUI.LevelUp();
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetLevel()
    {
        if(xp < 5)
        {
            return 1;
        }
        else if(xp < 100)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
