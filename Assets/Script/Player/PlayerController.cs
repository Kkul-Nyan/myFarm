using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    IPlayerState idleState, moveState, workState;
    PlayerStateContext playerStateContext;
    public WorkType currentWorkType{
        get; set;
    }

    private void Start() {
        playerStateContext = new PlayerStateContext(this);
        idleState = gameObject.AddComponent<PlayerStateIdle>();
        moveState = gameObject.AddComponent<PlayerStateMove>();
        workState = gameObject.AddComponent<PlayerStateWork>();

        playerStateContext.StateTransition(idleState);
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