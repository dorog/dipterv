using UnityEngine;

public class SpellPoint : MonoBehaviour
{
    public bool done = false;
    public SpellPattern SpellPattern;

    public void Hit()
    {
        //Debug.Log("Hit");
        if (!done)
        {
            done = true;
            SpellPattern.HitOne();
        }
    }

    public void ResetHit()
    {
        done = false;
    }
}
