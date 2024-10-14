using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public TrapType TrapTypes;

    public enum TrapType { Move, Disappear, strenlth }
    [SerializeField] public Vector3 target;
    [SerializeField] public float duration = 2.0f;
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
    private void Awake()
    {
        if (TrapTypes == TrapType.strenlth)
        {
            if (scaleRight == true)
                targetobj.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Start()
    {
        if (targetobj == null)
        {
            targetobj = this.gameObject;
        }
        startPos = targetobj.transform.localPosition;
        initScale = targetobj.transform.localScale;
    }

    private void Update()
    {
        if (TrapTypes == TrapType.Move && Detect)
        {
            float elasped = (Time.time - startTime) / duration;
            targetobj.transform.localPosition = Vector3.Lerp(startPos, target, elasped);
        }
        if (TrapTypes == TrapType.strenlth && Detect && !scaled)
        {
            timer += Time.deltaTime;
            if (timer > maintainTime)
            {
                scaling = true;
            }
        }

        if (scaling && !scaled)
        {
            targetobj.transform.localScale = Vector3.Lerp(
                targetobj.transform.localScale,
                target,
                Time.deltaTime * duration 
            );

            if (Vector3.Distance(targetobj.transform.localScale, target) < 0.01f)
            {
                targetobj.transform.localScale = target; 
                scaling = false;
                scaled = true; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !Detect)
        {
            Detect = true;
            startTime = Time.time;
        }
    }
    public void ResetTrap()
    {
        Detect = false;
        scaled = false;
        scaling = false;
        timer = 0;
        startTime = 0;
        targetobj.transform.localPosition = startPos;
        targetobj.transform.localScale = initScale;
    }
}


