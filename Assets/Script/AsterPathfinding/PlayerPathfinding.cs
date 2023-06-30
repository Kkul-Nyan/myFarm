using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfind;

public enum InputType
{
    Mouse,
    Touch
}

public class PlayerPathfinding : MonoBehaviour
{
    public InputType input;
    Vector2Int playerPos;
    Vector2Int targetPos;
    List<Node> PathNode;
    Node finalNode;
    public Vector3 targetPosition;
    public Vector3 clickPosition;
    int count = 0;
    public bool isMove = false;
    public float speed;

    private void Start()
    {
        //targetPosition을 시작때 초기화 시킵니다.
        targetPosition = transform.position;
    }

    private void Update()
    {
        CheckInput();
        Move();
    }

    //현재 들어온 입력값을 확인합니다.
    public void CheckInput()
    {
        switch (input)
        {
            case InputType.Mouse:
                MouseClickTarget();
                break;

            case InputType.Touch:
                break;

            default:
                break;
        }
    }

    /** 플레이어위치에서 목표지점까지 경로를 탐색합니다. 
    경로상 움직이던 중이라면, 플레이어가 움직이고있는 가장가까운 노드를 기준으로 새로운 경로를 구성합니다. */
    public void Check()
    {
        isMove = false;
        if(finalNode != null){
            playerPos = new Vector2Int(finalNode.x, finalNode.y);
        }
        else{
            playerPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
        
        Pathfinding.SearchPathfinding(playerPos, targetPos);
        PathNode = Pathfinding.FinalNodeList;
        count = 0;
        isMove = true;
    }
    // 경로를 초기화합니다. targetPosition 역시 초기화합니다.
    public void Reset()
    {
        
        PathNode.Clear();
        targetPosition = transform.position;
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
                    finalNode = PathNode[count];
                    Debug.Log("finalNode : " + finalNode.x +", "+ finalNode.y);
                    Reset();
                }
                Check();
                if(finalNode != null) PathNode.Insert(0, finalNode);
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                if (count == PathNode.Count - 1)
                {
                    Reset();
                    Debug.Log("도착");
                    finalNode = null;
                    isMove = false;
                    return;
                }
                else if (count < PathNode.Count)
                {
                    count++;
                    targetPosition = new Vector3(PathNode[count].x, PathNode[count].y);
                }
            }
        }
    }

    //기즈모를 통해 플레이어가 움직이는 경로를 확인합니다. 경로가 존재할때만 기즈모를 그립니다.    
    private void OnDrawGizmos()
    {
        if (PathNode == null || PathNode.Count == 0)
            return;

        for (int i = 0; i < PathNode.Count - 1; i++)
        {
            Gizmos.DrawLine(new Vector2(PathNode[i].x, PathNode[i].y), new Vector2(PathNode[i + 1].x, PathNode[i + 1].y));
        }
    }
}