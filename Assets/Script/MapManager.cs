
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
    private Dictionary<GameObject, Vector3> trapPositions;
    private Dictionary<GameObject, Vector3> cubePositions;
    public List<Trap> traps;
    private List<GameObject> cubes;
    private List<GameObject> button;
    public bool GravityRoom;
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
        cubePositions = new Dictionary<GameObject, Vector3>();
        cubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cube"));
        trapPositions = new Dictionary<GameObject, Vector3>();
        int trapLayer = LayerMask.NameToLayer(trapLayerName);
        traps = FindTrapsOnLayer(trapLayer);
        SaveCubePositions();
        SaveTrapPositions();
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
            }
        }
    }
    public void SaveTrapPositions()
    {
        trapPositions.Clear();
        foreach (var trap in traps)
        {
            if (trap != null && !trapPositions.ContainsKey(trap.gameObject))
            {
                trapPositions[trap.gameObject] = trap.transform.position;

            }
        }
    }
    public void ResetTrapPositions()
    {
        foreach (var kvp in trapPositions)
        {
            if (kvp.Key != null)
            {
                Trap trap = kvp.Key.GetComponent<Trap>();

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
    public void ResetButton()
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
                    SaveCubePositions();
                    transpos = true;
                }
                break;
        }
    }
}
