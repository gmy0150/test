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
    public enum ButtonTypeEnum { Flip,Gravity,Delete,OnTrap,Generate,GenTrap};

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
        if (ButtonType == ButtonTypeEnum.GenTrap)
        {
            opendoor.SetActive(false);
        }
    }

    void Update()
    {
        if (isbutton())
        {
            if (!isClick &&ButtonType == ButtonTypeEnum.Gravity)
            {
                PushBtn();

                isClick = true;
                player.gravityState.PushButton();
            }else if(!isClick &&ButtonType == ButtonTypeEnum.Flip)
            {
                PushBtn();
                runPlayer.FlipX();
                isClick = true;
            }
            else if (!isClick && ButtonType == ButtonTypeEnum.Delete)
            {
                PushBtn();
                opendoor.SetActive(false);
                isClick = true;
            }
            else if (!isClick && ButtonType == ButtonTypeEnum.OnTrap)
            {
                PushBtn();
                opendoor.GetComponentInChildren<Trap>().ActiveDetect();

                isClick = true;
            }
            else if (!isClick && ButtonType == ButtonTypeEnum.Generate)
            {
                PushBtn();
                opendoor.SetActive(true);
                isClick = true;
            }
            else if (!isClick && ButtonType == ButtonTypeEnum.GenTrap)
            {
                PushBtn();
                opendoor.SetActive(true);
                opendoor.GetComponentInChildren<Trap>().ActiveDetect();
                isClick = true;
            }
        }
    }
    void PushBtn()
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
        if (opendoor != null)
        {
            if(ButtonType == ButtonTypeEnum.Delete)
        opendoor.SetActive(true);
            if (ButtonType == ButtonTypeEnum.Generate || ButtonType == ButtonTypeEnum.GenTrap)
        opendoor.SetActive(false);

        }
        isClick = false;
        transform.position = savePos;
    }
}
