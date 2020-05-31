using UnityEngine;
using System;

[Serializable]
public abstract class PatternSpell : ScriptableObject
{
    public Sprite icon;
    public int level = 1;
    public int maxLevel = 4;
    public ElementType elementType = ElementType.TrueDamage;
}
