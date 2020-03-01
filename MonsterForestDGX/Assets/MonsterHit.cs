using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    public MonsterHealth health;
    public MonsterHitType monsterHitType;

    public void TakeDamage(float dmg)
    {
        if(monsterHitType == MonsterHitType.Body)
        {
            health.TakeDamageBody(dmg);
        }
        else
        {
            health.TakeDamageHead(dmg);
        }
    }
}

public enum MonsterHitType
{
    Body, Head
}
