using UnityEngine;

public class SpellAttack : PlayerSpell
{
    public float dmg = 10;
    public GameObject impactParticle;
    private bool hasCollided = false;

    private BattleManager battleManager;

    public void SetBattleManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleManager.monsterTurnStartDelegateEvent += VanishSpell;
    }

    public override string GetSpellType()
    {
        return "Damage";
    }

    public override float GetSpellTypeValue()
    {
        return dmg;
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            DamageMonster(hit);

            Vector3 impactNormal = hit.contacts[0].normal;

            hasCollided = true;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

            Destroy(impactParticle, 3f);
            Destroy(gameObject);
        }
    }

    private void VanishSpell()
    {
        hasCollided = true;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        battleManager.monsterTurnStartDelegateEvent -= VanishSpell;
    }

    private void DamageMonster(Collision hit)
    {
        MonsterHit monsterHit = hit.gameObject.GetComponent<MonsterHit>();
        if (monsterHit != null)
        {
            SpellManager.GetInstance().AddXpForHit(coverage);
            monsterHit.TakeDamage(dmg * coverage, elementType);
        }
    }
}
