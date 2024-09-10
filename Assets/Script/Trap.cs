using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public TrapType TrapTypes;

    public enum TrapType { Move, Disappear ,strenlth}
    [SerializeField]public Vector3  target;
    [SerializeField]public float duration = 2.0f;
    public float maintainTime = 2.0f;
    public bool scaleRight = true;
    float startTime;
    Vector3 startPos;
    Vector3 initScale;
    bool Detect;
    public GameObject targetobj;
    bool scaled;
    bool scaling;
    float timer;
    private void Awake() {
        if(TrapTypes == TrapType.strenlth){
            if(scaleRight == true)
                targetobj.transform.rotation = Quaternion.Euler(0,180,0);
            
        }
    }
    void Start()
    {
        if(targetobj == null){
            targetobj = this.gameObject;
        }
        startPos = targetobj.transform.position;
        initScale = targetobj.transform.localScale;
    }

    private void Update() {
        if(TrapTypes == TrapType.Move&&Detect){
            float elasped = (Time.time - startTime) / duration;
            targetobj.transform.position = Vector3.Lerp(startPos,target,elasped);
        }
         if (TrapTypes == TrapType.strenlth && Detect && !scaled)
        {
            // 스케일을 천천히 증가시키는 로직을 활성화
            timer += Time.deltaTime;
            if(timer > maintainTime){
                scaling = true;

            }
        }

        if (scaling && !scaled)
        {
            // 현재 스케일과 목표 스케일을 비교하여 천천히 증가
            targetobj.transform.localScale = Vector3.Lerp(
                targetobj.transform.localScale, 
                target, 
                Time.deltaTime * duration // 시간에 따라 스케일 증가 속도 조절
            );

            // 피벗에 상관없이 오른쪽이나 왼쪽으로만 스케일을 변경하기 위해 위치 보정
            // Vector3 oldPosition = targetobj.transform.position;
            // if (scaleRight)
            // {
            //     targetobj.transform.position = new Vector3(
            //         oldPosition.x + (targetobj.transform.localScale.x - initScale.x) / 2,
            //         oldPosition.y,
            //         oldPosition.z
            //     );
            // }
            // else
            // {
            //     targetobj.transform.position = new Vector3(
            //         oldPosition.x - (targetobj.transform.localScale.x - initScale.x) / 2,
            //         oldPosition.y,
            //         oldPosition.z
            //     );
            // }

            // 목표 스케일에 거의 도달하면 스케일링을 중지
            if (Vector3.Distance(targetobj.transform.localScale, target) < 0.01f)
            {
                targetobj.transform.localScale = target; // 최종 스케일 적용
                scaling = false;
                scaled = true; // 스케일 완료 표시
                Debug.Log("스케일 완료");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"&&!Detect){
            Detect = true;
            startTime = Time.time;
        }
    }
    public void ResetTrap(){
        Detect = false;
        scaled = false;
        scaling = false;
        timer = 0;
        startTime = 0;
        targetobj.transform.position = startPos;
        targetobj.transform.localScale = initScale;
        Debug.Log("현재위치"+targetobj.transform.position);
    }

}


