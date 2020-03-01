using UnityEngine;

public class BurstAttack : MonoBehaviour
{
    public float dmgPerSecond = 5;
    public float duration = 5;

    public void OnEnable()
    {
        Invoke(nameof(End), duration);
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        Health health = other.GetComponent<Health>();
        if (health == null)
        {
            return;
        }

        float dmg = Time.deltaTime / 1 * dmgPerSecond;
        health.TakeDamage(dmg);

        //gameObject.SetActive(false);
    }

    private void End()
    {
        gameObject.SetActive(false);
    }
}
