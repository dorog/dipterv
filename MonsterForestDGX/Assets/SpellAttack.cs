using UnityEngine;

public class SpellAttack : PlayerSpell
{
    public float dmg = 10;
    public GameObject impactParticle;
    private bool hasCollided = false;

    private void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            MonsterHit monsterHit = hit.gameObject.GetComponent<MonsterHit>();
            if (monsterHit != null)
            {
                SpellManager.GetInstance().AddXpForHit(id);
                monsterHit.TakeDamage(dmg, attackType);
            }

            Vector3 impactNormal = hit.contacts[0].normal;

            hasCollided = true;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

            Destroy(impactParticle, 3f);
            Destroy(gameObject);
        }
    }
}
