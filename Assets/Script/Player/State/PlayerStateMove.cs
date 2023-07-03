using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfind;

public class PlayerStateMove : MonoBehaviour, IPlayerState
{
    private PlayerController controller;
    Vector2Int playerPos;
    Vector2Int targetPos;
    Vector3 clickPosition;

    bool up;
    bool down;
    bool left;
    bool right;

    //총 변수가 8개 나와야 한다.
    float playerMoveX;
    float playerMoveY;

    int count = 0;
    bool isMove = false;

    
    public void Handle(PlayerController playerController){
        if(!controller){
            controller = playerController;
        }
    }

    private void Start()
    {
        //controller.targetPosition을 시작때 초기화 시킵니다.
        controller.TargetPosition = controller.transform.position;
    }

    private void Update()
    {
        MouseClickTarget();
        Move();
    }

    /** 플레이어위치에서 목표지점까지 경로를 탐색합니다. 
    경로상 움직이던 중이라면, 플레이어가 움직이고있는 가장가까운 노드를 기준으로 새로운 경로를 구성합니다. */
    public void Check()
    {
        isMove = false;
        if(controller.FinalNode != null){
            playerPos = new Vector2Int(controller.FinalNode.x, controller.FinalNode.y);
        }
        else{
            playerPos = new Vector2Int(Mathf.RoundToInt(controller.transform.position.x), Mathf.RoundToInt(controller.transform.position.y));
        }
        
        Pathfinding.SearchPathfinding(playerPos, targetPos);
        controller.PathNode = Pathfinding.FinalNodeList;
        count = 0;
        isMove = true;
    }
    // 경로를 초기화합니다. controller.targetPosition 역시 초기화합니다.
    public void Reset()
    {
        
        controller.PathNode.Clear();
        controller.TargetPosition = controller.transform.position;
    }

    /**마우스 좌클릭이 입력되면  좌클릭이 입력된 위치로 이동합니다. 움직이는 도중에 다른좌표를 클릭한 경우 현재움직이고있는 한칸의 경로를 기억한뒤 
    경로를 초기화 합니다. 기억한 한칸의 경로를 새경로상 가장 앞에 추가해서 캐릭터가 마주 1칸을 움직이고 새경로로 움직이게 합니다.*/
    public void MouseClickTarget()
    {
        // 마우스 클릭 입력 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭한 위치의 타일맵 좌표 계산
            clickPosition = GetMouseTilePosition();

            // 이동 가능한 위치인지 확인
            if (!IsObstacle(clickPosition))
            {
                targetPos = new Vector2Int(Mathf.RoundToInt(clickPosition.x), Mathf.RoundToInt(clickPosition.y));
                if (isMove)
                {
                    // 도중에 다른 좌표를 클릭한 경우
                    controller.FinalNode = controller.PathNode[count];
                    Debug.Log("controller.FinalNode : " + controller.FinalNode.x +", "+ controller.FinalNode.y);
                    Reset();
                }
                Check();
                if(controller.FinalNode != null) controller.PathNode.Insert(0, controller.FinalNode);
            }
        }
    }

    //마우스 좌표를 스크린상 좌표로 변환합니다.
    Vector2 GetMouseTilePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }

    // 주어진 위치에 장애물이 있는지 확인합니다.
    bool IsObstacle(Vector2 position)
    {
        Collider2D hitCollider;
        for (int i = 0; i < Pathfinding.blockedLayerMasks.Count; i++)
        {
            hitCollider = Physics2D.OverlapCircle(position, 0.1f, Pathfinding.blockedLayerMasks[i]);
            if (hitCollider != null)
            {
                Debug.Log("HitLayer : " + hitCollider);
                return true;
            }
        }
        return false;
    }

    /**캐릭터를 지정된 좌표로 한칸 한칸 이동합니다. 이동이 완료되면, 새로운 한칸의 좌표를 받아 이동합니다. 마지막 좌표에 도착하면 작동을 멈춥니다*/
    public void Move()
    {
        if (isMove)
        {
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.TargetPosition, controller.PlayerMoveSpeed * Time.deltaTime);
            if (Vector3.Distance(controller.transform.position, controller.TargetPosition) < 0.05f)
            {
                if (count == controller.PathNode.Count - 1)
                {
                    ResetAnimation();
                    Reset();
                    Debug.Log("도착");
                    controller.FinalNode = null;
                    isMove = false;
                    return;
                }
                else if (count < controller.PathNode.Count)
                {
                    count++;
                    controller.TargetPosition = new Vector3(controller.PathNode[count].x, controller.PathNode[count].y);
                    Debug.Log("PathNode.xy : " + controller.PathNode[count].x + ", "+ controller.PathNode[count].y + "Player.xy : " + controller.transform.position.x + ", " + controller.transform.position.y);
                    MoveAnimation();
                }
            }
        }
    }

    void MoveAnimation(){
        ResetAnimation();
        //우상
        if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x > 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y > 0){
            controller.Anim.SetBool("Right", true);
            controller.PlayerCharactor.flipX = true;
        }
        //우하
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x > 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y < 0){
            controller.Anim.SetBool("Right", true);
            controller.PlayerCharactor.flipX = true;
        }
        //좌상
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x < 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y > 0){
            controller.Anim.SetBool("Right", true);
        }
        //좌하
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x < 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y < 0){
            controller.Anim.SetBool("Right", true);
        }
        //우
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x > 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y == 0){
            controller.Anim.SetBool("Right", true);  
            controller.PlayerCharactor.flipX = true;
        }
        //좌
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x < 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y == 0){
            controller.Anim.SetBool("Right", true);
        }
        //상
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x == 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y > 0){
            controller.Anim.SetBool("Up", true);
        }
        //하
        else if(Mathf.RoundToInt(controller.transform.position.x) - controller.PathNode[count].x == 0 && Mathf.RoundToInt(controller.transform.position.y) - controller.PathNode[count].y < 0){
            controller.Anim.SetBool("Down", true);
        }   
    }

    void ResetAnimation(){
        controller.Anim.SetBool("Right", false);
        controller.Anim.SetBool("Up", false);
        controller.Anim.SetBool("Down", false);
        controller.PlayerCharactor.flipX = false;
    }

    //기즈모를 통해 플레이어가 움직이는 경로를 확인합니다. 경로가 존재할때만 기즈모를 그립니다.    
    private void OnDrawGizmos()
    {
        if (controller.PathNode == null || controller.PathNode.Count == 0)
            return;

        for (int i = 0; i < controller.PathNode.Count - 1; i++)
        {
            Gizmos.DrawLine(new Vector2(controller.PathNode[i].x, controller.PathNode[i].y), new Vector2(controller.PathNode[i + 1].x, controller.PathNode[i + 1].y));
 
        }
    }
}
