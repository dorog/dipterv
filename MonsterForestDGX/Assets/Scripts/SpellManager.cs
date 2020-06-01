﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellManager : SingletonClass<SpellManager>
{
    private PlayerExperience playerExperience;

    public SpellPattern SpellPattern;

    private readonly List<ISpellPattern> attackPatterns = new List<ISpellPattern>();
    private readonly List<ISpellPattern> defensePatterns = new List<ISpellPattern>();

    public Transform attackParent;
    public Transform defenseParent;

    private readonly int distanceDiff = 1100;

    private float lastCoverage = 0;

    public GameObject castParent;
    public CastEffect[] castEffects;

    public SpellsUI spellsUI;

    private void Awake()
    {
        Init(this);
    }

    void Start()
    {
        List<BasePaternSpell> basePaternSpells = SharedData.GameConfig.baseSpells.ToList();
        CreatePatterns(basePaternSpells, attackPatterns, attackParent);
        spellsUI.SetupUI(attackPatterns);

        playerExperience = PlayerExperience.GetInstance();
    }

    private void CreatePatterns(List<BasePaternSpell> BasePaternSpells, List<ISpellPattern> SpellPatterns, Transform parent, float extraHeigh = 0)
    {
        int x = 0;
        foreach (var basePaternSpell in BasePaternSpells)
        {
            CreateSpellPattern(x, parent, basePaternSpell, SpellPatterns, extraHeigh);
            x += distanceDiff;
        }
    }

    private void CreateSpellPattern(int x, Transform parent, BasePaternSpell basePaternSpell, List<ISpellPattern> SpellPatterns, float extraHeigh = 0)
    {
        List<Vector2> points = new List<Vector2>();
        List<SpellPatternPoint> spellPatternPoints = basePaternSpell.SpellPatternPoints.GetPoints();
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            points.Add(spellPatternPoints[i].Point);
        }

        PatternFormula spellPattern = new PatternFormula(points, basePaternSpell.icon);
        spellPattern.ElementType = basePaternSpell.SpellPatternPoints.attackType;
        spellPattern.level = basePaternSpell.level;
        spellPattern.Spells = basePaternSpell.levelsSpell;
        SpellPatterns.Add(spellPattern);
    }

    public void Guess(Vector3 point, bool canAttack)
    {
        List<ISpellPattern> SpellPatterns = canAttack ? attackPatterns : defensePatterns;
        foreach (var spellPattern in SpellPatterns)
        {
            spellPattern.Guess(point);
        }
    }

    public SpellResult GetSpell(bool canAttack)
    {
        List<ISpellPattern> SpellPatterns = canAttack ? attackPatterns : defensePatterns;
        int index = -1;
        float max = -1;
        for (int i = 0; i < SpellPatterns.Count; i++)
        {
            float result = SpellPatterns[i].GetResult();
            float minCoverage = SpellPatterns[i].GetMinCoverage();
            if ((minCoverage <= result) && (result > max))
            {
                index = i;
                max = result;
            }
        }


        if(index != -1)
        {
            playerExperience.AddExp(ExpType.Cast, max);
            SpellResult spellResult = new SpellResult
            {
                id = index,
                spell = SpellPatterns[index].GetSpell(),
                coverage = max,
                cooldown = SpellPatterns[index].GetCooldown()
            };

            ElementType elementType = SpellPatterns[index].GetElementType();
            GameObject castEffect = null;
            for(int i = 0; i < castEffects.Length; i++)
            {
                if(elementType == castEffects[i].ElementType)
                {
                    castEffect = castEffects[i].gameObject;
                    break;
                }
            }
            if(castEffect != null)
            {
                Instantiate(castEffect, castParent.transform);
            }

            return spellResult;
        }
        else
        {
            return null;
        }
    }

    public void ResetSpells()
    {
        foreach (var spellPattern in attackPatterns)
        {
            spellPattern.Reset();
        }
        foreach (var spellPattern in defensePatterns)
        {
            spellPattern.Reset();
        }
    }

    public void AddNewPattern(bool isAttack, SpellPatternPoints spellPatternPoints)
    {

    }

    private void AddNewAttackPattern(SpellPatternPoints spellPatternPoints)
    {

    }

    private void AddNewDefensePattern(SpellPatternPoints spellPatternPoints)
    {

    }

    public void Won()
    {
        playerExperience.AddExp(ExpType.Kill, lastCoverage);

        for (int i = 0; i < attackPatterns.Count; i++)
        {
            //Debug.Log(attackPatterns[i].level);
            SharedData.GameConfig.baseSpells[i].level = attackPatterns[i].GetLevelValue();
        }

        DataManager.GetInstance().Won();
    }

    public void AddXpForHit(float coverage)
    {
        lastCoverage = coverage;
        Debug.Log("Not only Attack!");
        playerExperience.AddExp(ExpType.Hit, coverage);
    }
}
