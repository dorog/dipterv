﻿using System.Collections;
using UnityEngine;

public class Monster : Fighter
{
    public Animator animator;
    public string appearAnimation;
    public float appearAnimationTime = 2;
    public string dieAnimation;

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

    public GameObject[] extraObjects;
    private bool died = false;

    private bool firstTurn = true;

    public override void StartTurn()
    {
        StartCoroutine(Strike());
    }

    private IEnumerator Strike()
    {
        Debug.Log("Monster Start Turn");

        if (firstTurn)
        {
            firstTurn = false;
            yield return new WaitForSeconds(2 + appearAnimationTime);
        }
        else
        {
            yield return new WaitForSeconds(2);
        }

        float animationTime = Attack();

        yield return new WaitForSeconds(2 + animationTime);

        float secondAttack = Random.Range(0, 101);
        if (secondAttack <= secondAttackChance)
        {
            animationTime = Attack();

            yield return new WaitForSeconds(2 + animationTime);

            float thirdAttack = Random.Range(0, 101);
            if (thirdAttack <= thirdAttackChance)
            {
                animationTime = Attack();

                yield return new WaitForSeconds(2 + animationTime);
            }
        }

        Debug.Log("Monster End Turn");

        float waitTime = Random.Range(minWaitTime, maxWaitTime + 1);
        StartCoroutine(Countdown(waitTime));

        yield return new WaitForSeconds(4);

        battleManager.PlayerTurn();
    }

    private IEnumerator Countdown(float time)
    {
        float duration = time;
        float normalizedTime = 0;
        while (normalizedTime <= 1f && !died)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        if (!died)
        {
            battleManager.MonsterTurn();
        }
    }

    private float Attack()
    {
        return attack.Attack();
    }

    private void Block()
    {
        health.SetUpBlock();
    }

    public void React()
    {
        float random = Random.Range(0, 101);
        if (random <= blockChance)
        {
            Block();
        }
    }

    public override void Die()
    {
        died = true;
        battleManager.MonsterDied();
        animator.SetTrigger(dieAnimation);
    }

    public void Appear()
    {
        foreach(var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
        health.SetUpHealth();
    }
}