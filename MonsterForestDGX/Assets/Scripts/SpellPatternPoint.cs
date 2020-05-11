using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPatternPoint
{
    public int Id { get; set; }
    public Vector3 Point { get; set; }

    public SpellPatternPoint(int id, Vector3 point)
    {
        Id = id;
        Point = point;
    }
}
