using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    public float blockValue = 10;

    public virtual float GetCalculatedDamage(float damage)
    {
        return CalculateDamaga(blockValue, damage);
    }

    protected float CalculateDamaga(float value, float dmg)
    {
        float blockedDamage = dmg;

        if (value >= blockedDamage)
        {
            blockedDamage = 0;
        }
        else
        {
            blockedDamage -= value;
        }

        return blockedDamage;
    }
}
