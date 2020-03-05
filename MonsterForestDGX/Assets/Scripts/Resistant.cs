using UnityEngine;

[CreateAssetMenu(fileName = "new Resistant", menuName = "Resistant")]
public class Resistant : ScriptableObject
{
    [Range(-100, 100)]
    public float fire = 0;
    [Range(-100, 100)]
    public float water = 0;
    [Range(-100, 100)]
    public float poison = 0;
    [Range(-100, 100)]
    public float laser = 0;

    public float CalculateDmg(float dmg, AttackType magicType)
    {
        float resistant = GetResistant(magicType);

        return dmg * (1 - (resistant / 100));
    }

    public float GetResistant(AttackType magicType)
    {
        switch (magicType)
        {
            case AttackType.Fire:
                return fire;
            case AttackType.Water:
                return water;
            case AttackType.Poison:
                return poison;
            case AttackType.Laser:
                return laser;
            default:
                return 0;
        }
    }
}
