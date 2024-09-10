using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmptyGround : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 참조
    public LayerMask playerLayer; // 플레이어 레이어
    public LayerMask cubeLayer; // 큐브 레이어
        private void Start() 
    {
        
    }

    private void Update()
    {
        // 특정 조건에서 타일을 업데이트 (예: 게임 로직에 따라)
        //UpdateEmptyTiles();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")||other.CompareTag("Cube")) // 충돌한 오브젝트의 태그를 확인
        {
            Debug.Log("Collision detected with Floor object: " + other.name);
            DisableTilemapCollider(other);
        }else{
            EnableTileMap(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")||other.CompareTag("Cube"))
        {
            EnableTileMap(other);
        }
    }

    void DisableTilemapCollider(Collider2D other)
    {
        Collider2D tilemapCollider = tilemap.GetComponent<Collider2D>();
        if (tilemapCollider != null)
        {
            Physics2D.IgnoreCollision(tilemapCollider,other,true);
        }
    }
    void EnableTileMap(Collider2D other){
    {
        Collider2D tilemapCollider = tilemap.GetComponent<Collider2D>();
            if (tilemapCollider != null)
            {
            Physics2D.IgnoreCollision(tilemapCollider,other ,false);
            }
        }
    }
}