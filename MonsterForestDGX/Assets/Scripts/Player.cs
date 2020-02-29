using UnityEngine;

public class Player : Fighter
{
    public IAttack attack;
    public bool canAttack = false;
    public BattleManager battleManager;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && !canAttack)
        {
            health.SetUpBlock();
        }
        if(Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        battleManager.PlayerAttack();
        attack.Attack();
    }

    public override void Die()
    {
        Debug.Log("Player die!");
        battleManager.PlayerDied();
    }

    public override void StartTurn()
    {
        Debug.Log("Player Start Turn!");
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
