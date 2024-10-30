
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public List<GameObject> maplist = new List<GameObject>();
    public int mapCount = 0;
    Player player;
    JustRunPlayer runplayer;
    CameraManager cameraManager;
    float cameraX, cameraY;
    public string trapLayerName;
    public bool transpos = false;
    public bool GravityRoom;
    public struct Data
    {
        public GameObject obj;
        public Vector3 position;
    }
     List<Trap> trapList = new List<Trap>();
    private List<Data> cubes = new List<Data>();
    private List<Data> Coins = new List<Data>();
    private List<GameObject> button;
     List<Data> trapPositions = new List<Data>();
    private List<Data> Brkfloor = new List<Data>();

    private void Awake()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("OpenDoor");
        foreach (GameObject obj in taggedObjects)
        {
            Renderer objRender = obj.GetComponent<Renderer>();
            if (objRender != null)
            {
                objRender.material.color = Color.red;
            }
        }
        Instance = this;
    }
    void Start()
    {
        button = new List<GameObject>(GameObject.FindGameObjectsWithTag("Button"));
        int trapLayer = LayerMask.NameToLayer(trapLayerName);
        trapList = FindTrapsOnLayer(trapLayer);
        GameObject[] findMapTag = GameObject.FindGameObjectsWithTag("Map");
        var sortedObjects = findMapTag.OrderBy(obj => obj.name).ToList();
        maplist = sortedObjects;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //runplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<JustRunPlayer>();
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();

        foreach (var mapobj in maplist)
        {
            mapobj.SetActive(false);
        }
        if (maplist.Count > 0)
        {
            maplist[0].SetActive(true);
            SaveList();
        }
    }
    public void SaveList()
    {
        SaveBK();
        SaveCoin();
        SaveCubePositions();
        SaveTrapPositions();
    }
    public void ResetList()
    {
        ResetBk();
        ResetCoin();
        ResetCubePositions();
        ResetButton();
        ResetTrapPositions();
    }
    private void SaveBK()
    {
        Brkfloor.Clear();
        GameObject[] BkList = GameObject.FindGameObjectsWithTag("Floor");
        foreach (var bk in BkList)
        {
            if (bk != null)
            {
                Brkfloor.Add(new Data { obj = bk, position = bk.transform.position });
            }
        }
    }
    void ResetBk()
    {
        foreach (var kvp in Brkfloor)
        {
            if (kvp.obj != null)//오브젝트 캐스팅 > 게임오브젝ㅌ틑
            {
                kvp.obj.SetActive(true);
            }
        }
    }
    private void SaveCoin()
    {
        Coins.Clear();
        GameObject[] coinslist = GameObject.FindGameObjectsWithTag("Coin");
        foreach (var Coin in coinslist)
        {
            if (Coin != null)
            {
                Coins.Add(new Data { obj = Coin ,position = Coin.transform.position});
            }
        }
    }
    private void ResetCoin()
    {
        foreach (var kvp in Coins)
        {
            if (kvp.obj is GameObject Coins)//오브젝트 캐스팅 > 게임오브젝ㅌ틑
            {
                Coins.SetActive(true);
                Debug.Log("확인중");
            }
        }
    }
    private void SaveCubePositions()
    {
        cubes.Clear();
        GameObject[] cubelist = GameObject.FindGameObjectsWithTag("Cube");
        foreach (var cube in cubelist)
        {
            if (cube != null)
            {
                cubes.Add(new Data { obj = cube ,position = cube.transform.position});
            }
        }
    }
    private void ResetCubePositions()
    {
        foreach (var kvp in cubes)
        {
            if (kvp.obj != null)
            {
                kvp.obj.gameObject.SetActive(true);
                kvp.obj.gameObject.transform.position = kvp.position;

                MetalObj met = kvp.obj.GetComponent<MetalObj>();
                if (met != null)
                {
                    met.DontFall();
                }
                if (met == null)
                    Debug.Log("없어");
            }
        }
    }
    private void SaveTrapPositions()
    {
        trapPositions.Clear();
        foreach (var trap in trapList)
        {
            if (trap != null)
            {
                trapPositions.Add(new Data{obj = trap.gameObject,position = trap.transform.position });
            }
        }
    }
    private void ResetTrapPositions()
    {
        foreach (var kvp in trapPositions)
        {
            if (kvp.obj != null)
            {
                Trap trap = kvp.obj.GetComponent<Trap>();

                if (trap != null)
                {
                    trap.ResetTrap();
                }
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
                    traps.Add(trapComponent);
                }
            }
        }
        return traps;
    }
    private void ResetButton()
    {
        foreach (var buttonObject in button)
        {
            Button buttonScript = buttonObject.GetComponent<Button>();
            if (buttonScript != null)
            {
                buttonScript.ResetButton();
            }
        }
    }
    private void Update()
    {
        switch (mapCount)
        {
            case 0:
            default:
                if (!transpos)
                {
                    maplist[mapCount].SetActive(true);
                    if(mapCount > 0)
                    maplist[mapCount - 1].SetActive(false);
                    if (maplist[mapCount].name.Contains("Gravity"))
                    {
                        GravityRoom = true;
                    }
                    else
                    {
                        GravityRoom = false;
                    }
                    if (player.enabled)
                    {
                        player.transform.position = maplist[mapCount].transform.position;
                        player.savePos = player.transform.position;
                        cameraX = maplist[mapCount].transform.position.x;
                        cameraY = maplist[mapCount].transform.position.y;
                        player.rigid.velocity = Vector2.zero;
                        cameraManager.transform.position = new Vector3(cameraX, cameraY, -10f);
                    }
                    else
                    {
                        runplayer.transform.position = maplist[mapCount].transform.position;
                        runplayer.savePos = runplayer.transform.position;
                        cameraX = runplayer.transform.position.x;
                        cameraY = runplayer.transform.position.y;
                        runplayer.rigid.velocity = Vector2.zero;

                        cameraManager.transform.position = new Vector3(cameraX, cameraY, -10f);
                    }
                    SaveList();
                    transpos = true;
                }
                break;
        }
    }
}
