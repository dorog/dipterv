using UnityEngine;

public class MonsterHealth : Health
{
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;

    public void TakeDamageBody(float dmg)
    {
        if (inBlock)
        {
            animator.SetTrigger(blockAnimation);
            TakeDamage(dmg);
        }
        else
        {
            animator.SetTrigger(bodyHitAnimation);
            TakeDamage(dmg);
        }
    }

    public void TakeDamageHead(float dmg)
    {
        if (inBlock)
        {
            animator.SetTrigger(blockAnimation);
        }
        else
        {
            animator.SetTrigger(headHitAnimation);
            TakeDamage(dmg);
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if(currentHp <= 0)
        {
            hpSlider.gameObject.SetActive(false);
        }
    }
}
