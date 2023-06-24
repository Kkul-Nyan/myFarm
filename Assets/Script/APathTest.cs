using UnityEngine;
using Pathfinding;
using UnityEngine.InputSystem;

public class APathTest : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도

    private Vector2 targetPosition; // 클릭한 위치
    private Seeker seeker; // Seeker 컴포넌트 참조
    private Path path; // 계산된 경로 저장
    private int currentWaypoint = 0; // 현재 경유지 인덱스
    private bool isMoving = false; // 이동 중인지 여부

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveAlongPath();
        }
    }

    private void OnMouseDown()
    {
        if (!isMoving)
        {
            // 클릭한 위치로 이동 시작
            targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            StartMoving();
        }
    }

    private void StartMoving()
    {
        // 이동 시작 전에 이전 경로 초기화
        if (path != null)
        {
            path = null;
        }

        // 경로 계산
        seeker.StartPath(transform.position, targetPosition, OnPathCalculated);
    }

    private void OnPathCalculated(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            isMoving = true;
        }
    }

    private void MoveAlongPath()
    {
        if (path == null)
        {
            return;
        }

        // 다음 경유지 확인
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // 도착 지점에 도착한 경우
            isMoving = false;
            return;
        }

        // 다음 경유지로 이동
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        Vector2 velocity = direction * moveSpeed * Time.deltaTime;
        transform.Translate(velocity);

        // 다음 경유지에 도착한 경우
        if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.1f)
        {
            currentWaypoint++;
        }
    }
}