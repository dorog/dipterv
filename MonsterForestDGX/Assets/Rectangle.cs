using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rectangle
{
    private readonly int id;
    private readonly float step;
    private readonly bool[] dones;
    private readonly int maxHit;

    private Straight maxY;
    private Straight minY;

    private Straight maxX;
    private Straight minX;

    private Vector2 distancePoint;
    private Vector2 direction;

    private HitResult lastHitResult = null;

    public Rectangle(int id, float step, int maxHit, Vector2 startPoint, Vector2 endPoint, float width)
    {
        this.id = id;
        this.step = step;
        this.maxHit = maxHit;
        dones = new bool[maxHit];

        CalculateLines(startPoint, endPoint, width);
    }

    private void CalculateLines(Vector2 startPoint, Vector2 endPoint, float width)
    {

        direction = (endPoint - startPoint).normalized;
        float signedAngle = Vector2.SignedAngle(Vector2.right, direction);

        if (signedAngle >= 0 && signedAngle < 90)
        {
            // 0 and I
            SetUpLines(startPoint, direction, startPoint, endPoint, width);
        }
        else if (signedAngle >= 90 && signedAngle < 180)
        {
            // 90 and II
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            Vector2 normal = new Vector2(direction.y, -direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else if (signedAngle < 0 && signedAngle >= -90)
        {
            // -90 and IV
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            Vector2 normal = new Vector2(-direction.y, direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else
        {
            // 180 and III
            SetUpLines(startPoint, -direction, endPoint, startPoint, width);
        }
    }

    private void SetUpLines(Vector2 distancePoint, Vector2 direction, Vector2 startPoint, Vector2 endPoint, float width)
    {
        this.distancePoint = distancePoint;

        Vector2 normal = new Vector2(-direction.y, direction.x);

        maxY = new Straight(startPoint + width * normal, normal, endPoint + width * normal);
        minY = new Straight(startPoint - width * normal, normal, endPoint - width * normal);

        maxX = new Straight(endPoint + width * normal, direction, endPoint - width * normal);
        minX = new Straight(startPoint + width * normal, direction, startPoint - width * normal);
    }

    private bool Include(Vector2 point)
    {
        float maxValueY = maxY.GetY(point.x);
        float minValueY = minY.GetY(point.x);

        float maxValueX = maxX.GetX(point.y);
        float minValueX = minX.GetX(point.y);

        return (point.y >= minValueY && point.y <= maxValueY) && (point.x <= maxValueX && point.x >= minValueX);
    }

    public int Guess(Vector2 point, int lastId)
    {
        if (id + maxHit <= lastId)
        {
            return -1;
        }

        if (lastHitResult != null)
        {
            List<IntersectionResult> results = new List<IntersectionResult>
            {
                LineIntersection(minX.P1, minX.P2, lastHitResult.Point, point),
                LineIntersection(minY.P1, minY.P2, lastHitResult.Point, point),
                LineIntersection(maxX.P1, maxX.P2, lastHitResult.Point, point),
                LineIntersection(maxY.P1, maxY.P2, lastHitResult.Point, point)
            };

            results.RemoveAll(x => !x.Intersected);

            if (results.Count > 0)
            {
                if(results.Count > 1)
                {
                    results.Sort((x, y) => IntersectionResult.SortByDistance(x, y, lastHitResult.Point));

                    List<int> indexes = new List<int>();
                    foreach (var result in results)
                    {
                        int index = GetCell(result.Point);
                        indexes.Add(index);
                    }

                    indexes = indexes.Distinct().ToList();

                    if(indexes.Count != 2)
                    {
                        Debug.LogError("Not two point!");
                    }

                    int lastIndex = indexes[0];
                    int actualIndex = indexes[1];

                    //Same #1
                    lastHitResult.Included = true;
                    lastHitResult.Point = point;

                    if (actualIndex > lastIndex)
                    {
                        for (int i = lastIndex; i <= actualIndex; i++)
                        {
                            if (i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return actualIndex + id;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    int lastIndex = -1;
                    int actualIndex = -2;

                    if (lastHitResult.Included)
                    {
                        lastIndex = GetCell(lastHitResult.Point);
                        actualIndex = GetCell(results[0].Point);
                    }
                    else
                    {
                        lastIndex = GetCell(results[0].Point);
                        actualIndex = GetCell(point);
                    }

                    //Same #3
                    lastHitResult.Included = true;
                    lastHitResult.Point = point;

                    if (actualIndex > lastIndex)
                    {
                        for (int i = lastIndex; i <= actualIndex; i++)
                        {
                            if (i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return actualIndex + id;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                if (Include(point) && lastHitResult.Included)
                {
                    int lastIndex = GetCell(lastHitResult.Point);
                    int actualIndex = GetCell(point);

                    //Same #2
                    lastHitResult.Included = true;
                    lastHitResult.Point = point;

                    if (actualIndex > lastIndex)
                    {
                        for(int i = lastIndex; i <= actualIndex; i++)
                        {
                            if(i + id > lastId)
                            {
                                dones[i] = true;
                            }
                        }

                        return actualIndex + id;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    lastHitResult = new HitResult()
                    {
                        Included = false,
                        Point = point
                    };

                    return -1;
                }
            }
        }
        else
        {
            if (Include(point))
            {
                lastHitResult = new HitResult()
                {
                    Included = true,
                    Point = point
                };

                int calculatedLastId = GetCell(point);

                dones[calculatedLastId] = true;

                return calculatedLastId + id;
            }
            else
            {
                lastHitResult = new HitResult()
                {
                    Included = false,
                    Point = point
                };

                return -1;
            }
        }
    }

    private int GetCell(Vector2 point)
    {
        Vector2 dir = point - distancePoint;
        float angle = Vector2.Angle(dir, direction);
        float length = dir.magnitude;

        float distance = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * angle) * length);

        int calculatedId = Mathf.FloorToInt(distance / step);
        calculatedId = calculatedId >= dones.Length ? dones.Length - 1 : calculatedId;

        return calculatedId;
    }

    public int GetHitNumber()
    {
        int hits = 0;
        for (int i = 0; i < dones.Length; i++)
        {
            if (dones[i])
            {
                hits++;
            }
        }

        return hits;
    }

    public int GetMaxHitNumber()
    {
        return maxHit;
    }

    public void Reset()
    {
        for(int i = 0; i < dones.Length; i++)
        {
            dones[i] = false;
        }
    }

    private class HitResult
    {
        public bool Included { get; set; }
        public Vector2 Point { get; set; }
    }

    private class IntersectionResult
    {
        public bool Intersected { get; set; }
        public Vector2 Point { get; set; }

        public static int SortByDistance(IntersectionResult first, IntersectionResult second, Vector2 startPoint)
        {
            float firstDistance = first.GetDistanceFrom(startPoint);
            float secondDistance = second.GetDistanceFrom(startPoint);

            if(firstDistance > secondDistance)
            {
                return 1;
            }
            else if(firstDistance < secondDistance)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public float GetDistanceFrom(Vector2 from)
        {
            return (Point - from).magnitude;
        }
    }

    //Copy
    //"Faster Line Segment Intersection" by Franklin Antonio(1992).
    private static IntersectionResult LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num, offset;
        float x1lo, x1hi, y1lo, y1hi;

        Ax = p2.x - p1.x;
        Bx = p3.x - p4.x;

        // X bound box test/
        if (Ax < 0)
        {
            x1lo = p2.x; x1hi = p1.x;
        }
        else
        {
            x1hi = p2.x; x1lo = p1.x;
        }

        if (Bx > 0)
        {
            if (x1hi < p4.x || p3.x < x1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (x1hi < p3.x || p4.x < x1lo) 
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        Ay = p2.y - p1.y;
        By = p3.y - p4.y;

        // Y bound box test//
        if (Ay < 0)
        {
            y1lo = p2.y; y1hi = p1.y;
        }
        else
        {
            y1hi = p2.y; y1lo = p1.y;
        }

        if (By > 0)
        {
            if (y1hi < p4.y || p3.y < y1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (y1hi < p3.y || p4.y < y1lo)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        Cx = p1.x - p3.x;
        Cy = p1.y - p3.y;
        d = By * Cx - Bx * Cy;  // alpha numerator//
        f = Ay * Bx - Ax * By;  // both denominator//

        // alpha tests//
        if (f > 0)
        {
            if (d < 0 || d > f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (d > 0 || d < f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        e = Ax * Cy - Ay * Cx;  // beta numerator//

        // beta tests //
        if (f > 0)
        {
            if (e < 0 || e > f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }
        else
        {
            if (e > 0 || e < f)
            {
                return new IntersectionResult()
                {
                    Intersected = false
                };
            }
        }

        // check if they are parallel
        if (f == 0)
        {
            return new IntersectionResult()
            {
                Intersected = false
            };
        }

        Vector2 intersection = Vector2.zero;

        // compute intersection coordinates //
        num = d * Ax; // numerator //
        offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;   // round direction //
        intersection.x = p1.x + (num + offset) / f;

        num = d * Ay;
        offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;
        intersection.y = p1.y + (num + offset) / f;

        return new IntersectionResult()
        {
            Intersected = true,
            Point = intersection
        };
    }

    private static bool same_sign(float a, float b)
    {
        return ((a * b) >= 0f);
    }
}
