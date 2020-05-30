using System.Collections;
using UnityEngine;

public class MovingTurnFill : TurnFill
{
    public string movingAnimation = "locomotion";
    public float forwardValue = 1f;
    public float backWardValue = 1f;
    private int direction = -1;

    private Vector3 startPosition;
    private Vector3 playerTurnPosition;

    private void Start()
    {
        startPosition = transform.position;
        playerTurnPosition = startPosition + transform.forward * (-1) * distance;
    }

    protected override IEnumerator Moving(bool forward, float time)
    {
        direction = forward ? 1 : -1;
        float value = forward ? forwardValue : backWardValue;
        animator.SetFloat(movingAnimation, value * direction);

        if (forward)
        {
            StartCoroutine(EnumeratorMoving.MoveToPosition(transform, startPosition, time));
        }
        else
        {
            StartCoroutine(EnumeratorMoving.MoveToPosition(transform, playerTurnPosition, time));
        }

        yield return new WaitForSeconds(time);

        animator.SetFloat(movingAnimation, 0);
    }
}
