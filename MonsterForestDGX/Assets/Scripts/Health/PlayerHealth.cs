using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Text hp;
    public GameObject block;

    public TimeDamageBlock timeDamageBlock;

    public override void SetUpHealth()
    {
        base.SetUpHealth();
        
        //hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }

    public void BlockDown()
    {
        //base.BlockDown();
        block.SetActive(false);
    }

    public void Heal(float amount)
    {
        if(maxHp - currentHp >= amount)
        {
            currentHp += amount;
        }
        else
        {
            currentHp = maxHp;
        }

        SetUpHealth();
    }

    protected override float GetBlockedDamage(float dmg)
    {
        if (timeDamageBlock != null)
        {
            return timeDamageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void SetDamageBlock()
    {
        if (timeDamageBlock != null)
        {
            timeDamageBlock.StartBlock();
        }
    }
}
