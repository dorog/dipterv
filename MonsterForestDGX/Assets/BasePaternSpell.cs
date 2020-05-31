using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = nameof(BasePaternSpell), menuName = nameof(BasePaternSpell))]
public class BasePaternSpell : PatternSpell
{
    public SpellPatternPoints SpellPatternPoints;
    public PlayerSpell[] levelsSpell;
}
