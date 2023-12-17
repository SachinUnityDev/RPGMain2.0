using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;
using Common;
using Spine.Unity;
using System;

namespace Combat
{
    
    public class GridController : MonoBehaviour 
    {

        // ALL MOVE RELATED POSITIONS 
        [Header("Data Classes")]
        public List<int> allTris = new List<int>() { 124, 134, 245, 346, 457, 467 };
        public List<int> allDiamonds = new List<int>() { 1234, 4567 }; 
        [SerializeField]GridPos2TilePosSO gridPos2TilePosSO;
        AdjMatrixData adjMatrixData;
       
        [SerializeField]GridMovement _gridMovement;
        [SerializeField] GridView _gridView; 
        float charSpeed = 0.5f;
        [SerializeField]List<DynamicPosData> allMovableDyna;
        [SerializeField] List<DynamicPosData> allAdjOccupied; 
        private void OnEnable()
        {
            allMovableDyna = new List<DynamicPosData>();
            _gridMovement = GetComponent<GridMovement>(); 
            adjMatrixData = new AdjMatrixData();
            _gridView = GetComponent<GridView>(); 
        }

     
        public void Move2Pos(DynamicPosData dyna, int targetCell)
        {
            // for single , if lane/tris/dia/hex 
            // use switch case
           
            // IF ROOTED CANNOT BE MOVED
            CharStateController charStateController = dyna.charGO.GetComponent<CharStateController>();
            if (charStateController.HasCharState(CharStateName.Rooted)) return; 

            if (targetCell < 1 || targetCell > 7) return;
            if (dyna == null) return;
           // Debug.Log("Target cell " + targetCell); 
            Vector3Int cellPos = _gridMovement.GetTilePos4Pos(dyna.charMode, targetCell);
            Vector3 targetPos = _gridMovement.GetWorldPosSingle(cellPos);
            dyna.charGO.transform.DOMove(targetPos, charSpeed, false);
            GridService.Instance.UpdateNewPosInDyna(dyna, targetCell);

            if(CombatService.Instance.currCharOnTurn.charModel.charID == 
                            dyna.charGO.GetComponent<CharController>().charModel.charID) 
                GridService.Instance.gridView.CharOnTurnHL(dyna);

            GridService.Instance.On_PosChg(dyna, targetCell);
            SetCharLayers(dyna);
        }

        void SetCharLayers(DynamicPosData dyna)
        {
            GameObject charGO = dyna.charGO;

            int sortLayer = dyna.GetLayerOrder(); 
       
           charGO.transform.GetChild(0).GetComponent<SkeletonAnimation>()
                                 .GetComponent<MeshRenderer>().sortingOrder = sortLayer;
           
            HPBarRenderSet(charGO, sortLayer); 
        }

        void HPBarRenderSet(GameObject charGO, int order)
        {
            SpriteRenderer[] allSpriteRenderer = charGO.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRen in allSpriteRenderer)
            {
                spriteRen.sortingOrder = order;
                if(spriteRen.gameObject.tag == "CharOrangeBar")
                {
                    spriteRen.sortingOrder = order + 1; 
                }
                if (spriteRen.gameObject.tag == "CharFillBar")
                {
                    spriteRen.sortingOrder = order + 2;
                }
            }
        }

        public void SwapPos (DynamicPosData targetDyna, DynamicPosData selectDyna)
        {
            int targetPos = targetDyna.currentPos;
            int selectPos = selectDyna.currentPos; 
            if (targetDyna.charMode != selectDyna.charMode) return;
           
            Move2Pos(selectDyna, targetPos);
            Move2Pos(targetDyna,selectPos );
        }

        public List<DynamicPosData> GetAvailableDynas(DynamicPosData selectDyna)
        {
            allMovableDyna.Clear(); allAdjOccupied.Clear(); 
            int count = selectDyna.cellsOccupied.Count; 
            List<int> adjPos = adjMatrixData.GetAdjacency(selectDyna.currentPos);
            
            switch (count)
            {
                
                case 1: 
                    if (selectDyna.charMode == CharMode.Ally)
                    {
                        foreach (int pos in adjPos)
                        {
                            if (!GridService.Instance.allOccupiedPosAlly.Any(t => t == pos))
                            {
                                DynamicPosData availableDyna = new DynamicPosData(CharMode.Ally, null, pos,
                                                                                    new List<int>() { pos });
                                allMovableDyna.Add(availableDyna);
                            }
                        
                        }
                        return allMovableDyna; 
                    }
                    break; 
                case 2:
                    if (selectDyna.charMode == CharMode.Ally)
                    {
                        foreach(int pos in adjPos)
                        {
                            if (!GridService.Instance.allOccupiedPosAlly.Any(t => t == pos)
                                &&!GridService.Instance.allOccupiedPosAlly.Any(t => t == pos+3))
                            {
                                DynamicPosData availableDyna = new DynamicPosData(CharMode.Ally, null, pos,
                                                                                   new List<int>() { pos, pos+3 });

                                allMovableDyna.Add(availableDyna);

                            }
                        }
                    }
                    if (selectDyna.charMode == CharMode.Enemy)
                    {
                        foreach (int pos in adjPos)
                        {
                            if (!GridService.Instance.allOccupiedPosEnemy.Any(t => t == pos)
                                && !GridService.Instance.allOccupiedPosEnemy.Any(t => t == pos + 3))
                            {
                                DynamicPosData availableDyna = new DynamicPosData(CharMode.Enemy, null, pos,
                                                                                   new List<int>() { pos, pos + 3 });

                                allMovableDyna.Add(availableDyna);

                            }
                        }
                    }
                    break; 
                default:
                    break;
            }

            return null; 
        }

        public List<DynamicPosData> GetAllAdjDynaOccupied(DynamicPosData selectDyna)
        {
            allAdjOccupied.Clear();
            int count = selectDyna.cellsOccupied.Count;
            List<int> adjPos = adjMatrixData.GetAdjacency(selectDyna.currentPos);

            foreach (DynamicPosData dyna in GridService.Instance.allCurrPosOccupiedByDyna)
            {
                if (dyna.charMode == selectDyna.charMode)
                {
                    if(adjPos.Any(t => t == dyna.currentPos))
                    {
                        allAdjOccupied.Add(dyna);
                    }
                }
            }
            return allAdjOccupied; 
        }
        public List<int> GetAllAdjCell(int cellNo)
        {
            return adjMatrixData.GetAdjacency(cellNo);
        }

        public bool IsValidSpace(CharModel _charModel, int _pos)
        {
            if (!IsCellInRange(_pos)) return false;  // Range chk 
          

            switch (_charModel._charOccupies)
            {
                case CharOccupies.Single:
                    if (GridService.Instance.IsPosVacant(new CellPosData(_charModel.charMode, _pos)))
                        return true; 
                    break;
                case CharOccupies.Lane:
                    if (GridService.Instance.IsPosVacant(new CellPosData(_charModel.charMode, _pos))
                        &&  GridService.Instance.IsPosVacant(new CellPosData(_charModel.charMode, _pos+3)))                            
                        return true;
                    break;
                case CharOccupies.Tris:
                    Debug.Log("TO BE Fixed after DEMO"); 
                        break;
                case CharOccupies.Diamond:
                    Debug.Log("TO BE Fixed after DEMO");
                    break;
                case CharOccupies.FullHex:
                    
                  //if (GridService.Instance.allCurrPosOccupiedByDyna.Any(t=>t.currentPos == ))

                        break;
                default:
                    break;
            }


            return false; 
        }

        public bool IsValidPos(CharMode _charMode, CharOccupies _charOccupies, int _cellPos)  // Cell no fwd will be tris value 
        {
            // check all the pos are within range 
            // check if none is occupied // charmode will define which list will rule.....       

            if (!IsCellInRange(_cellPos)) return false;  // Range chk 

            List<int> pos2beChecked = new List<int>(); 
            if (_charMode == CharMode.Ally)
            {
                pos2beChecked = GridService.Instance.allOccupiedPosAlly; 

            }else if (_charMode == CharMode.Enemy){
                pos2beChecked = GridService.Instance.allOccupiedPosEnemy;
            }

            switch (_charOccupies)
            {
                case CharOccupies.Single:                    
                        if (pos2beChecked.Any(p => p == _cellPos))
                            return false;
                        else return true;                                     
                    
                case CharOccupies.Lane:
                    if (!IsCellInRange(_cellPos + 3)) return false;
                   
                        if ((pos2beChecked.Any(p => p == _cellPos)) 
                            || (pos2beChecked.Any(p => p == _cellPos + 3)))
                            return false;
                        else return true;
                    
                case CharOccupies.Tris:                   
                        List<int> alltrisPos = _cellPos.SplitTris();
                        foreach (int pos in alltrisPos)
                        {
                            if (!IsCellInRange(pos)) return false;
                            if (pos2beChecked.Any(p => p == pos))
                                return false;
                        }
                        return true;                    
                   
                case CharOccupies.Diamond:
                    if (pos2beChecked.Any(p => p == _cellPos)
                            || pos2beChecked.Any(p => p == _cellPos + 1)
                            || pos2beChecked.Any(p => p == _cellPos + 2)
                            || pos2beChecked.Any(p => p == _cellPos + 3))
                            return false;
                        else return true;
                    
                case CharOccupies.FullHex:
                   
                        for (int i = 0; i < 8; i++)
                        {
                            if (pos2beChecked.Any(p => p == i))
                                return false;
                        }
                        return true;                   
                default:
                    Debug.Log("UnIntentional Default option");
                    break;
            }
            return false;
        }

        public List<int> GetSameLanePos(int pos)
        {

            List<int> sameLanePos = new List<int>();

            switch (pos)
            {
                case 1:
                    sameLanePos.AddRange(new List<int>() {1,4,7 });  
                    break;
                case 2:
                    sameLanePos.AddRange(new List<int>() { 2, 5 });
                    break;
                case 3:
                    sameLanePos.AddRange(new List<int>() { 3, 6 });
                    break;
                case 4:
                    sameLanePos.AddRange(new List<int>() { 1, 4,7 });
                    break;
                case 5:
                    sameLanePos.AddRange(new List<int>() { 2, 5 });
                    break;
                case 6:
                    sameLanePos.AddRange(new List<int>() { 3, 6 });
                    break;
                case 7:
                    sameLanePos.AddRange(new List<int>() { 1, 4,7 });
                    break;
                default:
                    break;

            }

            return sameLanePos; 
        }


        #  region Helper Methods

        public bool IsCellInRange(int cellNo)
        {
            if (cellNo >= 1 && cellNo <= 7)
                return true;
            else return false;
        }
        public bool IsTileOnGrid(Vector3Int _tilePos)
        {
            if (GetTileCharSide(_tilePos) == CharMode.Ally
                || GetTileCharSide(_tilePos) == CharMode.Enemy)
                return true;
            else
                return false;
        }
        public CharMode GetTileCharSide(Vector3Int _tilePos)
        {
            foreach (TileData pos in gridPos2TilePosSO.gridAlly)
            {
                if (_tilePos == pos.tilePos)
                    return CharMode.Ally;
            }
            foreach (TileData pos in gridPos2TilePosSO.gridEnemy)
            {
                if (_tilePos == pos.tilePos)
                    return CharMode.Enemy;
            }
            return CharMode.None;

        }

        #endregion

    }
}


































//if (selectDyna.charMode == CharMode.Ally)
//{
//    foreach (int pos in adjPos)
//    {
//        if (GridService.Instance.allOccupiedPosAlly.Any(t => t == pos))
//        {
//            DynamicPosData occupiedDyna = _gridView.GetDynaFromPos(pos, CharMode.Ally); 
//            allAdjOccupied.Add(occupiedDyna);
//        }
//    }
//    return allAdjOccupied;
//}
//if (selectDyna.charMode == CharMode.Enemy)
//{
//    foreach (int pos in adjPos)
//    {
//        if (GridService.Instance.allOccupiedPosEnemy.Any(t => t == pos))
//        {
//            DynamicPosData occupiedDyna = _gridView.GetDynaFromPos(pos, CharMode.Enemy);
//            allAdjOccupied.Add(occupiedDyna);
//        }
//    }
//    return allAdjOccupied;
//}















//public enum GridFXNames
//{
//    charPosMark,
//    charTargeted,
//    directionMark,
//}


//[SerializeField] Tilemap gridAllies;
//[SerializeField] Tilemap gridEnemies;

//[SerializeField] GameObject charPosMark;
//[SerializeField] GameObject charTargeted;
//[SerializeField] GameObject directionMark;
//public Dictionary<Vector3Int, GameObject> FXInPlay;

//// Start is called before the first frame update
//void Start()
//{
//    FXInPlay = new Dictionary<Vector3Int, GameObject>();
//}

//public void ShowGridFX(Tilemap _tilemap, GridFXNames _gridFXName, Vector3Int _tileMapPos)// takes in effect name and grid loc
//{
//    GameObject gridFXPrefab = GridEffectFactory(_gridFXName);
//    Vector3 worldPos = _tilemap.CellToWorld(_tileMapPos);

//    GameObject FXGO = Instantiate(gridFXPrefab, worldPos, Quaternion.identity);
//    FXInPlay.Add(_tileMapPos, FXGO);
//}

//GameObject GridEffectFactory(GridFXNames gridFXName)
//{
//    switch (gridFXName)
//    {
//        case GridFXNames.charPosMark: return charPosMark;
//        case GridFXNames.charTargeted: return charTargeted;
//        case GridFXNames.directionMark: return directionMark;
//        default: return null;

//    }
//}

