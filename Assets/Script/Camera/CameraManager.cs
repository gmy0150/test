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
    public float ShakeAmount = 3.0f;
    public float ShakeTime = 2.0f;
    bool isShaking;
    float saveSahke;
    MapManager mapManager;
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
        mapManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MapManager>();
    }

    void Update()
    {
        // getX = transform.position.x;
        // getY = transform.position.y;
        //if(!isShaking){
        //    originPos = transform.position;
        //    transform.localPosition = originPos;
        //}
        if (MapManager.Instance.transpos)
        {
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
        }

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
