using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    public IPlayerState CurrentState{
        get; set;
    }

    readonly PlayerController playerController;
    public PlayerStateContext(PlayerController player){
        playerController = player;
    }

    public void StateTransition(){
        CurrentState.Handle(playerController);
    }
    public void StateTransition(IPlayerState state){
        CurrentState = state;
        CurrentState.Handle(playerController);
    }

}
