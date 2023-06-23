using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWork : MonoBehaviour, IPlayerState
{
    public void Handle(PlayerController playerController){

    }
}

public enum WorkType{
    Farming,
    Camping
}
