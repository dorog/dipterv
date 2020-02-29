using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public Fighter fighter;
    public Text hp;
    private bool inBlock = false;

    public void Start()
    {
        hp.text = health.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if (inBlock)
        {
            inBlock = false;
            Debug.Log("Blocked");
            return;
        }
        //TODO: Add resistant calculation
        //TODO: Monster: If take dmg, chance for attack back -> start turn
        health -= dmg;
        hp.text = health.ToString();

        if (health <= 0)
        {
            fighter.Die();
        }
    }

    public void SetUpBlock()
    {
        inBlock = true;
    }
}
