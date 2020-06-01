using UnityEngine;
using System;

[Serializable]
public abstract class PatternSpell : ScriptableObject
{
    public Sprite icon;
    public ElementType elementType = ElementType.TrueDamage;
}
