
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> maplist = new List<GameObject>();
    public int mapCount = 0;
    Player player;
    CameraManager cameraManager;
    float cameraX,cameraY;
    public bool transpos = true;
    private Dictionary<GameObject, Vector3> cubePositions; // 큐브의 위치를 저장할 딕셔너리
    private List<GameObject> cubes; // 큐브 오브젝트 리스트
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
        cubePositions = new Dictionary<GameObject, Vector3>();

        cubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cube"));
        SaveCubePositions();
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
    public void SaveCubePositions()
    {
        cubePositions.Clear();
        foreach (var cube in cubes)
        {
            if (cube != null && !cubePositions.ContainsKey(cube))
            {
                cubePositions[cube] = cube.transform.position;
            }
        }
    }
    public void ResetCubePositions()
    {
        foreach (var kvp in cubePositions)
        {
            if (kvp.Key != null)
            {
                kvp.Key.transform.position = kvp.Value;
                Debug.Log($"큐브 {kvp.Key.name}의 위치를 재설정했습니다.");
            }
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
                player.savePos = player.transform.position;
                cameraX = maplist[mapCount].transform.position.x + 3;
                cameraY = maplist[mapCount].transform.position.y ;
                cameraManager.transform.position = new Vector3(cameraX,cameraY, -10f);
                player.rigid.velocity = Vector2.zero;
                SaveCubePositions();
                transpos = true;
            }

            break;
        }
    }
}
