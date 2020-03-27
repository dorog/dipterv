using UnityEngine;
using UnityEngine.UI;

public class SpellTreeUI : MonoBehaviour
{
    public Text xpText;
    private int xp = 0;
    private int unlockedLvl = 0;

    public SpellElement[] spellElements;

    private void OnEnable()
    {
        UpdateXP();
    }

    public void SetUpSpells(TreeLine treeLine)
    {
        xp = treeLine.xp;
        unlockedLvl = treeLine.lvl;

        for (int i = 0; i < unlockedLvl; i++)
        {
            spellElements[i].SetUp();
        }
    }

    public void AddXp(int amount)
    {
        xp += amount;
        UpdateXP();
    }

    private void UpdateXP()
    {
        xpText.text = xp + " XP";
    }

    public void LevelUp()
    {
        spellElements[unlockedLvl].SetUp();
        unlockedLvl++;
    }
}
