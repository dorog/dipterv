using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if(health == null)
        {
            return;
        }

        health.TakeDamage(10);

        Destroy(gameObject, 4);
    }
}
