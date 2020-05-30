using System.Collections;
using UnityEngine;

public class MonsterBodyDisappear : MonoBehaviour
{
    public float amount = 10;
    public float disappearTime = 10;
    public float delayTime = 6;

    public IEnumerator DisAppear()
    {
        Vector3 aimPosition = transform.position + amount * Vector3.down;

        yield return new WaitForSeconds(delayTime);

        StartCoroutine(EnumeratorMoving.MoveToPosition(transform, aimPosition, disappearTime));
    }
}
