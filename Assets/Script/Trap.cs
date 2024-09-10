using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public TrapType TrapTypes;

    public enum TrapType { Move, Disappear }
    [SerializeField]public Vector3  target;
    [SerializeField]public float duration = 2.0f;
    float startTime;
    Vector3 startPos;
    bool Detect;
    public GameObject targetobj;
    void Start()
    {
        if(targetobj == null){
            targetobj = this.gameObject;
        }
        startPos = targetobj.transform.position;
    }

    private void Update() {
        if(TrapTypes == TrapType.Move&&Detect){
            float elasped = (Time.time - startTime) / duration;
            targetobj.transform.position = Vector3.Lerp(startPos,target,elasped);

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
        startTime = 0;
        targetobj.transform.position = startPos;
        Debug.Log("현재위치"+targetobj.transform.position);
    }

}


