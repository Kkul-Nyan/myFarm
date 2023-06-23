using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerStateMove : MonoBehaviour, IPlayerState
{
    private PlayerController controller;

    public Tilemap tilemap;
    public float moveSpeed;
    [SerializeField]
    Camera mainCamera;
    Vector3 targetPosition;
    Vector3 touchPosition;
    [SerializeField]
    SpriteRenderer playerCharactor;
    Animator anim;

    Transform player;
    bool isMoving;

    private void Awake() {
        player = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        playerCharactor = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start() {
        moveSpeed = controller.speed;
    }

    public void Handle(PlayerController playerController){}
    void Update(){
        // 제작중에 화면터치를 이용하지 않고 마우스를 이용하고 있기 때문에 막아둔 상태입니다
        /*
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            Vector3Int tilePosition = tilemap.WorldToCell(clickPosition);
            targetPosition = tilemap.GetCellCenterWorld(tilePosition);
            isMoving = true;
        }
        */

        //마우스 버튼으로 화면 클릭시 그 클릭한 위치의 좌표를 입력받습니다.
        if(Mouse.current.leftButton.isPressed){
            ResetAnim();
            //InputManager에서 Value를 가져옵니다.
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            //가져온 값의 위치의 타일맵의 월드 좌표를 가져옵니다.
            
            Vector3Int tilePosition = tilemap.WorldToCell(clickPosition);
            //타일맵의 중앙부분의 좌표를 가져옵니다.
            targetPosition = tilemap.GetCellCenterWorld(tilePosition);
            
            //움직일수 있도록 합니다.
            isMoving = true;
        }
        if (isMoving)
        {
            // 이동 방향과 거리 계산합니다.
            Vector3 direction = targetPosition - player.position;
            float distance = moveSpeed * Time.deltaTime;
            Debug.Log(direction.normalized);

            // 플레이어캐릭터가 목표지점에 도착했는지 여부를 확인합니다.
            // 혹은 목표지점이 캐릭터가 현재 있는 좌표인지 확인합니다.
            if (direction.magnitude <= distance)
            {
                // 목표 위치에 도달한 경우 이동 중지
                player.position = targetPosition;
                isMoving = false;
                // 애니메이션과 관련된 bool값들을 초기화 시켜 Idle상태로 되돌립니다.
                ResetAnim();
            }
            
            //캐릭터의 움직임과 애니메이션을 통제합니다.
            else
            {
                //위쪽으로 이동시 Up 애니메이션의 재생합니다.
                if(direction.normalized.x <= 0.1 && direction.normalized.x >= -0.1f  && direction.normalized.y > 0){
                    
                    anim.SetBool("Up", true);
                }
                // 아래쪽으로 이동시 Down애니메이션을 재생합니다.
                else if(direction.normalized.x <= 0.1 && direction.normalized.x >= -0.1f && direction.normalized.y < 0){
                    
                    anim.SetBool("Down", true);
                }
                else{
                    //음수일 경우 X축으로 대칭해서 애니메이션을 재생합니다.
                    if(direction.normalized.x < 0){
                        playerCharactor.flipX = true;
                        anim.SetBool("Right", true);
                    }
                    //양수일 경우 기존 애니메이션을 그대로 재생합니다.
                    // 0이 없는 이유는 0의 경우 움직임이 없는 Idle이거나 Up,Down인 경우기에 포함하지 않습니다. 
                    else if(direction.normalized.x > 0){
                        playerCharactor.flipX = false;
                        anim.SetBool("Right", true);
                    }
                }
                // 이동 방향과 거리에 따라 유닛 이동합니다
                player.position += direction.normalized * distance;
            }
        }     
    }

    // 플레이어캐릭터가 충돌이 발생했을 때, 작동합니다.
    private void OnCollisionStay2D(Collision2D other) {
        // Map이라는 태그를가진 맵타일과 충돌시 플레이어가 그방향으로 오브젝트에 비비는것을 막습니다.
        if(other.transform.CompareTag("Map")){
            player.position = player.position;
            isMoving = false;   
        }
    }
    //모든 애니메이션을 초기화합니다.
    void ResetAnim(){
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Right", false);
    }
}
