using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMatrixConverter : MonoBehaviour
{
    public bool write = false;
    //public Transform flat;
    //public LineRenderer lineRenderer;

    public Transform point;
    public Transform plane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (write)
        {
            write = false;

            /*Vector3 relativPosition = point.position - plane.position;
            Vector3 projected = Vector3.ProjectOnPlane(relativPosition, plane.forward);
            Vector3 flattenedVector = plane.position + projected;*/

            //Vector3 relativ = point.position - plane.position;

            /*float up_angle = Vector3.SignedAngle(Vector3.forward, relativ, Vector3.up);
            float left_angle = Vector3.SignedAngle(Vector3.forward, relativ, Vector3.left);
            float forward_angle = Vector3.SignedAngle(Vector3.forward, relativ, Vector3.forward);

            Vector3 rotatedVector = Quaternion.Euler(-left_angle, up_angle, forward_angle) * relativ;


            Debug.Log(rotatedVector);*/

            //Vector3 playerPoint = flat.position + rotatedVector;

            //Plan B
            Vector3 relativ = point.position - plane.position;

            float angle = Vector3.SignedAngle(relativ, plane.up, plane.forward);
            int signalX = 1;
            if(angle < 0)
            {
                signalX = -1;
            }

            int signalY = 1;
            if(Mathf.Abs(angle) > 90)
            {
                signalY = -1;
            }

            Vector3 up = Vector3.ProjectOnPlane(relativ, plane.up);
            float x = up.magnitude * signalX;

            Vector3 right = Vector3.ProjectOnPlane(relativ, plane.right);
            float y = right.magnitude * signalY;
        }
    }
}
