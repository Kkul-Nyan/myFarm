using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfind{
    public class PathCommon : MonoBehaviour
    {
        [Header("MapSize")]
        public Vector2Int bottomLeft;
        public Vector2Int topRight;
        public bool allowDiagonal;
        public bool dontCrossCorner;
        
    
        private void Awake() {
            Pathfinding.allowDiagonal = allowDiagonal;
            Pathfinding.dontCrossCorner = dontCrossCorner;
            Pathfinding.bottomLeft = bottomLeft;
            Pathfinding.topRight = topRight;
        }
    }
}
    
