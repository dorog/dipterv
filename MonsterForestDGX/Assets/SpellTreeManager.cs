using System.Collections.Generic;
using UnityEngine;

public class SpellTreeManager : MonoBehaviour
{
    public TreeLine[] attackTreeLines;
    public TreeLine[] defenseTreeLines;
    public SpellManager spellManager;

    public GameObject SkillTree;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SkillTree.SetActive(!SkillTree.activeSelf);
        }
    }

    public void Awake()
    {
        SetTreeLineValues(attackTreeLines, true);
        SetTreeLineValues(defenseTreeLines, false);
    }

    private void Start()
    {
        foreach(var treeLine in attackTreeLines)
        {
            treeLine.spellTreeUI.SetUpSpells(treeLine);
        }
        //TODO: DefTreeLine foreach and setup
    }

    private void SetTreeLineValues(TreeLine[] treeLines, bool isAttack)
    {
        for(int i = 0; i < treeLines.Length; i++)
        {
            for(int j = 0; j < treeLines[i].spellPatternPoints.Length; j++)
            {
                SetSpellPatternPointsValues(treeLines[i].spellPatternPoints[j], isAttack, i);
            }
        }
    }

    private void SetSpellPatternPointsValues(SpellPatternPoints spellPatternPoints, bool isAttack, int treeLine)
    {
        spellPatternPoints.isAttack = isAttack;
        spellPatternPoints.treeLine = treeLine;
    }

    public List<SpellPatternPoints> GetAttackSpellPatternPoints()
    {
        return GetSepllPatternPoints(attackTreeLines);
    }

    public List<SpellPatternPoints> GetDefenseSpellPatternPoints()
    {
        return GetSepllPatternPoints(defenseTreeLines);
    }

    private List<SpellPatternPoints> GetSepllPatternPoints(TreeLine[] treeLines)
    {
        List<SpellPatternPoints> spellPatternPoints = new List<SpellPatternPoints>();

        for( int i = 0; i < treeLines.Length; i++)
        {
            for(int j = 0; j < treeLines[i].unlockedLvl; j++)
            {
                spellPatternPoints.Add(treeLines[i].spellPatternPoints[j]);
            }
        }

        return spellPatternPoints;
    }

    public void XpUpdate(bool isAttack, int treeLine, XpType xp)
    {
        TreeLine[] treeLineArray = isAttack ? attackTreeLines : defenseTreeLines;
        bool levelUp = treeLineArray[treeLine].AddXp(GetXp(xp));
        if (levelUp)
        {
            spellManager.AddNewPattern(isAttack, treeLineArray[treeLine].spellPatternPoints[treeLineArray[treeLine].unlockedLvl-1]);
        }
    }

    private int GetXp(XpType xpType)
    {
        switch (xpType)
        {
            case XpType.Cast:
                return 10;
            case XpType.Hit:
                return 20;
            case XpType.Kill:
                return 50;
            default:
                return 0;
        }
    }
}
