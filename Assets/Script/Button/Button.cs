using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    LayerMask cubemask;
    float transY;
    SpriteRenderer render;
    public GameObject opendoor;
    public float raylength = 0.5f;
    void Start()
    {
        cubemask = LayerMask.GetMask("Cube");
        
        render = GetComponent<SpriteRenderer>();
    }

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

        Gizmos.DrawLine(transform.position, transform.position -transform.up * raylength);
    }
    private bool isbutton() =>Physics2D.Raycast(transform.position, -transform.up,raylength,cubemask);
}
