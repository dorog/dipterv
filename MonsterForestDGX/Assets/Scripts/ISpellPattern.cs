using UnityEngine;

public interface ISpellPattern
{
    void Guess(Vector2 point);
    float GetResult();
    void Reset();
    GameObject GetSpell();
    ElementType GetElementType();
}
