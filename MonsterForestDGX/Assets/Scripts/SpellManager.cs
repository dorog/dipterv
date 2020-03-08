using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public float minCoverage = 0.6f;
    public SpellTreeManager spellTreeManager;

    public SpellPattern SpellPattern;

    private readonly List<SpellPattern> attackPatterns = new List<SpellPattern>();
    private readonly List<SpellPattern> defensePatterns = new List<SpellPattern>();

    public Transform attackParent;
    public Transform defenseParent;

    public AttackType attackType = AttackType.Undefined;
    public Text elementType;

    private readonly int distanceDiff = 1100;

    void Start()
    {
        elementType.text = attackType.ToString();

        CreatePatterns(spellTreeManager.GetAttackSpellPatternPoints(), attackPatterns, attackParent);
        CreatePatterns(spellTreeManager.GetDefenseSpellPatternPoints(), defensePatterns, defenseParent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackType = AttackType.Fire;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackType = AttackType.Water;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            attackType = AttackType.Undefined;
            elementType.text = attackType.ToString();
        }
    }

    private void CreatePatterns(List<SpellPatternPoints> SpellPatternPoints, List<SpellPattern> SpellPatterns, Transform parent, float extraHeigh = 0)
    {
        int x = 0;
        foreach (var spellPatternPoint in SpellPatternPoints)
        {
            CreateSpellPattern(x, parent, spellPatternPoint, SpellPatterns, extraHeigh);
            x += distanceDiff;

        }
    }

    private void CreateSpellPattern(int x, Transform parent, SpellPatternPoints spellPatternPoint, List<SpellPattern> SpellPatterns, float extraHeigh = 0)
    {
        GameObject gameObject = Instantiate(SpellPattern.gameObject, parent.position + new Vector3(x, extraHeigh, 0), Quaternion.identity, parent);

        SpellPattern spellPattern = gameObject.GetComponent<SpellPattern>();
        spellPattern.Type = spellPatternPoint.attackType;
        spellPattern.SpellPatternPoints = spellPatternPoint;
        spellPattern.DrawPoints();
        spellPattern.isAttack = spellPatternPoint.isAttack;
        spellPattern.treeLine = spellPatternPoint.treeLine;
        SpellPatterns.Add(spellPattern);
    }

    public void Guess(Vector3 point, bool canAttack)
    {
        List<SpellPattern> SpellPatterns = canAttack ? attackPatterns : defensePatterns;
        foreach (var spellPattern in SpellPatterns)
        {
            spellPattern.Guess(point);
        }
    }

    public GameObject GetSpell(bool canAttack)
    {
        List<SpellPattern> SpellPatterns = canAttack ? attackPatterns : defensePatterns;
        int index = -1;
        float max = -1;
        for (int i = 0; i < SpellPatterns.Count; i++)
        {
            if (SpellPatterns[i].Type == attackType)
            {
                float result = SpellPatterns[i].GetResult();
                if (result > max)
                {
                    index = i;
                    max = result;
                }
            }
        }

        Debug.Log("Coverage: " + max);
        if(minCoverage <= max && index != -1)
        {
            spellTreeManager.XpUpdate(canAttack, SpellPatterns[index].treeLine, XpType.Cast);
            return SpellPatterns[index].GetSpell();
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
            spellPattern.ResetPoints();
        }
        foreach (var spellPattern in defensePatterns)
        {
            spellPattern.ResetPoints();
        }
    }

    public void AddNewPattern(bool isAttack, SpellPatternPoints spellPatternPoints)
    {
        if (isAttack)
        {
            AddNewAttackPattern(spellPatternPoints);
        }
        else
        {
            AddNewDefensePattern(spellPatternPoints);
        }
    }

    private void AddNewAttackPattern(SpellPatternPoints spellPatternPoints)
    {
        int x = attackPatterns.Count * distanceDiff;
        CreateSpellPattern(x, attackParent, spellPatternPoints, attackPatterns);
    }

    private void AddNewDefensePattern(SpellPatternPoints spellPatternPoints)
    {
        int x = defensePatterns.Count * distanceDiff;
        CreateSpellPattern(x, defenseParent, spellPatternPoints, defensePatterns);
    }
}
