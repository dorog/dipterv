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
            StartCoroutine(MoveToPosition(transform, startPosition, time));
        }
        else
        {
            StartCoroutine(MoveToPosition(transform, playerTurnPosition, time));
        }

        yield return new WaitForSeconds(time);

        animator.SetFloat(movingAnimation, 0);
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
