using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFly : MonoBehaviour
{
    [Header("引力常量")]
    public float GravityScale = 1.0f;

    public GameObject stars;

    public Vector2 initSpeed;

    public bool flying;

    /******/

    private List<mController> ms;

    private Rigidbody2D rb;

    private Vector2 acc_main;

    private Vector2 vel_main;

    private float minX, maxX, minY, maxY;

    public void Start()
    {
        lr = GetComponent<LineRenderer>();
        ms = stars.GetComponentsInChildren<mController>().ToList();
        rb = GetComponent<Rigidbody2D>();
        foreach (var item in ms)
        {
            minX = Mathf.Min(minX, item.position.x);
            maxX = Mathf.Max(maxX, item.position.x);
            minY = Mathf.Min(minY, item.position.y);
            maxY = Mathf.Max(maxY, item.position.y);
        }
        vel_main = initSpeed;
        newVel = vel_main;
        newPos = transform.position;
    }

    private Vector2 tmpCenterPos;

    public Vector2 centerPos
    {
        get
        {
            tmpCenterPos = Vector2.zero;
            foreach (var item in ms)
            {
                tmpCenterPos = (tmpCenterPos + item.position) / 2;
            }
            tmpCenterPos = (tmpCenterPos + (Vector2)transform.position) / 2;
            float disX = Mathf.Max(transform.position.x, maxX) - Mathf.Min(transform.position.x, minX);
            float disY = Mathf.Max(transform.position.y, maxY) - Mathf.Min(transform.position.y, minY);
            maxDis = Mathf.Max(disX, disY);
            return tmpCenterPos;
        }
    }

    public float maxDis { get; private set; }

    bool isInStar = false;

    Vector2 newPos;

    Vector2 newVel;

    const float dt = 0.03f;

    float t = 0f;

    const float maxT = 50.0f;

    int pointSize = 0;

    LineRenderer lr;

    public void Update()
    {
        if (!flying)
        {
            //newPos = transform.position;
            while (t <= maxT)
            {
                CalAcc(newPos);
                if (!isInStar)
                {
                    newVel += acc_main * dt;
                }
                newPos += newVel * dt;
                pointSize++;
                lr.SetVertexCount(pointSize);
                lr.SetPosition(pointSize - 1, newPos);
                t += dt;
            }
            flying = true;
            t = 0;
        }
    }

    public void FixedUpdate()
    {
        if (flying)
        {
            CalAcc(transform.position);
            if (!isInStar)
            {
                vel_main += acc_main * dt;
            }
            transform.position += (Vector3)vel_main * dt;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    /// <summary>
    /// 计算加速度
    /// </summary>
    [Obsolete("不用了",false)]
    public void CalAcc()
    {
        acc_main = Vector2.zero;
        Vector2 dir;
        for (int i = 0; i < ms.Count; i++)
        {
            dir = ms[i].position - (Vector2)transform.position;
            if (dir.magnitude < 2.0f)
            {
                isInStar = true;
                continue;
            }
            else
            {
                isInStar = false;
            }
            acc_main += dir.normalized * GravityScale * ms[i].mass /
                Mathf.Pow(dir.magnitude, 2.0f);
        }
    }
    /// <summary>
    /// 根据自定义位置计算加速度
    /// </summary>
    /// <param name="curPos"></param>
    public void CalAcc(Vector2 curPos)
    {
        acc_main = Vector2.zero;
        Vector2 dir;
        for (int i = 0; i < ms.Count; i++)
        {
            dir = ms[i].position - curPos;
            if (dir.magnitude < 1.0f)
            {
                isInStar = true;
                continue;
            }
            else
            {
                isInStar = false;
            }
            acc_main += dir.normalized * GravityScale * ms[i].mass /
                Mathf.Pow(dir.magnitude, 2.0f);
        }
    }

    public void SetFly()
    {
        flying = !flying;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, vel_main));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + 20 * acc_main);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(tmpCenterPos, maxDis / 2);
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag.GetHashCode() == "Star".GetHashCode())
    //    {
    //        //Debug.Log("在球内, 停止加速");
    //        isInStar = true;
    //    }
    //}
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag.GetHashCode() == "Star".GetHashCode())
    //    {
    //        //Debug.Log("出球内, 开始加速");
    //        isInStar = false;
    //    }
    //}


}
