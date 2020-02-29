using System.Collections;
using UnityEngine;

public class Monster : Fighter
{
    public IAttack attack;
    [Range(0, 100)]
    public float blockChance = 10f;
    public BattleManager battleManager;
    [Range(0, 100)]
    public float secondAttackChance = 50;
    [Range(0, 100)]
    public float thirdAttackChance = 10;

    public float minWaitTime = 1;
    public float maxWaitTime = 10;

    public override void StartTurn()
    {
        StartCoroutine(Strike());
    }

    private IEnumerator Strike()
    {
        Debug.Log("Monster Start Turn");

        yield return new WaitForSeconds(2);

        Attack();
        float secondAttack = Random.Range(0, 100);
        if (secondAttack <= secondAttackChance)
        {
            yield return new WaitForSeconds(2);

            Attack();
            float thirdAttack = Random.Range(0, 100);
            if (thirdAttack <= thirdAttackChance)
            {
                yield return new WaitForSeconds(2);

                Attack();
            }
        }

        Debug.Log("Monster End Turn");

        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        StartCoroutine(Countdown(waitTime));

        yield return new WaitForSeconds(4);

        battleManager.PlayerTurn();
    }

    private IEnumerator Countdown(float time)
    {
        float duration = time;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        battleManager.MonsterTurn();
    }

    private void Attack()
    {
        Debug.Log("Attack!");
        attack.Attack();
    }

    private void Block()
    {
        Debug.Log("Monster Block!");
        health.SetUpBlock();
    }

    public void React()
    {
        float random = Random.Range(0, 100);
        if (random <= blockChance)
        {
            Block();
        }
    }

    public override void Die()
    {
        battleManager.MonsterDied();
    }
}
