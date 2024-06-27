using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalObj : MonoBehaviour
{
    [SerializeField]private float groundCheckDistance = 0.5f;

    // CameraManager Camera;
    private LayerMask whatIsGround;
    private LayerMask BrokenGround;
    [SerializeField]private Transform groundCheck;

    Vector2 startPosition;
    private float distance = 3.0f;
    Rigidbody2D rigid;
    bool isFalling;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("Floor");
        BrokenGround = LayerMask.GetMask("BrokenFloor");
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        // 레이캐스트를 그릴 위치와 방향 설정
        Gizmos.DrawLine(groundCheck.position, groundCheck.position -transform.up * groundCheckDistance);
    }
    private void BrokenGroundDetected() {
        if(isFalling){
            RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,BrokenGround);
            RaycastHit2D BrokenDistance = Physics2D.Raycast(groundCheck.position, Physics2D.gravity.normalized,Mathf.Infinity,BrokenGround);
            if (BrokenDistance.collider != null) {
                float collisionDistance = Vector2.Distance(startPosition, BrokenDistance.point);

                if (collisionDistance >= distance) {
                    if(groundHit.collider !=null){
                        CameraManager.Instance.ShakeCamera();
                        Destroy(groundHit.collider.gameObject);
                    }
                }
            }
        }else if(!isFalling && transform.hasChanged){
            startPosition = transform.position;
            isFalling = true;
        }
        transform.hasChanged = false;
    }
    private bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,whatIsGround);
    void Update()
    {
        transform.up = -Physics2D.gravity.normalized;
        if(IsGroundDetected()){
            rigid.velocity = Vector2.zero;   
        }
        BrokenGroundDetected();
        if(rigid.velocity.y == 0 && rigid.velocity.x == 0){
            isFalling = false;
        }
    }
}
