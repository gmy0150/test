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
    public int numberOfRays = 5;
    Vector2 savePos;
    protected float buttonValue;
    AudioSource audioSource;
    AudioClip clip;
    protected void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        playermask = LayerMask.GetMask("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        runPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<JustRunPlayer>();
        render = GetComponent<SpriteRenderer>();
        savePos = transform.position;
        audioSource = GetComponent<AudioSource>();
        //howray();
        if (ButtonType == ButtonTypeEnum.GenTrap)
        {
            opendoor.SetActive(false);
        }
    }

    void Update()
    {
        PushThat();
    }
    void PushThat()
    {
        if (IsButtonHit())
        {
            if(!isClick)
                audioSource.Play();

            if (!isClick && ButtonType == ButtonTypeEnum.Gravity)
            {
                PushBtn();

                isClick = true;
                player.gravityState.PushButton();
            }
            else if (!isClick && ButtonType == ButtonTypeEnum.Flip)
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
    private Vector2 GetRayDirection()
    {
        switch (ButtonTouch)
        {
            case Type.under: return transform.up; // 아래쪽
            case Type.on: return -transform.up; // 위쪽
            case Type.left: return transform.right; // 왼쪽
            case Type.right: return -transform.right; // 오른쪽
            default: return Vector2.zero; // 기본값
        }
    }
    public bool IsButtonHit()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector2 rayDirection = GetRayDirection();
            Vector2 rayOrigin = (Vector2)transform.position + GetOffset(i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, raylength, cubemask | playermask);
            if (hit.collider != null)
            {
                return true; // 레이캐스트가 충돌하면 true 반환
            }
        }
        return false; // 모든 레이에서 충돌이 없으면 false 반환
    }
    private Vector2 GetOffset(int index)
    {
        float offset = (index - (numberOfRays - 1) / 2f) * (transform.localScale.y / numberOfRays); // Y 방향의 스케일에 기반한 오프셋
        return ButtonTouch == Type.left || ButtonTouch == Type.right ? new Vector2(0, offset) : new Vector2(offset, 0); // 방향에 따라 오프셋 조정
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 레이 색상 설정
        DrawRays(); // 레이 그리기 호출
    }
    // 레이 그리기
    public void DrawRays()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector2 rayDirection = GetRayDirection();
            Vector2 rayOrigin = (Vector2)transform.position + GetOffset(i);
            Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection * raylength);
        }
    }

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
