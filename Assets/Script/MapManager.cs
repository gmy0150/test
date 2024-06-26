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
    public bool transpos = true;
    void Start() {
        GameObject[] findMapTag = GameObject.FindGameObjectsWithTag("Map");
        var sortedObjects = findMapTag.OrderBy(obj => obj.name).ToList();
        maplist = sortedObjects;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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
            default:
            if(!transpos){
                maplist[mapCount].SetActive(true);
                maplist[mapCount - 1].SetActive(false);
                player.transform.position = maplist[mapCount].transform.position;
                player.rigid.velocity = Vector2.zero;
                transpos = true;
                
            }

            break;
        }
    }
}
