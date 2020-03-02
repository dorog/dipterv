using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        CreatePatterns(AttackSpellPatternPoints, AttackPatterns, attackParent);
        CreatePatterns(DefSpellPatternPoints, DefPatterns, defParent);
    }

    private void CreatePatterns(List<SpellPatternPoints> SpellPatternPoints, List<SpellPattern> SpellPatterns, Transform parent)
    {
        int x = 0;
        foreach (var spellPatternPoint in SpellPatternPoints)
        {
            GameObject gameObject = Instantiate(SpellPattern.gameObject, parent.position + new Vector3(x, 0, 0), Quaternion.identity, parent);
            x += 1100;

            SpellPattern spellPattern = gameObject.GetComponent<SpellPattern>();
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
        int index = 0;
        float max = SpellPatterns[index].GetResult();
        for (int i = 0; i < SpellPatterns.Count; i++)
        {
            float result = SpellPatterns[i].GetResult();
            if(result > max)
            {
                index = i;
                max = result;
            }
        }

        Debug.Log("Coverage: " + max);
        if(minCoverage <= max)
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
