using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfind;


public class PlayerPathfinding : MonoBehaviour
{
    Vector2Int playerPos;
    Vector2Int targetPos;

    public void Check() {
        playerPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        targetPos = new Vector2Int(3,3);
        Pathfinding.SearchPathfinding(playerPos, targetPos);
    }
    


    private void OnDrawGizmos(){
        if(Pathfinding.FinalNodeList == null){
            return;
        }
        if(Pathfinding.FinalNodeList.Count != 0) for (int i = 0; i < Pathfinding.FinalNodeList.Count - 1; i++)
        Gizmos.DrawLine(new Vector2(Pathfinding.FinalNodeList[i].x, Pathfinding.FinalNodeList[i].y), new Vector2(Pathfinding.FinalNodeList[i + 1].x, Pathfinding.FinalNodeList[i + 1].y));
    }
}
