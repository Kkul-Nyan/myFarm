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
        targetPosition = transform.position;
    }

    private void Update()
    {
        CheckInput();
        Move();
    }

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

    public void Reset()
    {
        PathNode.Clear();
        targetPosition = transform.position;
    }

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
                    Reset();
                }
                Check();
                if(finalNode != null) PathNode.Insert(0, finalNode);
            }
        }
    }

    Vector2 GetMouseTilePosition()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }

    bool IsObstacle(Vector2 position)
    {
        Collider2D hitCollider;
        // 주어진 위치에 장애물이 있는지 확인
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

    public void Move()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                if (count == PathNode.Count - 1)
                {
                    Reset();
                    Debug.Log("도착");
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