using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;
using Common;

namespace Combat
{
    
    public class GridView : MonoBehaviour 
    {
        // all display Input 
        // all display outputs
        [Header("UI Elements")]
        [SerializeField] Tilemap _tileMap;

        [Header("SOs")]
        [SerializeField] GridModelSO _gridModelSO;          
        [SerializeField] GridPos2TilePosSO gridPos2TilePosSO;
       
                
        [SerializeField] DynamicPosData currentdyna = null;
        [SerializeField] DynamicPosData selectDyna = null;
        [SerializeField] DynamicPosData targetDyna = null;

        public Vector3Int currTilePos = Vector3Int.zero; 
        Vector3Int prevTilePos = Vector3Int.zero;
        [SerializeField] int currentpos = -1;       
        [SerializeField] TileState _tileState;
      
        public List<Vector3Int> currentSelectedTiles;

        GridMovement gridMovement;
        GridController gridController;

        GameObject FxCursor;
        List<GameObject> allFxRemote = new List<GameObject>();

        CharMode prevCharMode = CharMode.None;

        void Start()
        {
            _tileMap = GridService.Instance.gridLayout.GetComponentInChildren<Tilemap>();
            gridMovement = GetComponent<GridMovement>();
            gridController = GetComponent<GridController>(); 
            currentSelectedTiles = new List<Vector3Int>();
            GridService.Instance.currSelectionState = SelectionState.Hover;            
           
            
            CombatEventService.Instance.OnCombatLoot += OnCombatEndOrInLoot; 

        }

      

        private void Update()
        {

            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    HLAllTiles();
            //}
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("In fire 2"+ hit.transform.name);
                }
                else
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("in fire 1" + hit.transform.name);
                }
            }
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
            //RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (Input.GetMouseButtonDown(1))
                {                   
                    if (hit.transform.name == "TileBase")
                    {
                        currTilePos = _tileMap.WorldToCell(hit.point);
                        Debug.Log("CURR TILE POS" + hit.point + "COnverted" + _tileMap.CellToWorld(currTilePos));
                             
                        if (gridController.IsTileOnGrid(currTilePos))
                        {                          
                            GridService.Instance.On_CellPosRightClicked(GetPos4TilePos(currTilePos));   // right clicked on tile                          
                        }
                    }
             
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.name == "TileBase")
                    {
                        currentdyna = null; 
                        currTilePos = _tileMap.WorldToCell(hit.point);
                        if (gridController.IsTileOnGrid(currTilePos)) 
                        {
                            currentdyna = GetDynaFromTilePos(currTilePos);

                            if (GridService.Instance.IsTileHLOne(currTilePos))
                            {
                                if (CombatService.Instance.combatState == CombatState.INCombat_InSkillSelected
                                    || CombatService.Instance.combatState == CombatState.INTactics)   //SOmething IN HERE .. 
                                {
                                    //if(currentdyna.charGO != null || )
                                    //CellPosData cellPos = GridService.Instance.gr
                                    
                                    CellPosData cellPosData = GridService.Instance.gridView.GetPos4TilePos(currTilePos);
                                    GridService.Instance.On_CellPosLeftClicked(cellPosData); 
                                    CombatEventService.Instance.On_targetClicked(currentdyna, cellPosData);
                                }       
                            }
                            
                        }                      
                    }

                }
            }
        }

        void OnCombatEndOrInLoot(bool inLoot)
        {

            DestroyImmediate(FxCursor);

        }

        //void HLAllTiles()
        //{

        //    GameObject FxAlly = _gridModelSO.allyOnTurnHL;
        //    GameObject FxEnemy = _gridModelSO.enemyOnTurnHL;
        //    Quaternion quat = GridService.Instance.gridLayout.transform.rotation;

        //    for (int i = 1; i < 8; i++)
        //    {
        //        Vector3Int tilePos = gridMovement.GetTilePos4Pos(CharMode.Ally, i);
        //        Vector3 worldPos = gridMovement.GetWorldPosSingle(tilePos);

        //       Instantiate(FxAlly, worldPos, quat);

        //    }
        //    for (int i = 1; i < 8; i++)
        //    {
        //        Vector3Int tilePos = gridMovement.GetTilePos4Pos(CharMode.Enemy, i);
        //        Vector3 worldPos = gridMovement.GetWorldPosSingle(tilePos);

        //        Instantiate(FxEnemy, worldPos, quat);
        //    }
        //}

        

        public void CharOnTurnHL(DynamicPosData dyna)
        {
            if(dyna == null)
            {
                Debug.Log("DEAD OBJECT TURN");
                return; 
            }
            //if (CombatService.Instance.combatState != CombatState.INCombat_normal)
            //    return;
            Vector3 worldPos = GridService.Instance.GetDynaWorldPos(dyna); 
            GameObject FxAlly = _gridModelSO.allyOnTurnHL;
            GameObject FxEnemy = _gridModelSO.enemyOnTurnHL;
            Quaternion quat = GridService.Instance.gridLayout.transform.rotation; 

            if (dyna.charMode == prevCharMode)
            {
                FxCursor.transform.DOMove(worldPos, 0.0f);
            }
            else
            {
                if (dyna.charMode == CharMode.Ally)
                {
                    DestroyImmediate(FxCursor);
                    FxCursor = Instantiate(FxAlly, worldPos, quat);
                }
                if (dyna.charMode == CharMode.Enemy)
                {
                    DestroyImmediate(FxCursor);
                    FxCursor = Instantiate(FxEnemy, worldPos, quat);
                }
            }
            prevCharMode = dyna.charMode;
        }

        public void ChangeSelectionState(Vector3Int _tilePos, DynamicPosData _dynaPosData)
        {
            if (!gridController.IsTileOnGrid(_tilePos))
                return;
            // Selection State to  tile State conversion 
            TileState _tileState = (TileState)((int)GridService.Instance.currSelectionState);

            CharMode charMode = gridController.GetTileCharSide(_tilePos);
            int pos = GetPos4TilePos(_tilePos).pos; 
            if ( charMode == _dynaPosData.charMode)
            {              

                if (_dynaPosData.cellsOccupied.Any(c => c == pos))
                {
                    selectDyna = _dynaPosData;    // Dyna is known here 
                    foreach (var t in _dynaPosData.cellsOccupied)
                    {
                        ToggleTileState(t, _dynaPosData.charMode, _tileState);
                    }
                }
            }
           // targetDyna = null;  // why ?

        }

        public DynamicPosData GetDynaFromTilePos(Vector3Int _tilePos)
        {
            CellPosData cData = GetPos4TilePos(_tilePos);
            foreach (DynamicPosData dyna in GridService.Instance.allCurrPosOccupiedByDyna)
            {
                if (dyna != currentdyna)
                {
                    if (cData.charMode == dyna.charMode)
                    {
                        if (dyna.cellsOccupied.Any(c => c == cData.pos))
                            return dyna;
                    }
                }              
            }
            return currentdyna; 
        }

        public DynamicPosData GetDynaFromPos(int pos, CharMode charMode)
        {
            foreach (DynamicPosData dyna in GridService.Instance.allCurrPosOccupiedByDyna)
            {
               
                    if (charMode == dyna.charMode)
                    {
                        if (dyna.cellsOccupied.Any(c => c == pos))
                            return dyna;
                    }                
            }
            return null; 
        }


        void cleanSelectDyna()
        {
            foreach (int pos in selectDyna.cellsOccupied)
            {
                ToggleTileState(pos, selectDyna.charMode, TileState.Normal); 
            }
        }    

        public void ToggleTileState(int _pos, CharMode _charMode,  TileState _tileState)
        {
            Vector3Int _tilePos = gridMovement.GetTilePos4Pos(_charMode , _pos); 
            _tileMap.SetTile(_tilePos, _gridModelSO.GetTile(_tileState, _charMode));
            currentSelectedTiles.Add(_tilePos); 
        }

        public CellPosData GetPos4TilePos(Vector3Int _tilePos)
        {
       
            for (int i =0; i < gridPos2TilePosSO.gridAlly.Length; i++)
            {
                if (gridPos2TilePosSO.gridAlly[i].tilePos == _tilePos)
                {
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, ++i);
                    return cellPosData;  
                }
            }

            for (int i = 0; i < gridPos2TilePosSO.gridEnemy.Length; i++)
            {
                if (gridPos2TilePosSO.gridEnemy[i].tilePos == _tilePos)
                {

                    CellPosData cellPosData = new CellPosData(CharMode.Enemy, ++i);
                    return cellPosData;
                }
            }
            Debug.Log("no matching tile found ! "); 
            return null; 
        }

        public void ClearAllTileState()
        {
            // gridSO and using pos and clean all tilestate, to normal
            for(int i = 1; i <=7; i++)
            {
                ToggleTileState(i, CharMode.Ally, TileState.Normal);
                ToggleTileState(i, CharMode.Enemy, TileState.Normal);
            }
        }

        public void ToggleDynaState(DynamicPosData HLDyna, TileState _tileState)
        {            
            foreach(int pos in HLDyna.cellsOccupied)
            {
                ToggleTileState(pos, HLDyna.charMode, _tileState);
            }
        }

        




    }



}

//if (Physics.Raycast(ray, out hit, 1000f))
//{
//    if (hit.transform.name == "TileBase")
//    {
//        currTilePos = _tileMap.WorldToCell(hit.point);

//        if ((currTilePos != prevTilePos) && (gridController.IsTileOnGrid(currTilePos)))
//        {
//            currentdyna = GetDynaFromTilePos(currTilePos); int pos = GetPos4TilePos(currTilePos).pos;

//            if (currentdyna != selectDyna)
//            {
//                cleanSelectDyna();
//                foreach (DynamicPosData dynaPosData in GridService.Instance.allCurrPosOccupied)
//                {
//                   // Debug.Log("inside dyna loop " + dynaPosData.cellsOccupied[0]);
//                    ChangeSelectionState(currTilePos, dynaPosData);  // dyna later... 
//                }
//            }                      
//            currentdyna = selectDyna;
//            prevTilePos = currTilePos;                         
//        }
//    }        
//}








//if (FxCursor == null )
//{
//    if(CombatService.Instance.currCharOnTurn.charModel.charMode == CharMode.Ally)
//    {
//        FxCursor = Instantiate(FxAlly, worldPos, quat);
//    }
//    else
//    {
//        FxCursor = Instantiate(FxAlly, worldPos, quat);
//    }
//}                                    
//else
//{
