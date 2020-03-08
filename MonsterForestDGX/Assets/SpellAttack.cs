using UnityEngine;

public class SpellAttack : MonoBehaviour
{
    public GameObject parent;
    public float dmg = 10;
    public AttackType attackType = AttackType.Undefined;

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
