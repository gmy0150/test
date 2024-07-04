using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> maplist = new List<GameObject>();
    public int mapCount = 0;
    Player player;
    CameraManager cameraManager;
    float cameraX,cameraY;
    public bool transpos = true;
    private void Awake() {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("OpenDoor");
        foreach(GameObject obj in taggedObjects){
            Renderer objRender = obj.GetComponent<Renderer>();
            if(objRender != null){
                objRender.material.color = Color.red;
            }
        }
    }
    void Start() {
        GameObject[] findMapTag = GameObject.FindGameObjectsWithTag("Map");
        var sortedObjects = findMapTag.OrderBy(obj => obj.name).ToList();
        maplist = sortedObjects;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();

        foreach(var mapobj in maplist){
            mapobj.SetActive(false);
        }
        if(maplist.Count > 0){
            maplist[0].SetActive(true);
        }

    }
    private void Update() {
        switch (mapCount)
        {
            case 0:
                cameraX = maplist[mapCount].transform.position.x + 3;
                cameraY = maplist[mapCount].transform.position.y - 7;
                cameraManager.transform.position = new Vector3(cameraX,cameraY, -10f);

            break;
            default:
            if(!transpos){
                maplist[mapCount].SetActive(true);
                maplist[mapCount - 1].SetActive(false);
                player.transform.position = maplist[mapCount].transform.position;
                cameraX = maplist[mapCount].transform.position.x + 3;
                cameraY = maplist[mapCount].transform.position.y ;
                Debug.Log("작동");
                cameraManager.transform.position = new Vector3(cameraX,cameraY, -10f);
                player.rigid.velocity = Vector2.zero;
                transpos = true;
            }

            break;
        }
    }
}
