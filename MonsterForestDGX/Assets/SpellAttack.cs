using UnityEngine;

public class SpellAttack : PlayerSpell
{
    public GameObject parent;
    public float dmg = 10;

    private void OnTriggerEnter(Collider other)
    {
        MonsterHit monsterHit = other.gameObject.GetComponent<MonsterHit>();
        if (monsterHit != null)
        {
            monsterHit.TakeDamage(dmg, attackType);
        }

        Destroy(parent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        MonsterHit monsterHit = collision.gameObject.GetComponent<MonsterHit>();
        if (monsterHit != null)
        {
            monsterHit.TakeDamage(dmg, attackType);
        }

        Destroy(parent);
    }
}
