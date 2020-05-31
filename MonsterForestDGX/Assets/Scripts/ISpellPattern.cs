using UnityEngine;

public abstract class ISpellPattern
{
    public abstract void Guess(Vector2 point);
    public abstract float GetResult();
    public abstract void Reset();
    public abstract GameObject GetSpell();
    public abstract ElementType GetElementType();
    public abstract string GetSpellType();
    public abstract string GetLevel();
    public abstract int GetLevelValue();
    public abstract Sprite GetIcon();
    public abstract float GetTypeValue();
    public abstract float GetCooldown();
}
