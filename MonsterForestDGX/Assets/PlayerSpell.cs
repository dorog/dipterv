using UnityEngine;

public abstract class PlayerSpell : MonoBehaviour
{
    public ElementType elementType = ElementType.TrueDamage;
    public float coverage;
    public float cd;

    public abstract string GetSpellType();
    public abstract float GetSpellTypeValue();
}
