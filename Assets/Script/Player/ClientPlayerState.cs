using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ClientPlayerState : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    int stateInt;
    public int StateInt{
        get{ return stateInt;}
        set{ stateInt = value;}
    }
    
    private void Start() {
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    void CheckState(){
        if(Mouse.current.leftButton.isPressed){
            switch (stateInt){
                case 0:
                    playerController.IdlePlayer();
                break;
                case 1:
                    playerController.MovePlayer();
                break;
                case 2:
                    playerController.WorkPlayer(WorkType.Farming);
                break;
                default :
                    playerController.IdlePlayer();
                break;
            }
        }
    }
}


