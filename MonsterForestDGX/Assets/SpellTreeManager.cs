using System.Collections.Generic;
using UnityEngine;

public class SpellTreeManager : SingletonClass<SpellTreeManager>
{
    public SpellManager spellManager;

    public GameObject SkillTree;

    public TreeLine[] attackTreeLines;
    public TreeLine[] defenseTreeLines;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SkillTree.SetActive(!SkillTree.activeSelf);
        }
    }

    public void Awake()
    {
        DataManager dataManager = DataManager.GetInstance();
        //SpellTreeLineData spellTreeLineData = dataManager.gameData.SpellTreeLineData;

        //SetTreeLineValues(attackTreeLines, spellTreeLineData.attackTreeLine, true);
        //SetTreeLineValues(defenseTreeLines, spellTreeLineData.defenseTreeLine, false);

        Init(this);
    }

    private void Start()
    {
        foreach(var treeLine in attackTreeLines)
        {
            treeLine.spellTreeUI.SetUpSpells(treeLine);
        }
        foreach (var treeLine in defenseTreeLines)
        {
            treeLine.spellTreeUI.SetUpSpells(treeLine);
        }
    }

    private void SetTreeLineValues(TreeLine[] treeLines, TreeLineData[] treeLineData, bool isAttack)
    {
        for(int i = 0; i < treeLines.Length; i++)
        {
            for(int j = 0; j < treeLines[i].spellPatternPoints.Length; j++)
            {
                treeLines[i].xp = treeLineData[i].xp;
                treeLines[i].lvl = treeLineData[i].lvl;
                SetSpellPatternPointsValues(treeLines[i].spellPatternPoints[j], isAttack, i);
            }
        }
    }

    private void SetSpellPatternPointsValues(SpellPatternPoints spellPatternPoints, bool isAttack, int treeLine)
    {
        //spellPatternPoints.isAttack = true; //isAttack;
        //spellPatternPoints.treeLine = 1; //treeLine;
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
            for(int j = 0; j < treeLines[i].lvl; j++)
            {
                spellPatternPoints.Add(treeLines[i].spellPatternPoints[j]);
            }
        }

        return spellPatternPoints;
    }

    public void XpUpdate(bool isAttack, int treeLine, ExpType xp)
    {
        /*TreeLine[] treeLineArray = isAttack ? attackTreeLines : defenseTreeLines;
        bool levelUp = treeLineArray[treeLine].AddXp(GetXp(xp));
        if (levelUp)
        {
            spellManager.AddNewPattern(isAttack, treeLineArray[treeLine].spellPatternPoints[treeLineArray[treeLine].lvl-1]);
        }*/
    }

    public void Won()
    {
        SpellTreeLineData spellTreeLineData = new SpellTreeLineData(attackTreeLines, defenseTreeLines);

        DataManager dataManager = DataManager.GetInstance();
        //dataManager.Won(spellTreeLineData);
    }

    private int GetXp(ExpType xpType)
    {
        switch (xpType)
        {
            case ExpType.Cast:
                return 0;
            case ExpType.Hit:
                return 0;
            case ExpType.Kill:
                return 0;
            default:
                return 0;
        }
    }
}
