using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    public MonsterHealth health;
    public MonsterHitType monsterHitType;

    public void TakeDamage(float dmg, ElementType magicType)
    {
        if(monsterHitType == MonsterHitType.Body)
        {
            health.TakeDamageBody(dmg, magicType);
        }
        else
        {
            health.TakeDamageHead(dmg, magicType);
        }
    }
}

public enum MonsterHitType
{
    Body, Head
}
