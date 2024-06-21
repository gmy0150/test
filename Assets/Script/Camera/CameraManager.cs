using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance{get;private set;}
    GameObject player;
    Vector3 originPos;
    float getX,getY;
    public float ShakeAmount = 3.0f;
    public float ShakeTime = 2.0f;
    bool isShaking;
    float saveSahke;
    void Awake() {
        if(Instance == null){
            Instance = this;
            originPos = transform.position;
        }    
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        saveSahke = ShakeTime;
    }

    void Update()
    {
        getX = player.transform.position.x;
        getY = player.transform.position.y;
        originPos = new Vector3(getX,getY,-10f);
        transform.localPosition = originPos;
    }
    public void ShakeCamera(){
        if(!isShaking){
            ShakeTime = saveSahke;
            isShaking = true;
            StartCoroutine(PerformShake());
        }
    }
    private IEnumerator PerformShake() {
        while (ShakeTime > 0) {
            transform.position = Random.insideUnitSphere * ShakeAmount + originPos;
            ShakeTime -= Time.deltaTime;
            yield return null; 
        }
        transform.position = originPos;
        isShaking = false;
    }
}
