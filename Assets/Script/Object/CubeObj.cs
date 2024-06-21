using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    [SerializeField]private float groundCheckDistance = 0.5f;
    private LayerMask whatIsGround;
    [SerializeField]private Transform groundCheck;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("Floor");   
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        // 레이캐스트를 그릴 위치와 방향 설정
        Gizmos.DrawLine(groundCheck.position, groundCheck.position -transform.up * groundCheckDistance);
    }
    private bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,whatIsGround);
    void Update()
    {
        transform.up = -Physics2D.gravity.normalized;
        if(IsGroundDetected()){
            rigid.velocity = Vector2.zero;   
        }
    }
}
