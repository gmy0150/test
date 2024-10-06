using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    LayerMask cubemask;
    LayerMask playermask;
    float transY;
    protected SpriteRenderer render;
    public GameObject opendoor;
    protected Player player;
    protected JustRunPlayer runPlayer;
    Vector3 ToRay;
    protected bool isClick;
    public Type ButtonTouch;
    public enum Type { under, on,right,left};
    public ButtonTypeEnum ButtonType;
    public enum ButtonTypeEnum { Flip,Gravity};

    public float raylength = 0.55f;
    Vector2 savePos;
    protected float buttonValue;
    protected void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        playermask = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        runPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<JustRunPlayer>();
        render = GetComponent<SpriteRenderer>();
        savePos = transform.position;
        howray();
    }

    void Update()
    {
        if (isbutton())
        {
            if (!isClick &&ButtonType == ButtonTypeEnum.Gravity)
            {
                switch (ButtonTouch)
                {
                    case Type.under:
                        buttonValue = transform.position.y - 0.2f;
                        transform.position = new Vector3(transform.position.x, buttonValue, transform.position.z);
                        break;
                    case Type.on:
                        buttonValue = transform.position.y + 0.2f;
                        transform.position = new Vector3(transform.position.x, buttonValue, transform.position.z);

                        break;
                    case Type.left:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue, transform.position.y, transform.position.z);

                        break;
                    case Type.right:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue, transform.position.y, transform.position.z);


                     break;
                }

                isClick = true;
                player.gravityState.PushButton();
            }else if(!isClick &&ButtonType == ButtonTypeEnum.Flip)
            {
                switch (ButtonTouch)
                {
                    case Type.under:
                        buttonValue = transform.position.y - 0.2f;
                        transform.position = new Vector3(transform.position.x, buttonValue, transform.position.z);
                        break;
                    case Type.on:
                        buttonValue = transform.position.y + 0.2f;
                        transform.position = new Vector3(transform.position.x, buttonValue, transform.position.z);

                        break;
                    case Type.left:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue, transform.position.y, transform.position.z);

                        break;
                    case Type.right:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue, transform.position.y, transform.position.z);


                        break;
                }
                runPlayer.FlipX();
                isClick = true;
            }
        }
    }
    void howray(){
        switch(ButtonTouch){
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

    public void ResetButton(){
        isClick = false;
        transform.position = savePos;
    }
}
