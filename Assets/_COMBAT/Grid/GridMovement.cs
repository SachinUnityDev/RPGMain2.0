using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using Common;

namespace Combat
{
    public enum MoveDir 
    {
        None, 
        Forward, 
        Backward, 
    }

    public class GridMovement : MonoBehaviour
    {
        // all scripts movement related 
        [SerializeField] GridPos2TilePosSO _grid2cellPosSO; 
        Grid _gridLayout;
         
       
        [SerializeField] Vector3 movePos;
        [SerializeField] float moveSpeed;

        [SerializeField] AdjMatrixData adjMatrixData; 
        void OnEnable()
        {
            moveSpeed = 2.5f;
            adjMatrixData = new AdjMatrixData();
            _gridLayout = GridService.Instance.gridLayout;
           // _grid2cellPosSO = GridService.Instance.grid2CellPos; 
        }

        public List<int> GetUnOccupiedPosList(DynamicPosData dynamicPosData)
        {
            List<int> fullList = new List<int>() {1,2,3,4,5,6,7}; 
            if (dynamicPosData.charMode == CharMode.Ally)
            {
               return fullList.Except(GridService.Instance.allOccupiedPosAlly).ToList(); 

            }else if (dynamicPosData.charMode == CharMode.Enemy) {
                return fullList.Except(GridService.Instance.allOccupiedPosEnemy).ToList();
            }
            return null; 
        }
        public bool MovebyRow(DynamicPosData dynamicPos, MoveDir moveDir, int steps)
        {
           // Debug.Log("DYNA " + dynamicPos.currentPos + dynamicPos.charMode);

            int targetCell = 0; 
            if(dynamicPos.charMode == CharMode.Enemy)
            {
                switch (moveDir)
                {
                    case MoveDir.None:
                        break;
                    case MoveDir.Forward:
                        targetCell = dynamicPos.currentPos - steps * 3;
                        break;
                    case MoveDir.Backward:
                        targetCell = dynamicPos.currentPos + steps * 3;
                        break;
                    default:
                        break;
                }
            }
            else if(dynamicPos.charMode == CharMode.Ally)
            {
                switch (moveDir)
                {
                    case MoveDir.None:
                        break;
                    case MoveDir.Backward:
                        targetCell = dynamicPos.currentPos + steps * 3;
                        break;
                    case MoveDir.Forward:
                        targetCell = dynamicPos.currentPos - steps * 3;
                        break;
                    default:
                        break;
                }
            }
          
            if (targetCell.IsWithinRange())
            {
                if (GetUnOccupiedPosList(dynamicPos).Any(t => t == targetCell))
                {
                    Debug.Log("iniside push/pull/move" + targetCell); 
                    GridService.Instance.gridController.Move2Pos(dynamicPos, targetCell);
                }else
                {
                    return false; 
                }
            }
            return true;

        }

        //public bool CanTargetBePushed(DynamicPosData dynamicPos, MoveDir moveDir, int steps)
        //{
        //    int targetCell = 0;
        //    switch (moveDir)
        //    {
        //        case MoveDir.None:
        //            break;
        //        case MoveDir.Forward:
        //            targetCell = dynamicPos.currentPos - steps * 3;
        //            break;
        //        case MoveDir.Backward:
        //            targetCell = dynamicPos.currentPos + steps * 3;
        //            break;
        //        default:
        //            break;
        //    }
        //    if (targetCell.IsWithinRange())
        //    {
        //        if (GetUnOccupiedPosList(dynamicPos).Any(t => t == targetCell))
        //        {
        //            return true; 
                
        //        }
        //    }

        //            return false; 
        //}



        public List<int> GetAllSingleMovePos(CharOccupies _charOccupies, int cellNoFwd, int trisNo =-1)  // cell Nos Side only for triad
        {
            List<int> possibleSinglePos = new List<int>();
            List<int> possibleLanePos = new List<int>();
            List<int> possibleDiamondPos = new List<int>();
            possibleSinglePos = adjMatrixData.GetAdjacency(cellNoFwd); 
            switch (_charOccupies)
            {               
                case CharOccupies.Single:
                    return possibleSinglePos;               
                
                case CharOccupies.Lane:
                    foreach (int tFwd in possibleSinglePos)
                    {
                        int tRear = tFwd + 3;
                        if ( (tRear >= 1 && tRear <= 7))
                        {
                            possibleLanePos.Add(tFwd); 
                        }
                    }
                    return possibleLanePos; 
                    
                case CharOccupies.Tris:
                    if (trisNo == -1) break;
                    return null; 
                  //  return adjMatrixData.TrisAjacency(trisNo); 

                case CharOccupies.Diamond:                    
                    
                    if (cellNoFwd == 1)                    
                        possibleDiamondPos.Add(4); 
                    else
                        possibleDiamondPos.Add(1);

                    return possibleDiamondPos; 
                default:
                    break;
            }
            return null; 
        }

        #region Position Methods

        public Vector3Int GetGOCellValue(GameObject go)
        {
            Vector3Int _cellPosition = _gridLayout.WorldToCell(go.transform.position);
            return _cellPosition;
        }

        public Vector3 GetWorldPosSingle(Vector3Int cellPos)  // get world pos for single occupies
        {
            Vector3 worldPos = _gridLayout.CellToWorld(cellPos);
            return worldPos;
        }

        public Vector3 GetWorldPos4Long(CharMode charMode, int cellNosFwd, int cellNosRear)
        {
            Vector3Int cellPosFwd = GetTilePos4Pos(charMode, cellNosFwd);
            Vector3Int cellPosRear = GetTilePos4Pos(charMode, cellNosRear);
            Vector3 worldPosFwd = GetWorldPosSingle(cellPosFwd);
            Vector3 worldPosRear = GetWorldPosSingle(cellPosRear);

            return (worldPosFwd+worldPosRear)/2; 
        }

        public Vector3 GetWorldPos4Diamond(CharMode charMode, int cellNosFwd, int cellNosRear)
        {
            Vector3Int cellPosFwd = GetTilePos4Pos(charMode, cellNosFwd);
            Vector3Int cellPosRear = GetTilePos4Pos(charMode, cellNosRear);
            Vector3 worldPosFwd = GetWorldPosSingle(cellPosFwd);
            Vector3 worldPosRear = GetWorldPosSingle(cellPosRear);

            return (worldPosFwd + worldPosRear) / 2;
        }

        public Vector3 GetWorldPos4Triad(CharMode charMode, int cellNosFwd, int cellNosRear,int cellNosSide)                                                       
        {
            Vector3Int cellPosFwd = GetTilePos4Pos(charMode, cellNosFwd);
            Vector3Int cellPosRear = GetTilePos4Pos(charMode, cellNosRear);
            Vector3Int cellPosSide = GetTilePos4Pos(charMode, cellNosSide);

            float centerX = (cellPosFwd.x + cellPosRear.x + cellPosSide.x)/ 3;
            float centerY = (cellPosFwd.y + cellPosRear.y + cellPosSide.y) / 3;
            float centerZ = (cellPosFwd.z + cellPosRear.z + cellPosSide.z) / 3;

            Vector3 centeroid = new Vector3(centerX , centerY , centerZ); 
           return centeroid ;
        }

        public Vector3 GetWorldPos4Hex(CharMode charMode)
        {
            Vector3Int pos = GetTilePos4Pos(charMode, 4);
            return GetWorldPosSingle(pos); 
            
        }

        public Vector3Int GetTilePos4Pos(CharMode charMode, int cellNos)
        {
            
            switch (charMode)
            {
                case CharMode.None:
                    break;
                case CharMode.Ally:
                    var grid2PosA = _grid2cellPosSO.gridAlly[--cellNos].tilePos; 
                    return grid2PosA;
                    
                case CharMode.Enemy:
                    //Debug.Log("INDEX" + cellNos); 
                    var grid2PosB = _grid2cellPosSO.gridEnemy[--cellNos].tilePos;
                    return grid2PosB;
                
                default:
                    break;
            }
            return Vector3Int.zero;
        }

        #endregion





        //void Update()
        //{

        //    if (Input.GetKey(KeyCode.D))  //1A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 1);
        //        Debug.Log(cellPos + "Cell Pos");
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.E))  //2A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 2);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.X))  //3A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 3);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.S))  //4A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 4);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.W))  //5A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 5);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.Z))  //6A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 6);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.A))  //5A	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Ally, 7);
        //        movePos = GetWorldPos(cellPos);
        //    }

        //    if (Input.GetKey(KeyCode.H))  //1E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 1);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.U))  //2E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 2);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.N))  //3E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 3);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.J))  //4E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 4);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.I))  //5E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 5);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.M))  //6E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 6);
        //        movePos = GetWorldPos(cellPos);
        //    }
        //    if (Input.GetKey(KeyCode.K))  //7E	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 7);
        //        movePos = GetWorldPos(cellPos);
        //    }

        //    if (Input.GetKey(KeyCode.G))  //Diamond	
        //    {
        //        Vector3Int cellPos = GetTilePos4Pos(CharMode.Enemy, 1);
        //        Vector3Int cellPos2 = GetTilePos4Pos(CharMode.Enemy, 4);

         //       Vector3 pos = GetWorldPos(cellPos);
        //        Vector3 pos2 = GetWorldPos(cellPos2);
        //        movePos = new Vector3((pos.x + pos2.x) / 2, pos.y, pos.z);

        //    }


        // testPlayer.transform.position = Vector3.MoveTowards(testPlayer.transform.position, movePos, moveSpeed * Time.deltaTime);


        //}
    }




}





