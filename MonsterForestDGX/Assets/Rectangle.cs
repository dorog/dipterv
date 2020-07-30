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
            //Debug.Log("I");
            SetUpLines(startPoint, direction, startPoint, endPoint, width);
        }
        else if (signedAngle >= 90 && signedAngle < 180)
        {
            // 90 and II
            //Debug.Log("II");
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            /*Debug.Log("F: " + (endPoint - startPoint) / 2);
            Debug.Log("S: " + startPoint);
            Debug.Log("H: " + half);*/
            Vector2 normal = new Vector2(direction.y, -direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else if (signedAngle < 0 && signedAngle >= -90)
        {
            // -90 and IV
            //Debug.Log("IV");
            Vector2 half = (endPoint - startPoint) / 2 + startPoint;
            Vector2 normal = new Vector2(-direction.y, direction.x);
            SetUpLines(startPoint, normal, half - width * normal, half + width * normal, (startPoint - endPoint).magnitude / 2);
        }
        else
        {
            // 180 and III
            //Debug.Log("III");
            SetUpLines(startPoint, -direction, endPoint, startPoint, width);
        }
    }

    private void SetUpLines(Vector2 distancePoint, Vector2 direction, Vector2 startPoint, Vector2 endPoint, float width)
    {
        this.distancePoint = distancePoint;

        Vector2 normal = new Vector2(-direction.y, direction.x);

        maxY = new Straight(startPoint + width * normal, normal);
        minY = new Straight(startPoint - width * normal, normal);

        maxX = new Straight(endPoint, direction);
        minX = new Straight(startPoint, direction);
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
        if (Include(point))
        {
            Vector2 dir = point - distancePoint;
            float angle = Vector2.Angle(dir, direction);
            float length = dir.magnitude;

            float distance = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * angle) * length);

            int calculatedId = Mathf.FloorToInt(distance / step);
            calculatedId = calculatedId >= dones.Length ? dones.Length - 1 : calculatedId;
            if (!dones[calculatedId] && calculatedId + id > lastId)
            {
                dones[calculatedId] = true;
                return calculatedId + id;
            }
            else
            {
                return -1;
            }
        }

        return -1;
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
}
