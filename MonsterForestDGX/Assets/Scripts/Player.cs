using UnityEngine;

public class Player : Fighter
{
    public PlayerRayAttack attack;

    public bool canAttack = false;
    public BattleManager battleManager;
    public Rigidbody rb;
    public bool InBattle = false;
    private bool inCast = false;

    private float rotation = 0;
    public float rotationSpeed = 10;
    private float movement = 0;
    public float movementSpeed = 10;
    private Vector3 mousePosition;
    public GameObject magicCircle;
    public PlayerHealth playerHealth;

    public Color DefenseGridColor;
    public Color AttackGridColor;
    public SpriteRenderer grid;

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
        health.inBlock = false;
        canAttack = false;
        inCast = false;
        playerHealth.BlockDown();
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
            if (Input.GetMouseButtonDown(0) && !inCast)
            {
                inCast = true;
                mousePosition = Input.mousePosition;
                magicCircle.SetActive(true);
            }
        }
    }

    public void Def()
    {
        health.SetUpBlock();
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void Attack(float dmg)
    {
        battleManager.PlayerAttack();

        attack.mousePosition = mousePosition;
        attack.dmg = dmg;
        attack.Attack();

        magicCircle.SetActive(false);
        inCast = false;
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
        //Necessary?
        magicCircle.SetActive(false);
        battleManager.PlayerDied();
    }

    public override void StartTurn()
    {
        AttackTurn();
    }

    public void DefTurn()
    {
        grid.color = DefenseGridColor;
        canAttack = false;
        magicCircle.SetActive(false);
        inCast = false;
    }

    public void AttackTurn()
    {
        grid.color = AttackGridColor;
        canAttack = true;
        magicCircle.SetActive(false);
        inCast = false;
    }
}
