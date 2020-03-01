using UnityEngine;

public class Attack : MonoBehaviour
{
    public float dmg = 10;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if(health == null)
        {
            return;
        }

        health.TakeDamage(dmg);

        gameObject.SetActive(false);
    }
}
