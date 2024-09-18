
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public List<GameObject> maplist = new List<GameObject>();
    public int mapCount = 0;
    Player player;
    CameraManager cameraManager;
    float cameraX,cameraY;
    public string trapLayerName;
    public bool transpos = true;
    public LayerMask trapLayer;
    private Dictionary<GameObject, Vector3> trapPositions;
    public List<Trap> traps;
    private Dictionary<GameObject, Vector3> cubePositions; // 큐브의 위치를 저장할 딕셔너리
    private List<GameObject> cubes; // 큐브 오브젝트 리스트
    Trap trap1;
    private List<GameObject> button;
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
        button = new List<GameObject>(GameObject.FindGameObjectsWithTag("Button"));
        cubePositions = new Dictionary<GameObject, Vector3>();
        cubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cube"));
        trapPositions = new Dictionary<GameObject,Vector3>();
        int trapLayer = LayerMask.NameToLayer(trapLayerName);
        traps = FindTrapsOnLayer(trapLayer);
        //traps = new List<GameObject>(FindObjectsOnLayer(trapLayer));
        SaveCubePositions();
        SaveTrapPositions();
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

    // Trap의 위치를 저장하는 메서드
    public void SaveTrapPositions()
    {
        trapPositions.Clear();
        foreach (var trap in traps)
        {
            if (trap != null && !trapPositions.ContainsKey(trap.gameObject))
            {
                trapPositions[trap.gameObject] = trap.transform.position;
                Debug.Log("Trap 위치"+trapPositions[trap.gameObject]);

            }
        }
        Debug.Log("Trap 위치가 저장되었습니다.");
    }

    // Trap의 위치를 초기화하는 메서드
    public void ResetTrapPositions()
    {
        foreach (var kvp in trapPositions)
        {
            if (kvp.Key != null)
            {
                Trap trap = kvp.Key.GetComponent<Trap>();
                    Debug.Log("!?!?!");

                if(trap != null){
                    Debug.Log("작동!");
                    trap.ResetTrap();
                }
                //kvp.Key.transform.position = kvp.Value;
                //Debug.Log($"Trap {kvp.Key.name}의 위치를 재설정했습니다.");
                //Debug.Log($"trap{kvp.Key.name}의 위치 이동"+ kvp.Value  );
            }
            else
            {
                Debug.LogWarning("Trap이 null입니다. 위치 재설정이 실패했습니다.");
            }
        }
    }
    private List<Trap> FindTrapsOnLayer(int layer)
    {
        List<Trap> traps = new List<Trap>();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.layer == layer)
            {
                Trap trapComponent = obj.GetComponent<Trap>();
                if (trapComponent != null)
                {
                    traps.Add(trapComponent); // Trap 컴포넌트를 가진 GameObject를 추가
                }
            }
        }

        return traps;
    }
    public void ResetButton(){
        foreach (var buttonObject in button)
        {
            OpenButton buttonScript = buttonObject.GetComponent<OpenButton>();
            if (buttonScript != null)
            {
                buttonScript.ResetButton(); 
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
