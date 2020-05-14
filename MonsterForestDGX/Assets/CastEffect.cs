using UnityEngine;

public class CastEffect : MonoBehaviour
{
    public ElementType ElementType;
    public float aliveTime = 2f;

    private void Start()
    {
        Destroy(gameObject, aliveTime);
    }
}
