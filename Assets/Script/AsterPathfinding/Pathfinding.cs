using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pathfind{

    public class Node
    {
        public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

        public bool isWall;
        public Node ParentNode;

        // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
        public int x, y, G, H;
        public int F { get { return G + H; } }
    }
    

    public class Pathfinding : MonoBehaviour
    {
        public static Vector2Int bottomLeft, topRight; 
        private static Vector2Int startPos;
    
        private static Vector2Int targetPos;
       

        public static List<Node> FinalNodeList;
        public static bool allowDiagonal, dontCrossCorner;

        static int sizeX, sizeY;
        static Node[,] NodeArray;
        static Node StartNode, TargetNode, CurNode;
        static List<Node> OpenList, ClosedList;
        public static List<LayerMask> blockedLayerMasks = new List<LayerMask>();

        public static void SearchPathfinding(Vector2Int startXY, Vector2Int targetXY)
        {
            startPos = startXY;
            targetPos = targetXY;
            // NodeArray의 크기 정해주고, isWall, x, y 대입
            sizeX = topRight.x - bottomLeft.x + 1;
            sizeY = topRight.y - bottomLeft.y + 1;
            NodeArray = new Node[sizeX, sizeY];
           

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    bool isWall = false;
                    foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(bottomLeft.x + i ,bottomLeft.y + j), 0.4f))
                        for(int k = 0; k < blockedLayerMasks.Count; k++){
                            int colbitlayer = 1 << col.gameObject.layer;
                            if (colbitlayer == blockedLayerMasks[k].value){
                                isWall = true;
                            }
                        
                        }

                    NodeArray[i, j] = new Node(isWall,bottomLeft.x + i,bottomLeft.y + j);
                }
            }
            

            // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
            StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
            Debug.Log("StartNode : "+ StartNode.isWall + ", "+ StartNode.x +", "+ StartNode.y);
            TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

            OpenList = new List<Node>() { StartNode };
            ClosedList = new List<Node>();
            FinalNodeList = new List<Node>();

            
                while (OpenList.Count > 0)
                {
                // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
                CurNode = OpenList[0];
                for (int i = 1; i < OpenList.Count; i++)
                    if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

                OpenList.Remove(CurNode);
                ClosedList.Add(CurNode);


                // 마지막
                if (CurNode == TargetNode)
                {
                    Node TargetCurNode = TargetNode;
                    while (TargetCurNode != StartNode)
                    {
                        FinalNodeList.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.ParentNode;
                    }
                    FinalNodeList.Add(StartNode);
                    FinalNodeList.Reverse();

                    for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                    return;
                }


                // ↗↖↙↘
                if (allowDiagonal)
                {
                    OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                    OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                    OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                    OpenListAdd(CurNode.x + 1, CurNode.y - 1);
                }

                // ↑ → ↓ ←
                OpenListAdd(CurNode.x, CurNode.y + 1);
                OpenListAdd(CurNode.x + 1, CurNode.y);
                OpenListAdd(CurNode.x, CurNode.y - 1);
                OpenListAdd(CurNode.x - 1, CurNode.y);
            }
            
        }

        static void OpenListAdd(int checkX, int checkY)
        {
            // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
            if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
            {
                // 대각선 허용시, 벽 사이로 통과 안됨
                if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
                if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

                
                // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
                Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
                int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


                // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
                if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.G = MoveCost;
                    NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                    NeighborNode.ParentNode = CurNode;

                    OpenList.Add(NeighborNode);
                }
            }
        }

        
    }
}