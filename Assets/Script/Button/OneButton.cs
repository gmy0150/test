using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneButton : Button
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsButtonHit()){
            if(!isClick){
                switch(ButtonTouch){
                    case Type.under:
                    buttonValue = transform.position.y - 0.2f;
                    transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);
                    break;
                    case Type.on:
                        buttonValue = transform.position.y + 0.2f;
                    transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);

                    break;
                    case Type.left:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);

                    break;
                    case Type.right:
                        buttonValue = transform.position.x - 0.2f;
                        transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);
                    break;
                }
                isClick = true;
                player.gravityState.PushButton();
            }
        }
    }
    void OnDrawGizmos(){
        base.OnDrawGizmos();
    }
    
}
