using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Fighter, IEnemy
{
    public string MonsterName = "";
    public Text nameText;
    public Animator animator;
    public string appearAnimation;
    public float appearAnimationTime = 2;
    public string disappearAnimation;
    public float disappearAnimationTime = 2;
    public string dieAnimation;

    public IAttack attack;
    [Range(0, 100)]
    public float blockChance = 10f;
    public BattleManager battleManager;
    [Range(0, 100)]
    public float[] extraAttackChances;

    public float minWaitTime = 1;
    public float maxWaitTime = 10;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    private bool died = false;

    private bool firstTurn = true;

    public TurnFill turnFill;

    private bool firstRound = true;

    public override void StartTurn()
    {
        if (firstRound)
        {
            firstRound = false;
            MonsterTurnStart();
        }
        else
        {
            turnFill.MoveForward();
        }
    }

    private IEnumerator Strike()
    {
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

        for (int i = 0; i < extraAttackChances.Length; i++)
        {
            float extraAttack = Random.Range(0, 101);
            if (extraAttack <= extraAttackChances[i])
            {
                animationTime = Attack();

                yield return new WaitForSeconds(2 + animationTime);
            }
            else
            {
                break;
            }
        }

        float waitTime = Random.Range(minWaitTime + turnFill.time, maxWaitTime + 1 + turnFill.time);
        StartCoroutine(Countdown(waitTime));

        yield return new WaitForSeconds(4);

        turnFill.MoveBack();
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

    public void React()
    {
        float random = Random.Range(0, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
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
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
        health.SetUpHealth();
        //nameText.text = MonsterName;
    }

    public void Disappear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(false);
        }
        foreach (var particle in extraParticles)
        {
            particle.Stop();
        }

        animator.SetTrigger(disappearAnimation);
    }

    public void MonsterTurnEnd()
    {
        battleManager.PlayerTurn();
    }

    public void MonsterTurnStart()
    {
        StartCoroutine(nameof(Strike));
    }

    public void ResetMonster()
    {
        firstRound = true;
        firstTurn = true;

        StopCoroutine(nameof(Strike));
        //StopCoroutine(Countdown(2));

        Disappear();

        health.ResetHealth();
    }

    public Health GetHealth()
    {
        return GetComponent<MonsterHealth>();
    }
}
