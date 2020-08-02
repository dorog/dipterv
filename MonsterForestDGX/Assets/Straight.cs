using UnityEngine;

public class Straight
{
    private readonly float value;
    private readonly Vector2 normal;

    public Vector2 P1 { get; private set; }
    public Vector2 P2 { get; private set; }

    public Straight(Vector2 point, Vector2 normal, Vector2 end)
    {
        P1 = point;
        P2 = end;

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
