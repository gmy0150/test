using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        rigid.velocity = new Vector2( moveSpeed * facingDir, rigid.velocity.y);
        if (Input.GetButtonDown("Vertical") && IsGroundDetected())
        {
            rigid.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (enable)
        {
            if (other.tag == "nextmap")
            {
                Skips();
            }
            if (other.tag == "respawn")
            {
                Respawns();
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
    }
    public void FlipX()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
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
    void Skips()
    {
        FlipInit();
        mapManager.PlayerTranspos();
    }
    public void Respawns()
    {
        transform.position = savePos;
        mapManager.ResetList();
        FlipInit();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enable)
        {
            if (other.transform.tag == "spike")
            {
                Respawns();
            }
        }
    }
}
