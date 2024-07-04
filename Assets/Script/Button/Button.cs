using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    LayerMask cubemask;
    float transY;
    SpriteRenderer render;
    public GameObject opendoor;
    Player player;
    Vector3 ToRay;
    [SerializeField]private Type ButtonType;
    
    [SerializeField]private enum Type{under, on,right,left};
    public float raylength = 0.5f;
    float buttonValue;
    bool isClick;
    void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        render = GetComponent<SpriteRenderer>();
        howray();
    }

    void Update()
    {
        if(isbutton()){
            if(!isClick){
                switch(ButtonType){
                    case Type.under:
                        buttonValue = transform.position.y - 0.2f;
                    transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);
                    break;
                    case Type.on:
                        buttonValue = transform.position.y + 0.2f;
                    transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);

                    break;
                    case Type.left:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);

                    break;
                    case Type.right:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);


                    break;
                }

                isClick = true;
            
                render.material.color = Color.red;
                if(gameObject !=null){
                    opendoor.SetActive(false);
                }
            }
        }
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
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + ToRay * raylength);
    }
    private bool isbutton() =>Physics2D.Raycast(transform.position, ToRay,raylength,cubemask);
}
