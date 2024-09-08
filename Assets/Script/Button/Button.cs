using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    LayerMask cubemask;
    LayerMask playermask;
    float transY;
    protected SpriteRenderer render;
    public GameObject opendoor;
    protected Player player;
    Vector3 ToRay;
    protected bool isClick;
    public Type ButtonType;
    
    public enum Type{under, on,right,left};
    public float raylength = 0.55f;
    protected float buttonValue;
    protected void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        playermask = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        render = GetComponent<SpriteRenderer>();
        howray();
    }

    void Update()
    {
        
    }
    void howray(){
        switch(ButtonType){
            case Type.under:
                ToRay = transform.up;
            break;
            case Type.on:
                ToRay = -transform.up;
            break;
            case Type.left:
                ToRay = transform.right;
            break;
            case Type.right:
                ToRay = -transform.right;
            break;
        }

    }
    protected void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + ToRay * raylength);
    }
    public bool isbutton() =>Physics2D.Raycast(transform.position, ToRay,raylength,cubemask|playermask);
}
