using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    
    PlayerStateContext playerStateContext;
    IPlayerState idleState, moveState, workState;
    public WorkType currentWorkType{
        get; set;
    }

    float speed = 1f;
    public float Speed{
        get{return speed;}
        set{speed = value;}
    }

    Camera mainCamera;
    public Camera MainCamera{
        get{return mainCamera;}
        set{mainCamera = value;}
    }

    Animator anim; 
    public Animator Anim{
        get{return anim;}
        set{anim = value;}
    }

    Transform player;
    public Transform Player{
        get{return player;}
        set{player = value;}
    }

    SpriteRenderer playerCharactor;
    public SpriteRenderer PlayerCharactor{
        get{return playerCharactor;}
        set{playerCharactor = value;}
    }

    
    bool isMoving;
    public bool IsMoving{
        get{return isMoving;}
        set{isMoving = value;}
    }

    public LayerMask obstacleLayer{get; set;}// 이동 불가능한 타일맵의 레이어
    private Vector2 targetPosition; // 클릭한 위치
    public Tilemap tilemap{get; set;}

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        playerCharactor = GetComponentInChildren<SpriteRenderer>();
        obstacleLayer = LayerMask.GetMask("Obstacle"); 
        tilemap = FindFirstObjectByType<Tilemap>();
    }

    private void Start() {
        playerStateContext = new PlayerStateContext(this);
        idleState = gameObject.AddComponent<PlayerStateIdle>();
        moveState = gameObject.AddComponent<PlayerStateMove>();
        workState = gameObject.AddComponent<PlayerStateWork>();

        playerStateContext.StateTransition(moveState);
    }

    public void IdlePlayer(){
        playerStateContext.StateTransition(idleState);
    }
    public void MovePlayer(){
        playerStateContext.StateTransition(moveState);

    }

    public void WorkPlayer(WorkType workType){
        currentWorkType = workType;
        playerStateContext.StateTransition(workState);

    }

}