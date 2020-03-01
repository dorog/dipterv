using UnityEngine;

public class Player : Fighter
{
    public IAttack attack;

    public bool canAttack = false;
    public BattleManager battleManager;
    public Rigidbody rb;
    public bool InBattle = false;

    private float rotation = 0;
    public float rotationSpeed = 10;
    private float movement = 0;
    public float movementSpeed = 10;

    private void Start()
    {
        health.SetUpHealth();
    }

    public void Battle()
    {
        InBattle = true;
    }

    public void BattleEnd()
    {
        InBattle = false;
    }

    private void Update()
    {
        if (!InBattle)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotation -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotation += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movement += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement -= 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) && !canAttack)
            {
                health.SetUpBlock();
            }
            if (Input.GetMouseButtonDown(0) && canAttack)
            {
                battleManager.PlayerAttack();
                attack.Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * movement * Time.fixedDeltaTime * movementSpeed);
        movement = 0;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotation, 0) * Time.deltaTime * rotationSpeed);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rotation = 0;
    }

    public override void Die()
    {
        battleManager.PlayerDied();
    }

    public override void StartTurn()
    {
        AttackTurn();
    }

    public void DefTurn()
    {
        canAttack = false;
    }

    public void AttackTurn()
    {
        canAttack = true;
    }
}
