using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public PlayerFly playerFly;

    public void Start()
    {
        cm = GetComponent<Camera>();
    }

    Camera cm;

    Vector2 tmpPos;

    Vector2 tmpCenter;

    float tmpDis;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
    }

    public void FixedUpdate()
    {
        tmpCenter = Vector2.SmoothDamp(transform.position, playerFly.centerPos,ref tmpPos, .2f);

        transform.position = new Vector3(tmpCenter.x,tmpCenter.y,transform.position.z);

        cm.orthographicSize = Mathf.SmoothDamp(cm.orthographicSize, 0.5f*playerFly.maxDis, ref tmpDis, .6f); ;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(tmpCenter, .3f);
    }
}
