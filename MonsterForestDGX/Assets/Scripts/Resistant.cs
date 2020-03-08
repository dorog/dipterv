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

    public float CalculateDmg(float dmg, ElementType magicType)
    {
        float resistant = GetResistant(magicType);

        return dmg * (1 - (resistant / 100));
    }

    public float GetResistant(ElementType magicType)
    {
        switch (magicType)
        {
            case ElementType.Fire:
                return fire;
            case ElementType.Water:
                return water;
            case ElementType.Poison:
                return poison;
            case ElementType.Laser:
                return laser;
            default:
                return 0;
        }
    }
}
