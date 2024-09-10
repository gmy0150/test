using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmptyGround : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
        private void Start() 
    {
        
    }

    private void Update()
    {
        // 특정 조건에서 타일을 업데이트 (예: 게임 로직에 따라)
        //UpdateEmptyTiles();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) // 충돌한 오브젝트의 태그를 확인
        {
            Debug.Log("Collision detected with Floor object: " + other.name);
            DisableTilemapCollider();
        }else{
            EnableTileMap();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            EnableTileMap();
        }
    }

    void DisableTilemapCollider()
    {
        Collider2D tilemapCollider = tilemap.GetComponent<Collider2D>();
        if (tilemapCollider != null)
        {
            tilemapCollider.enabled = false; 
        }
    }
    void EnableTileMap(){
    {
        Collider2D tilemapCollider = tilemap.GetComponent<Collider2D>();
            if (tilemapCollider != null)
            {
                tilemapCollider.enabled = true;
            }
        }
    }
}