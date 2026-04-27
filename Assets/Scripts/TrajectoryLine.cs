using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{

    [Header("Line renderer variables")]
    public LineRenderer line;
    [Range (2,30)]
    public int resolution; // ปรับเส้นให้ละเอียดมากขึ้น (จริงๆ 15 ก้พอแล้ว)

    [Header("Formula variables")]
    public Vector2 velocity; // ปรับเส้นสูง / ต่ำ x = แนวนอน y = แนวตั้ง
    public float yLimit;     // ห้ามปรับค่ามากกว่า 0 ให้ตั้งตั้งแต่ -1 ลงไป เพราะถ้าตั้งมากกว่า 0 ฟิสิกส์เกมพัง (มีไว้ปรับขนาดของเส้น)
    private float gravity; 

    [Header("Linecast variables")]
    [Range (2,30)]
    public int linecastResolution;
    public LayerMask canHit;
   

    private void Start()
    {
        gravity = Mathf.Abs(Physics2D.gravity.y);
    }

    private void Update()
    {
        StartCoroutine(RenderArc());
    }





    public IEnumerator RenderArc()
    { 
        line.positionCount = resolution + 1;
        line.SetPositions(CalculateLineArray());
        yield return null;
        

    }



    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];

        var lowestTimeValue = MaxTimeX() / resolution;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }
        return lineArray;
    }

    private Vector2 HitPosition()
    { 
        var LowestTimeValue = MaxTimeY() / linecastResolution;

        for (int i = 0; i < linecastResolution + 1; i++)
        {
            var t = LowestTimeValue * i;
            var point = LowestTimeValue * (i + 1);
            var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(point), canHit);

            if (hit)
            {
                return hit.point;
            }
        }

        return CalculateLinePoint(MaxTimeY());


    }






    private Vector3 CalculateLinePoint(float t)
    { 
        float x = velocity.x * t;
        float y = (velocity.y * t) - (gravity * Mathf.Pow(t,2)) / 2f;

        return new Vector3(x + transform.position.x, y + transform.position.y);

    }


    private float MaxTimeY()
    { 
        var v = velocity.y;
        var vv =  v* v;

        var t = (v + Mathf.Sqrt(vv + 2 * gravity *(transform.position.y - yLimit))) / gravity;

        return t;
    }

    private float MaxTimeX()
    {
        var x = velocity.x;
        var t = (HitPosition().x - transform.position.x) / x;
        return t;
    }


}
