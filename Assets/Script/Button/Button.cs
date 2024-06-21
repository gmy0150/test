using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    LayerMask cubemask;
    float transY;
    SpriteRenderer render;
    public GameObject opendoor;
    void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isbutton()){
            transY = 9.5f;
            float a = transform.position.y + 0.2f;

            transform.position = new Vector3(transform.position.x,transY,transform.position.z);
            
            render.material.color = Color.red;
            if(gameObject !=null){
            opendoor.SetActive(false);

            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        // 레이캐스트를 그릴 위치와 방향 설정
        Gizmos.DrawLine(transform.position, transform.position -transform.up * 1f);
    }
    private bool isbutton() =>Physics2D.Raycast(transform.position, -transform.up,1f,cubemask);
}
