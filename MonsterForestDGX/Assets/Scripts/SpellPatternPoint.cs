using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPatternPoint
{
    public int Id { get; set; }
    public Vector2 Point { get; set; }

    public SpellPatternPoint(int id, Vector2 point)
    {
        Id = id;
        Point = point;
    }
}
