using UnityEngine;

[CreateAssetMenu(fileName = "new Resistant", menuName = "Resistant")]
public class Resistant : ScriptableObject
{
    [Range(-100, 100)]
    public int water = 0;
    [Range(-100, 100)]
    public int earth = 0;
    [Range(-100, 100)]
    public int fire = 0;
    [Range(-100, 100)]
    public int air = 0;

    public float CalculateDmg(float dmg, ElementType magicType)
    {
        float resistant = GetResistant(magicType);

        return dmg * (1 - (resistant / 100));
    }

    public float GetResistant(ElementType magicType)
    {
        switch (magicType)
        {
            case ElementType.Water:
                return water;
            case ElementType.Earth:
                return water;
            case ElementType.Fire:
                return fire;
            case ElementType.Air:
                return air;
            default:
                return 0;
        }
    }
}
