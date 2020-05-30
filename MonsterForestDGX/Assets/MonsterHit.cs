using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    public MonsterHealth health;
    public MonsterHitType monsterHitType;
    [Range(0, 200)]
    public float dmgPercent = 100;

    public void TakeDamage(float dmg, ElementType magicType)
    {
        float damage = dmg * dmgPercent / 100;
        if(monsterHitType == MonsterHitType.Body)
        {
            health.TakeDamageBody(damage, magicType);
        }
        else
        {
            health.TakeDamageHead(damage, magicType);
        }
    }
}

public enum MonsterHitType
{
    Body, Head
}
