using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JustRunPlayer : MonoBehaviour
{
    private Quaternion saveMap;
    public Rigidbody2D rigid { get; private set; }
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    public bool cubes;
    GameObject saveCube;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    public LayerMask CubeLayer;
    public float cubeCheckDistance;
    bool die = false;

    public float moveSpeed = 8f;
    public Vector3 savePos;
    public float jumpforce;
    MapManager mapManager;
    Camera cam;
    public bool enable;
    void Start()
    {
        cam = Camera.main;
        savePos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        mapManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapManager>();
    }

    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, -10);

        rigid.velocity = new Vector2(moveSpeed * facingDir, rigid.velocity.y);
  
        if (Input.GetButtonDown("Vertical") && IsGroundDetected())
        {
            rigid.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (enable)
        {
            if (other.tag == "nextmap")
            {
                Skip();
            }
            if (other.tag == "respawn")
            {
                Respawns();
            }
        }
    }*/

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
                Debug.Log("너 작동하면안ㅇㅁㄴㅇㅁㄴㅇ..");
                Respawn();
            }
        }
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, -transform.up, groundCheckDistance, whatIsGround | CubeLayer);
    public bool isCube() => Physics2D.Raycast(transform.position, transform.right, cubeCheckDistance, CubeLayer);
    public GameObject CheckCube()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, cubeCheckDistance, CubeLayer);
        Debug.DrawRay(transform.position, transform.right * cubeCheckDistance);

        if (hit.collider != null)
        {
            if (IsGroundDetected())
                rigid.velocity = Vector2.zero;
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    void FlipInit()
    {
        facingDir = 1;
        facingRight = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !facingRight;
    }
    public void FlipX()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = !facingRight;
    }

    public void FlipController()
    {
        if (!facingRight)
        {
            FlipX();
        }
        else if ( facingRight)
        {        
            FlipX();
        }

    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rigid.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController();
    }
    void FreezeCube(GameObject cube)
    {
        Rigidbody2D rigid = cube.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            cubes = true;
        }
    }
    void UnFreezeCube()
    {
        Rigidbody2D rigid = saveCube.GetComponent<Rigidbody2D>();
        if (rigid != null)
        {
            rigid.constraints = RigidbodyConstraints2D.None;
            cubes = false;
        }
    }
    /*void Skips()
    {
        FlipInit();
        mapManager.PlayerTranspos();
    }*/

    void Skip()
    {
        FlipInit();

        GameObject[] activeMaps = GameObject.FindGameObjectsWithTag("Map");
        int number = 0;
        foreach (var map in activeMaps)
        {
            if (map.activeSelf) // 활성화된 오브젝트만 확인
            {
                number = GetNumberAfterUnderscore(map.name);
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
        }
        else
            mapManager.mapCount++;
        mapManager.transpos = false;
    }
    int GetNumberAfterUnderscore(string name)
    {
        // "_" 다음에 나오는 숫자만 추출
        Match match = Regex.Match(name, @"_(\d+)$");
        return match.Success ? int.Parse(match.Groups[1].Value) : -1;
    }
    int GetStageNumber(string sceneName)
    {
        Match match = Regex.Match(sceneName, @"\d+");
        return match.Success ? int.Parse(match.Value) : -1;
    }
    /*public void Respawns()
    {
        transform.position = savePos;
        mapManager.ResetList();
        FlipInit();
    }*/

    public void Respawn()
    {
        if (!die)
        {
            die = true;
            rigid.velocity = Vector2.zero;
            StartCoroutine(Reset());
            DeathCount.CountUp();
        }
    }
    IEnumerator Reset()
    {
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        FlipInit();
        rigid.velocity = Vector2.zero;
        transform.position = savePos;
        mapManager.ResetList();
        die = false;
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (enable)
        {
            if (other.transform.tag == "spike")
            {
                Respawn();
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D other)
    {
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
