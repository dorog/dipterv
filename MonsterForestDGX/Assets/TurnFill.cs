using System.Collections;
using UnityEngine;

public abstract class TurnFill : MonoBehaviour
{
    public Monster monster;
    public float time = 2;
    public float distance = 10;

    public Animator animator;

    public void MoveBack()
    {
        StartCoroutine(Move(false));
    }

    public void MoveForward()
    {
        StartCoroutine(Move(true));
    }

    private IEnumerator Move(bool forward)
    {
        StartCoroutine(Moving(forward, time));

        yield return new WaitForSeconds(time);

        if (forward)
        {
            monster.MonsterTurnStart();
        }
        else
        {
            monster.MonsterTurnEnd();
        }
    }

    protected abstract IEnumerator Moving(bool forward, float time);
}
