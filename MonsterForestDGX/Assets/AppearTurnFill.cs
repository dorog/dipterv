using System.Collections;
using UnityEngine;

public class AppearTurnFill : TurnFill
{
    public string appearAnimation;
    public float appearAnimationTime;
    public string disapperAnimation;
    public float disappearAnimationTime;

    public float undergroundtime;

    private void Start()
    {
        time = undergroundtime + appearAnimationTime + disappearAnimationTime;
    }

    protected override IEnumerator Moving(bool forward, float time)
    {
        animator.SetTrigger(disapperAnimation);
        yield return new WaitForSeconds(disappearAnimationTime);

        gameObject.transform.position += gameObject.transform.forward * distance * (forward ? 1 : -1);
        yield return new WaitForSeconds(undergroundtime);

        animator.SetTrigger(appearAnimation);
        yield return new WaitForSeconds(appearAnimationTime);
    }
}
