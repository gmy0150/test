using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid{get;private set;}
    public PlayerIdleState idleState{get; private set;}
    public PlayerMoveState moveState{get; private set;}
    public PlayerJumpState jumpState {get; private set;}
    public PlayerAirState AirState {get; private set;}
    public PlayerGravityControl gravityState{get; private set;}
    public PlayerFreezeCube freezeState{get; private set;}

    public PlayerControl stateMachine{get;private set;}
     AudioSource audiosource;
    public AudioClip[] audioclips;
    private Quaternion saveMap;
    public int facingDir {get;private set;} = -1;
    private bool facingRight = true;
    private bool gravityRight = false;

    [SerializeField]private float groundCheckDistance;
    [SerializeField]private LayerMask whatIsGround;
    [SerializeField]private LayerMask whatIsBtn;
    [SerializeField]private LayerMask bkfloor;
    [SerializeField]private Transform groundCheck;
    public LayerMask CubeLayer;
    public float cubeCheckDistance;
    bool die = false;
    public float moveSpeed = 8f;
    public Vector3 savePos;
    public float jumpforce;
    MapManager mapManager;
    int gravity = 10;
    int rotate = 90;
    public bool enable;
    void Awake() {  
        stateMachine = new PlayerControl();
        freezeState = new PlayerFreezeCube(this,stateMachine);
        idleState = new PlayerIdleState(this,stateMachine);
        moveState = new PlayerMoveState(this,stateMachine);
        gravityState = new PlayerGravityControl(this,stateMachine);
        jumpState = new PlayerJumpState(this,stateMachine);
        AirState = new PlayerAirState(this,stateMachine);
    }
    void Start(){
        audiosource = GetComponent<AudioSource>();
        savePos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        mapManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapManager>();
        stateMachine.Initalize(idleState);

        Physics2D.gravity = new Vector3(0f,-gravity,0f);
        
    }


    void Update(){
        if (die)
        {
            stateMachine.Initalize(idleState);
            return;
        }
        stateMachine.currentState.Update();

        gravitycontrol();
        if(Input.GetKeyDown(KeyCode.B)){
        }
        if(Input.GetKeyDown(KeyCode.R)){
            if(!die)
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
    int numberOfRays = 3;
    public bool IsGroundDetected()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            // 각 레이의 시작 위치를 계산합니다.
            Vector2 rayOrigin = (Vector2)groundCheck.position + GetOffset(i);
            // 레이를 쏘고 바닥과 충돌하는지 확인합니다.
            if (Physics2D.Raycast(rayOrigin, -transform.up, groundCheckDistance, whatIsGround | CubeLayer | whatIsBtn | bkfloor))
            {
                return true; 
            }
        }
        return false; 
    }
    private Vector2 GetOffset(int index)
    {
        float spacing = 0.3f; // 레이 간격
        return new Vector2(index * spacing, 0); // x축 방향으로 간격을 둡니다.
    }
    //public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up,groundCheckDistance,whatIsGround|CubeLayer|whatIsBtn|bkfloor);
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // 레이 색상 설정
        for (int i = 0; i < numberOfRays; i++)
        {
            Vector2 rayOrigin = (Vector2)groundCheck.position + GetOffset(i);
            Vector2 rayDirection = -transform.up * groundCheckDistance; // 레이 방향과 길이
            Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection); // 레이 그리기
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enable && !die)
        {
            if (other.tag == "nextmap")
            {
                Debug.Log("너 작동하면안돼..");
                Skip();
            }
            if (other.tag == "respawn")
            {
                Debug.Log("너 작동하면안돼..");
                if(!die)
                    Respawn();
            }
        }
    }
    void Skip(){
        gravityState.count = -2;
        GameObject[] activeMaps = GameObject.FindGameObjectsWithTag("Map");
        int number = 0;
        foreach (var map in activeMaps)
        {
            if (map.activeSelf) // 활성화된 오브젝트만 확인
            {
                number = GetNumberAfterUnderscore(map.name);
                Debug.Log("추출된 번호: " + number);
                if (number != -1)
                {
                    Debug.Log("추출된 번호: " + number);
                }
            }
        }
        if (number == 5)
        {
            string curname = SceneManager.GetActiveScene().name;
            int stageNum = GetStageNumber(curname);
            Debug.Log(curname);
            if (stageNum != -1)
            {
                int nextStageNumber = stageNum + 1;
                string NextScene = "Stage_" + nextStageNumber;
                SceneManager.LoadScene(NextScene);
                Debug.Log(NextScene);
            }
        }else
         mapManager.mapCount++;
        mapManager.transpos = false;
        EntryAudio(0);
    }
    public void EntryAudio(int audiocnt)
    {
        audiosource.clip = audioclips[audiocnt];
        audiosource.Play();
    }
    int GetNumberAfterUnderscore(string name)
    {
        // "_" 다음에 나오는 숫자만 추출
        Match match = Regex.Match(name, @"_(\d+)\b");
        Debug.Log(name);
        Debug.Log("2" + match);
        return match.Success ? int.Parse(match.Groups[1].Value) : -1;
    }
    int GetStageNumber(string sceneName)
    {
        Match match = Regex.Match(sceneName, @"\d+");
        return match.Success ? int.Parse(match.Value) : -1;
    }
    public void Respawn(){
        die = true;
        rigid.velocity = Vector2.zero;
        DeathCount.CountUp();
        EntryAudio(2);

        StartCoroutine(Reset());
    }
    IEnumerator Reset()
    {
        rigid.velocity = Vector2.zero;

        yield return new WaitForSeconds(1);
        rigid.velocity = Vector2.zero;
        gravityState.count = -2;
        transform.position = savePos;
        mapManager.ResetList() ;
        die = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (enable)
        {
            if (other.transform.tag == "spike")
            {
                Debug.Log("작동?");
                Respawn();
            }
        }
    }
}