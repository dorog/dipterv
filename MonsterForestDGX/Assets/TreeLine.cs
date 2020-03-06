using System;

[Serializable]
public class TreeLine
{
    public int xp = 0;
    public int unlockedLvl = 1;
    public SpellPatternPoints[] spellPatternPoints;

    public bool AddXp(int amount)
    {
        xp += amount;

        return IsleveledUp();
    }

    private bool IsleveledUp()
    {
        int realLvl = GetLevel();
        if(realLvl > unlockedLvl)
        {
            unlockedLvl = realLvl;
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
