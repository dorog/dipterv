using UnityEngine;

public class SpellPoint : MonoBehaviour
{
    public bool done = false;
    public SpellPattern SpellPattern;
    public int Id;
    public int StackNumber;

    public void Hit(int lastId)
    {
        if (!done && Id > lastId)
        {
            done = true;
            SpellPattern.HitOne(Id);
        }
    }

    public void ResetHit()
    {
        done = false;
    }
}
