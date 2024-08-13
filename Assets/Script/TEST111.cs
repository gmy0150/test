using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST111 : MonoBehaviour
{
    public Transform characterTransform; // 캐릭터의 Transform
    public float angle = 15f; // 레이캐스트의 각도
    public float distance = 10f; // 레이캐스트 거리

    public LayerMask targetLayer; // 적들이 있는 레이어
    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
         


        // 기준 방향 (캐릭터의 오른쪽 방향)
        Vector2 baseDirection = characterTransform.right;

        // 위쪽 방향을 회전시킴 (Z 축 기준 2D 회전)
        Vector2 upperDirection = Quaternion.Euler(0, 0, angle) * baseDirection;

        // 아래쪽 방향을 회전시킴 (Z 축 기준 2D 회전)
        Vector2 lowerDirection = Quaternion.Euler(0, 0, -angle) * baseDirection;

        // 레이캐스트를 발사할 시작점
        Vector2 origin = characterTransform.position;

        // 디버그용 레이 그리기
        Debug.DrawRay(origin, upperDirection * distance, Color.red);
        Debug.DrawRay(origin, lowerDirection * distance, Color.blue);

        // 두 레이 사이에 있는 모든 오브젝트 추적
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, baseDirection, distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Vector2 toHit = hit.point - origin;
                float angleToHit = Vector2.SignedAngle(baseDirection, toHit);

                if (angleToHit >= -angle && angleToHit <= angle)
                {
                    // 삼각형 내부에 있는 경우
                    Debug.Log("Hit: " + hit.collider.name);
                }
            }
        }
    }
}
