using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2;

    public Transform tf;

    //移动参数

    private void Start(){
        rb2.gravityScale = -1;
    }
}
