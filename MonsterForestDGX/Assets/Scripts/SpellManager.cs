using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public float minCoverage = 0.6f;
    
    public List<SpellPatternPoints> AttackSpellPatternPoints = new List<SpellPatternPoints>();
    public List<SpellPatternPoints> DefSpellPatternPoints = new List<SpellPatternPoints>();
    public SpellPattern SpellPattern;

    private List<SpellPattern> AttackPatterns = new List<SpellPattern>();
    private List<SpellPattern> DefPatterns = new List<SpellPattern>();

    public Transform attackParent;
    public Transform defParent;

    public AttackType attackType = AttackType.Undefined;
    public Text elementType;

    void Start()
    {
        elementType.text = attackType.ToString();

        CreatePatterns(AttackSpellPatternPoints, AttackPatterns, attackParent);
        CreatePatterns(DefSpellPatternPoints, DefPatterns, defParent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            attackType = AttackType.Fire;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            attackType = AttackType.Water;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0))
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
            GameObject gameObject = Instantiate(SpellPattern.gameObject, parent.position + new Vector3(x, extraHeigh, 0), Quaternion.identity, parent);
            x += 1100;

            SpellPattern spellPattern = gameObject.GetComponent<SpellPattern>();
            spellPattern.Type = spellPatternPoint.attackType;
            spellPattern.SpellPatternPoints = spellPatternPoint;
            spellPattern.DrawPoints();

            SpellPatterns.Add(spellPattern);
        }
    }

    public void Guess(Vector3 point, bool canAttack)
    {
        List<SpellPattern> SpellPatterns = canAttack ? AttackPatterns : DefPatterns;
        foreach (var spellPattern in SpellPatterns)
        {
            spellPattern.Guess(point);
        }
    }

    public GameObject GetSpell(bool canAttack)
    {
        List<SpellPattern> SpellPatterns = canAttack ? AttackPatterns : DefPatterns;
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
            return SpellPatterns[index].GetSpell();
        }
        else
        {
            return null;
        }
    }

    public void ResetSpells()
    {
        foreach (var spellPattern in AttackPatterns)
        {
            spellPattern.ResetPoints();
        }
        foreach (var spellPattern in DefPatterns)
        {
            spellPattern.ResetPoints();
        }
    }
}
