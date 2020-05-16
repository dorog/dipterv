using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : SingletonClass<SpellManager>
{
    public float minCoverage = 0.6f;
    //public SpellTreeManager spellTreeManager;

    public SpellPattern SpellPattern;

    private readonly List<SpellPattern> attackPatterns = new List<SpellPattern>();
    private readonly List<SpellPattern> defensePatterns = new List<SpellPattern>();

    public Transform attackParent;
    public Transform defenseParent;

    //public ElementType attackType = ElementType.TrueDamage;
    //public Text elementType;

    private readonly int distanceDiff = 1100;

    private int lastSpell = -1;
    private float lastCoverage = 0;

    public GameObject castParent;
    public CastEffect[] castEffects;

    private void Awake()
    {
        Init(this);
    }

    void Start()
    {
        //elementType.text = attackType.ToString();

        CreatePatterns(SharedData.GameConfig.baseSpells.ToList(), attackPatterns, attackParent);
        //CreatePatterns(spellTreeManager.GetDefenseSpellPatternPoints(), defensePatterns, defenseParent);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            //attackType = ElementType.Fire;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackType = ElementType.Water;
            elementType.text = attackType.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            attackType = ElementType.TrueDamage;
            elementType.text = attackType.ToString();
        }
    }*/

    private void CreatePatterns(List<BasePaternSpell> BasePaternSpells, List<SpellPattern> SpellPatterns, Transform parent, float extraHeigh = 0)
    {
        int x = 0;
        foreach (var basePaternSpell in BasePaternSpells)
        {
            CreateSpellPattern(x, parent, basePaternSpell, SpellPatterns, extraHeigh);
            x += distanceDiff;
        }
    }

    private void CreateSpellPattern(int x, Transform parent, BasePaternSpell basePaternSpell, List<SpellPattern> SpellPatterns, float extraHeigh = 0)
    {
        GameObject gameObject = Instantiate(SpellPattern.gameObject, parent.position + new Vector3(x, extraHeigh, 0), Quaternion.identity, parent);

        SpellPattern spellPattern = gameObject.GetComponent<SpellPattern>();
        spellPattern.Type = basePaternSpell.SpellPatternPoints.attackType;
        spellPattern.SpellPatternPoints = basePaternSpell.SpellPatternPoints;
        spellPattern.DrawPoints();
        spellPattern.isAttack = true;
        spellPattern.level = basePaternSpell.level;
        spellPattern.xp = basePaternSpell.xp;
        spellPattern.Spells = basePaternSpell.levelsSpell;
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

    public SpellResult GetSpell(bool canAttack)
    {
        List<SpellPattern> SpellPatterns = canAttack ? attackPatterns : defensePatterns;
        int index = -1;
        float max = -1;
        for (int i = 0; i < SpellPatterns.Count; i++)
        {
            float result = SpellPatterns[i].GetResult();
            if (result > max)
            {
                index = i;
                max = result;
            }
        }


        if(minCoverage <= max && index != -1)
        {
            SpellPatterns[index].AddXp(XpType.Cast, max);
            SpellResult spellResult = new SpellResult
            {
                id = index,
                spell = SpellPatterns[index].GetSpell(),
                coverage = max
            };

            ElementType elementType = SpellPatterns[index].Type;
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

            //SpellPatterns[index].Type
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
            spellPattern.ResetPoints();
        }
        foreach (var spellPattern in defensePatterns)
        {
            spellPattern.ResetPoints();
        }
    }

    public void AddNewPattern(bool isAttack, SpellPatternPoints spellPatternPoints)
    {
        /*if (isAttack)
        {
            AddNewAttackPattern(spellPatternPoints);
        }
        else
        {
            AddNewDefensePattern(spellPatternPoints);
        }*/
    }

    private void AddNewAttackPattern(SpellPatternPoints spellPatternPoints)
    {
        //int x = attackPatterns.Count * distanceDiff;
        //CreateSpellPattern(x, attackParent, spellPatternPoints, attackPatterns);
    }

    private void AddNewDefensePattern(SpellPatternPoints spellPatternPoints)
    {
        //int x = defensePatterns.Count * distanceDiff;
        //CreateSpellPattern(x, defenseParent, spellPatternPoints, defensePatterns);
    }

    public void Won()
    {
        attackPatterns[lastSpell].AddXp(XpType.Kill, lastCoverage);

        for (int i = 0; i < attackPatterns.Count; i++)
        {
            Debug.Log(attackPatterns[i].xp);
            SharedData.GameConfig.baseSpells[i].xp = attackPatterns[i].xp;
            Debug.Log(attackPatterns[i].level);
            SharedData.GameConfig.baseSpells[i].level = attackPatterns[i].level;
        }

        DataManager.GetInstance().Won();
    }

    public void AddXpForHit(int id, float coverage)
    {
        lastSpell = id;
        lastCoverage = coverage;
        Debug.Log("Not only Attack!");
        attackPatterns[id].AddXp(XpType.Hit, coverage);
    }
}
