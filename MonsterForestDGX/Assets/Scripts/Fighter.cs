using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public Health health;

    public abstract void StartTurn();
    public abstract void Die();
}
