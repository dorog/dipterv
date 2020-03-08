using UnityEngine;

public class MonsterHealth : Health
{
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;

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
            animator.SetTrigger(headHitAnimation);
            TakeDamage(dmg, magicType);
        }
    }

    public override void TakeDamage(float dmg, ElementType magicType)
    {
        base.TakeDamage(dmg, magicType);
        if(currentHp <= 0)
        {
            hpSlider.gameObject.SetActive(false);
        }
    }
}
