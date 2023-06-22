using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed;
    public Camera mainCamera;
    Vector3 targetPosition;
    Vector3 touchPosition;
    public SpriteRenderer playerCharactor;
    Animator anim;

    Transform player;
    bool isMoving;

    private void Awake() {
        player = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update(){
        /*if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector3 clickPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3Int tilePosition = tilemap.WorldToCell(clickPosition);
            targetPosition = tilemap.GetCellCenterWorld(tilePosition);
            isMoving = true;
        }*/
        if(Mouse.current.leftButton.isPressed){
            ResetAnim();
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition = tilemap.WorldToCell(clickPosition);
            targetPosition = tilemap.GetCellCenterWorld(tilePosition);
            isMoving = true;
        }
        if (isMoving)
        {
            // 이동 방향과 거리 계산
            Vector3 direction = targetPosition - player.position;
            float distance = moveSpeed * Time.deltaTime;
            Debug.Log(direction.normalized);
            // 유닛의 이동 보간 처리
            if (direction.magnitude <= distance)
            {
                // 목표 위치에 도달한 경우 이동 중지
                player.position = targetPosition;
                isMoving = false;
                ResetAnim();
            }
            else
            {
                if(direction.normalized.x <= 0.1 && direction.normalized.x >= -0.1f  && direction.normalized.y > 0){
                    
                    anim.SetBool("Up", true);
                }
                else if(direction.normalized.x <= 0.1 && direction.normalized.x >= -0.1f && direction.normalized.y < 0){
                    
                    anim.SetBool("Down", true);
                }
                
                else{
                    if(direction.normalized.x < 0){
                        playerCharactor.flipX = true;
                        anim.SetBool("Right", true);
                    }
                    else if(direction.normalized.x > 0){
                        playerCharactor.flipX = false;
                        anim.SetBool("Right", true);
                    }
                }
                // 이동 방향과 거리에 따라 유닛 이동
                player.position += direction.normalized * distance;
            }
        }     
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.transform.CompareTag("Map")){
            player.position = player.position;
            isMoving = false;   
        }
    }

    void ResetAnim(){
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
    }
}
