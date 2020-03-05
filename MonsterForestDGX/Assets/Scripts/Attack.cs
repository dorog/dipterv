using UnityEngine;

public class Attack : MonoBehaviour
{
    public float dmg = 10;
    public AttackType magicType = AttackType.Undefined;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if(health == null)
        {
            return;
        }

        health.TakeDamage(dmg, magicType);

        gameObject.SetActive(false);
    }
}
