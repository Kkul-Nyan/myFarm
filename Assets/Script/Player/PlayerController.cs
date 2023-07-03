using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfind;

public class PlayerController : MonoBehaviour
{
    
    PlayerStateContext playerStateContext;
    IPlayerState idleState, moveState, workState;
    private Vector2 targetPosition;
    public Vector2 TargetPosition{
        get{return targetPosition;}
        set{targetPosition = value;}
    }

    public Tilemap tilemap{get; set;}
    private Node targetPos;
    public Node TargetPos{
        get{return targetPos;}
        set{targetPos = value;}
    }
    Node finalNode;
    public Node FinalNode{
        get{return finalNode;}
        set{finalNode = value;}
    }

    List<Node> pathNode;
    public List<Node> PathNode{
        get{return pathNode;}
        set{pathNode = value;}
    }

    public WorkType currentWorkType{
        get; set;
    }

    float playerMoveSpeed = 3f;
    public float PlayerMoveSpeed{
        get{return playerMoveSpeed;}
        set{playerMoveSpeed = value;}
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

 


    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Transform>();
        playerCharactor = GetComponentInChildren<SpriteRenderer>();
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