using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Quaternion saveMap;
    public Rigidbody2D rigid{get;private set;}
    public PlayerIdleState idleState{get; private set;}
    public PlayerMoveState moveState{get; private set;}
    public PlayerGravityControl gravityState{get; private set;}
    public PlayerControl stateMachine{get;private set;}
    [SerializeField]private float groundCheckDistance;
    [SerializeField]private LayerMask whatIsGround;
    [SerializeField]private Transform groundCheck;

    int gravity = 10;
    public float moveSpeed = 8f;
    int rotate = 90;
    float xInput;
    private void Awake() {
        stateMachine = new PlayerControl();

        idleState = new PlayerIdleState(this,stateMachine);
        moveState = new PlayerMoveState(this,stateMachine);
        gravityState = new PlayerGravityControl(this,stateMachine);
        
    }
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        stateMachine.Initalize(idleState);

        Physics2D.gravity = new Vector3(0f,-gravity,0f);
        
    }

    void Update(){
        stateMachine.currentState.Update();
        switch (gravityState.count) {
            case 1:
                Physics2D.gravity = new Vector2(gravity, 0f);
                break;
            case 2:
                Physics2D.gravity = new Vector2(0f, gravity);
                break;
            case 3:
                Physics2D.gravity = new Vector2(-gravity, 0f);
                break;
            case 4:
                Physics2D.gravity = new Vector2(0f, -gravity);
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotate * gravityState.count);
    }
    public void SetVelocity(float _xVelocity,float _yVelocity){
        rigid.velocity = new Vector2(_xVelocity,_yVelocity);
    }
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,whatIsGround);
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        // 레이캐스트를 그릴 위치와 방향 설정
        Gizmos.DrawLine(groundCheck.position, groundCheck.position -transform.up * groundCheckDistance);
    }
}
