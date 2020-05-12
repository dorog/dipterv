using UnityEngine;

public class SpellMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.fixedDeltaTime * transform.forward * speed);
    }
}
