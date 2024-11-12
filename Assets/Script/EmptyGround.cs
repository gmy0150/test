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
        Collider2D[] tilemapCollider = tilemap.GetComponentsInChildren<Collider2D>();
        List<Collider2D> colliderlist = new List<Collider2D>(tilemapCollider);
        foreach (Collider2D collider in colliderlist)
        {
            Physics2D.IgnoreCollision(collider, other, true);
            if (other.CompareTag("Player"))
            {
                collider.gameObject.layer = 0;
            }
        }
    }
    void EnableTileMap(Collider2D other)
    {
        // Tilemap에 있는 모든 Collider2D를 가져와 리스트로 저장
        Collider2D[] tilemapColliders = tilemap.GetComponentsInChildren<Collider2D>();
        List<Collider2D> colliderList = new List<Collider2D>(tilemapColliders);

        foreach (Collider2D collider in colliderList)
        {
            Physics2D.IgnoreCollision(collider, other, false);

            if (other.CompareTag("Player"))
            {
                collider.gameObject.layer = 6;
            }
        }
    }
}