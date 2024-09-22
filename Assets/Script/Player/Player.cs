using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Quaternion saveMap;
    public Rigidbody2D rigid{get;private set;}
    public PlayerIdleState idleState{get; private set;}
    public PlayerMoveState moveState{get; private set;}
    public PlayerJumpState jumpState {get; private set;}
    public PlayerAirState AirState {get; private set;}
    public PlayerGravityControl gravityState{get; private set;}
    public PlayerFreezeCube freezeState{get; private set;}

    public PlayerControl stateMachine{get;private set;}
    public int facingDir {get;private set;} = -1;
    private bool facingRight = true;
    private bool gravityRight = false;

    [SerializeField]private float groundCheckDistance;
    public float cubeCheckDistance;
    [SerializeField]private LayerMask whatIsGround;
    public LayerMask CubeLayer;
    [SerializeField]private Transform groundCheck;

    int gravity = 10;
    public float moveSpeed = 8f;
    public Vector3 savePos;
    public float jumpforce;
    MapManager mapManager;
    int rotate = 90;
    void Awake() {  
        stateMachine = new PlayerControl();
        freezeState = new PlayerFreezeCube(this,stateMachine);
        idleState = new PlayerIdleState(this,stateMachine);
        moveState = new PlayerMoveState(this,stateMachine);
        gravityState = new PlayerGravityControl(this,stateMachine);
        jumpState = new PlayerJumpState(this,stateMachine);
    }
    void Start(){
        savePos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        mapManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapManager>();
        stateMachine.Initalize(idleState);

        Physics2D.gravity = new Vector3(0f,-gravity,0f);
        
    }


    void Update(){
        stateMachine.currentState.Update();

        gravitycontrol();
        if(Input.GetKeyDown(KeyCode.B)){
        }
        if(Input.GetKeyDown(KeyCode.R)){
            Respawn();
        }
    }
    int y = 1;
    int x = 0;
    float rotatevalue;
    public void gravitycontrol(){
        switch (gravityState.count) {
            case 1:
                Physics2D.gravity = new Vector2(gravity, 0f);
                gravityRight = false;
                x = 1;
                y = 0;
                rotatevalue = 1;
                break;
            case 2:
                Physics2D.gravity = new Vector2(0f, gravity);
                gravityRight = true;
                x = 0;
                y = 1;
                rotatevalue = 2;
                break;
            case -1:
                Physics2D.gravity = new Vector2(-gravity, 0f);
                gravityRight = true;
                x = 1;
                y = 0;
                rotatevalue = 3;
                break;
            case -2:
                Physics2D.gravity = new Vector2(0f, -gravity);
                gravityRight = false;
                x = 0;
                y = 1;
                rotatevalue = 4;
                break;
        }
        transform.rotation = Quaternion.Euler(x*FlipValue(), y*FlipValue(), rotate * rotatevalue);
        
    }
    public void SetVelocity(float _xVelocity,float _yVelocity){
        rigid.velocity = new Vector2(_xVelocity,_yVelocity);
        FlipController();
    }
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,whatIsGround|CubeLayer);
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundCheck.position, groundCheck.position -transform.up * groundCheckDistance);

        Gizmos.DrawLine(transform.position, transform.position + transform.right * cubeCheckDistance);

    }
    private void FlipX(){
        if(gravityState.count ==2 || gravityState.count == -2){
            facingDir = facingDir * -1;
            facingRight = !facingRight;
        }
    }
    private void FlipY(){
        if(gravityState.count ==1 || gravityState.count == -1){
            facingDir = facingDir * -1;
            facingRight = !facingRight;
        }
    }
    public void FlipController(){
        if(rigid.velocity.x > 0 && !facingRight){//양수로 가고 facingRight가 false일 때 기본이 false니까 오른쪽을 보고있을 때, 오른쪽 이동일 때
            FlipX();
        }else if(rigid.velocity.x < 0 && facingRight){//음수로 가고 facingRight가 true일 때 왼쪽이동 transform.flip을 쓰면 문제가 되는 부분이 많으니까 이렇게 처리해서 하는 부분 이건 공부를 해야겠다.           
            FlipX();
        }else if(rigid.velocity.y > 0 && !facingRight){
            FlipY();
        }else if(rigid.velocity.y < 0 && facingRight){
            FlipY();
        }
    }
    private float FlipValue(){
        if(facingRight && !gravityRight){
            return 0f;
        }
        else if(!facingRight && !gravityRight){
            return 180f;
        }
        else if(facingRight && gravityRight){
            return 180f;
        }else if(!facingRight && gravityRight){
            return 0;
        }
        else{
            return 0;
        }
    }
    public bool isCube()=> Physics2D.Raycast(transform.position, transform.right,cubeCheckDistance,CubeLayer);
    public GameObject CheckCube(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, cubeCheckDistance, CubeLayer);
        Debug.DrawRay(transform.position, transform.right*cubeCheckDistance);

        if(hit.collider != null){
            switch(gravityState.count){
                default:
                if(IsGroundDetected())
                    rigid.velocity = Vector2.zero;
            break;
            }
            return hit.collider.gameObject;

        }
        else{
            return null;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "nextmap"){
            Skip();
        }
        if(other.tag == "respawn"){
            Respawn();
        }
    }
    void Skip(){
        gravityState.count = -2;
        mapManager.mapCount++;
        mapManager.transpos = false;
    }
    void Respawn(){
        gravityState.count = -2;
        transform.position = savePos;
        mapManager.ResetCubePositions();
        mapManager.ResetTrapPositions();
        mapManager.ResetButton();
    }
    void getDamage(){
        Vector2 pushDir = -Vector2.right * facingDir;
        rigid.AddForce(pushDir * 15f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "spike"){
            getDamage();
            Debug.Log("?");
        }
    }
}