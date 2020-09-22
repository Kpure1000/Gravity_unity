using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mController : MonoBehaviour
{
    /// <summary>
    /// 质量
    /// </summary>
    public float mass;

    /// <summary>
    /// 比例体积
    /// </summary>
    public float volume
    {
        get
        {
            return m_scale * 100;
        }
        set
        {
            m_scale = value / 100;
        }
    }

    public Vector2 position
    {   
        get
        {
            return transform.position;
        }
    }

    /// <summary>
    /// 缩放比例
    /// </summary>
    private float m_scale;

}
