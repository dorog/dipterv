using UnityEngine;

public class MonsterHealth : Health
{
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;
    public MonsterBodyDisappear monsterBodyDisappear;

    private bool death = false;

    private SpellManager spellManager;

    private void Start()
    {
        spellManager = SpellManager.GetInstance();
    }

    public void TakeDamageBody(float dmg, ElementType magicType)
    {
        if (inBlock)
        {
            TakeDamage(dmg, magicType);
            animator.SetTrigger(blockAnimation);
        }
        else
        {
            TakeDamage(dmg, magicType);
            if(currentHp > 0)
            {
                animator.SetTrigger(bodyHitAnimation);
            }
        }
    }

    public void TakeDamageHead(float dmg, ElementType magicType)
    {
        if (inBlock)
        {
            animator.SetTrigger(blockAnimation);
        }
        else
        {
            TakeDamage(dmg, magicType);
            if (currentHp > 0)
            {
                animator.SetTrigger(headHitAnimation);
            }
        }
    }

    public override void TakeDamage(float dmg, ElementType magicType)
    {
        //Player doesn't need it?
        if (death)
        {
            return;
        }
        base.TakeDamage(dmg, magicType);
        if(currentHp <= 0)
        {
            death = true;
            hpSlider.gameObject.SetActive(false);
            StartCoroutine(monsterBodyDisappear.DisAppear());
        }
    }
}
