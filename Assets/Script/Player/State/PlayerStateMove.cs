using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMove : MonoBehaviour, IPlayerState
{
    private PlayerController controller;
    Vector3 targetPosition;
    Vector3 touchPosition;
    float moveSpeed;
    private Coroutine movementCoroutine;

    
    public void Handle(PlayerController playerController){
        if(!controller){
            controller = playerController;
            
        }
    }
        void Update()
    {  
        
        // 마우스 클릭 입력 감지
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
           
            // 클릭한 위치의 타일맵 좌표 계산
            targetPosition = GetMouseTilePosition();

            // 이동 가능한 위치인지 확인
            if (!IsObstacle(targetPosition))
            {
                if(movementCoroutine != null){
                    StopCoroutine(movementCoroutine);
                }
                // 이동 가능한 위치이므로 이동 시작
                moveSpeed = controller.Speed;
                movementCoroutine = StartCoroutine(MoveToTargetPosition());
            }
        }
    }

    Vector2 GetMouseTilePosition()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int clickPosition = controller.tilemap.WorldToCell(worldPosition);
        Vector3 tilePosition = controller.tilemap.GetCellCenterWorld(clickPosition);
        
        return tilePosition;
        
    }

    IEnumerator MoveToTargetPosition()
    {
        controller.IsMoving = true;

        while (controller.Player.position != targetPosition)
        {
            // 타겟 위치로 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        controller.IsMoving = false;
    }

    bool IsObstacle(Vector2 position)
    {
        // 주어진 위치에 장애물이 있는지 확인
        Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.1f, controller.obstacleLayer);
        return (hitCollider != null);
    }
  
    //모든 애니메이션을 초기화합니다.
    void ResetAnim(){
        controller.Anim.SetBool("Up", false);
        controller.Anim.SetBool("Down", false);
        controller.Anim.SetBool("Right", false);
    }
}

