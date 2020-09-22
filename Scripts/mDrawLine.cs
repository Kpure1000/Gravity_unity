using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mDrawLine : MonoBehaviour
{
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    private int pointSize = 0;

    Vector2 position = Vector2.zero;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("按下鼠标");
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointSize++;
            lr.SetVertexCount(pointSize);
        }
        lr.SetPosition(pointSize - 1, position);
    }

    public void OnGUI()
    {
        GUILayout.Label(position.ToString());
    }
}
