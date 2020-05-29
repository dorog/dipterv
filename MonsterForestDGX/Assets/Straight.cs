using UnityEngine;

public class Straight
{
    private readonly float value;
    private readonly Vector2 normal;

    public Straight(Vector2 point, Vector2 normal)
    {
        value = point.x * normal.x + point.y * normal.y;
        this.normal = normal;
    }

    public float GetY(float x)
    {
        float y = (value - normal.x * x) / normal.y;
        return y;
    }

    public float GetX(float y)
    {
        float x = (value - normal.y * y) / normal.x;
        return x;
    }
}
