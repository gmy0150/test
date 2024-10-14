using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtton : Button
{
    // Start is called before the first frame update
    float savevalue;
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(isbutton()){
            Debug.Log("나와");
            if(!isClick){
                switch(ButtonTouch){
                    case Type.under:
                    savevalue = transform.position.y;
                    buttonValue = transform.position.y - 0.2f;
                    // transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);
                    break;
                    case Type.on:
                    savevalue = transform.position.y;
                        buttonValue = transform.position.y + 0.2f;
                    // transform.position = new Vector3(transform.position.x,buttonValue,transform.position.z);

                    break;
                    case Type.left:
                    savevalue = transform.position.y;
                        buttonValue = transform.position.x - 0.2f;
                        // transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);

                    break;
                    case Type.right:
                    savevalue = transform.position.y;
                        buttonValue = transform.position.x - 0.2f;
                        // transform.position = new Vector3(buttonValue,transform.position.y,transform.position.z);


                    break;
                }
                isClick = true;
                if(gameObject !=null){
                    opendoor.SetActive(false);
                }
                Debug.Log("오름");
            }

        }else if(isClick){
            isClick = false;
            // player.gravityState.ButtonOut();
            if(gameObject !=null){
                    opendoor.SetActive(true);
                }
            // switch(ButtonType){
            //     case Type.under:
            //     case Type.on:
            //         transform.position = new Vector3(transform.position.x,savevalue,transform.position.z);
            //     break;
            //     case Type.right:
            //     case Type.left:
            //         transform.position = new Vector3(savevalue,transform.position.y,transform.position.z);
            //     break;
            // }
        }
    }
    void OnDrawGizmos(){
        base.OnDrawGizmos();
    }
}
