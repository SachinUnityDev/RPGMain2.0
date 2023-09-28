using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 
using System.Linq;
using Common;
using System;
using System.IO;


namespace Combat
{
    [System.Serializable]
    public class CellPosData
    {
        public int pos;
        public CharMode charMode;
       
        public CellPosData(CharMode _charMode, int _pos)
        {
            pos = _pos;
            charMode = _charMode;
        }    
    }

    public class GridService : MonoSingletonGeneric<GridService>, ISaveableService
    {
        #region Declarations
        public Grid gridLayout;
        public GridPos2TilePosSO grid2CellPos;
        public GridModelSO gridModelSO;

        [HideInInspector] public GridController gridController;
        [HideInInspector] public GridView gridView;
        [HideInInspector] public GridMovement gridMovement;

        [Header("Position DYNA List")]
        public List<DynamicPosData> allCurrPosOccupiedByDyna = new List<DynamicPosData>();

        [Header("Position value list")]
        public List<int> allOccupiedPosAlly;
        public List<int> allOccupiedPosEnemy;
        public List<int> allUnOccupiedPosAlly;
        public List<int> allUnOccupiedPosEnemy;

        public List<int> occupiedByChar;   // irrelvant for NOW !
        //  always update occupiedbychar 
        public List<DynamicPosData> allTargetedDyna;                                                                              // this all currPosOccupied
        public DynamicPosData targetSelected = null; 
        
        public SelectionState currSelectionState;
        public List<int> unOccupiedTargetPos = new List<int>();

        // Action Events
        public event Action<CellPosData> OnCellPosClicked;

        [Header("Save and Load")]
        public List<String> allDynaStr = new List<string>();

        #endregion
        void Start()
        {
            gridController = GetComponent<GridController>();
            gridView = GetComponent<GridView>();
            gridMovement = GetComponent<GridMovement>();
            CombatEventService.Instance.OnCombatInit += GridInit;
           // CombatEventService.Instance.OnTargetClicked += GridUpdateOnTargetSelected;
           // CombatEventService.Instance.OnCharDeath += UpdateGridOnCharDeath;
            CombatEventService.Instance.OnEOT +=  ClearOldTargets;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnCombatInit -= GridInit;
            CombatEventService.Instance.OnEOT -= ClearOldTargets;
        }
        public void GridPosInit()
        {
            UpdateCellsOccupiedList();  
        }

        public void UpdateCellsOccupiedList()
        {
            allOccupiedPosAlly.Clear();
            allOccupiedPosEnemy.Clear();
            foreach (DynamicPosData Dyna in allCurrPosOccupiedByDyna)
            {
                if(Dyna.charMode == CharMode.Ally)
                {
                    Dyna.cellsOccupied.ForEach(t => allOccupiedPosAlly.Add(t));                    

                }else if (Dyna.charMode == CharMode.Enemy)
                {
                    Dyna.cellsOccupied.ForEach(t => allOccupiedPosEnemy.Add(t));
                }
                ReviseUnOccupied(); 
            }
        }

#region DYNA BASED CONTROL allcurrPos 

        public void PlaceCharOnGrid(DynamicPosData dyna)
        {
            CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos); 
            Vector3Int _tilePos = gridMovement.GetTilePos4Pos(dyna.charMode, dyna.currentPos);
            
            CharController charController = dyna.charGO.GetComponent<CharController>();
            if (IsPosVacant(cell))
            {
                switch (charController.charModel._charOccupies)
                {
                    case CharOccupies.Single:
                        dyna.charGO.transform.position = gridMovement.GetWorldPosSingle(_tilePos);
                        dyna.cellsOccupied.Add(dyna.currentPos);
                        break;
                    case CharOccupies.Lane:

                        dyna.charGO.transform.position = gridMovement
                            .GetWorldPos4Long(dyna.charMode, dyna.currentPos, dyna.currentPos + 3);
                        dyna.cellsOccupied.Add(dyna.currentPos);
                        dyna.cellsOccupied.Add(dyna.currentPos + 3);
                        break;
                    default:
                        Debug.Log("Unintentional default action triggered");
                        break;
                }
            }
            // check if pos is empty.. using 
           
        }

        public bool IsPosVacant(CellPosData cellPos)
        {
            foreach (DynamicPosData dyna in allCurrPosOccupiedByDyna)
            {
                if(dyna.charMode == cellPos.charMode)
                {
                    if (dyna.cellsOccupied.Any(t=>t == cellPos.pos))
                    {
                        return false; 
                    }
                }
            }
            return true; 
        }


        public void UpdateGridOnCharDeath(CharController _charController)
        {
            GameObject charGO = _charController.gameObject;
            DynamicPosData dyna = GetDyna4GO(charGO);
            allCurrPosOccupiedByDyna.Remove(dyna);
        }

        public void UpdateNewPosInDyna(DynamicPosData dynamicPosData, int newPos)
        {
            int index = allCurrPosOccupiedByDyna.FindIndex(t => t == dynamicPosData);
            int prevPos = dynamicPosData.currentPos;
            dynamicPosData.cellsOccupied.Remove(prevPos);
            dynamicPosData.cellsOccupied.Add(newPos);
            dynamicPosData.currentPos = newPos;
            dynamicPosData.FwdtilePos = gridMovement.GetTilePos4Pos(dynamicPosData.charMode, newPos);
            // update in Dyna List

            allCurrPosOccupiedByDyna[index] = dynamicPosData;
            UpdateCellsOccupiedList();
        }


        public void PlaceCharAsPerSaveGrid()    // only used to Init from saved file
        {
            foreach (DynamicPosData dyna in allCurrPosOccupiedByDyna)
            {
                Debug.Log("Dyna " + dyna.charGO.name);
                CharController charController = dyna.charGO.GetComponent<CharController>();
                if (dyna.charMode == CharMode.Ally)
                {
                    PlaceCharOnGrid(dyna);
                    // PlaceAllyOnSpot(charController, dyna.currentPos);                   
                }
               
                allOccupiedPosAlly.Distinct().ToList();
                allOccupiedPosEnemy.Distinct().ToList();
                ReviseUnOccupied();

            }
        }
 #endregion


        void Add2CharDynaList(DynamicPosData dyna)
        {
            allCurrPosOccupiedByDyna.Add(dyna);
            allCurrPosOccupiedByDyna.Distinct().ToList();


        }
     
      

        void ReviseUnOccupied()
        {
            allUnOccupiedPosEnemy.Clear();
            allUnOccupiedPosAlly.Clear(); 
            for (int i = 1; i < 8; i++)
            {
                allUnOccupiedPosEnemy.Add(i);
                allUnOccupiedPosAlly.Add(i); 
            }          

            foreach (int k in allOccupiedPosEnemy)
            {
                allUnOccupiedPosEnemy.Remove(k);
               
            }
            foreach (int m in allOccupiedPosAlly)
            {
                allUnOccupiedPosAlly.Remove(m);
            }
        }
      

        #region Actions
        public void GridInit()
        {
            allOccupiedPosAlly = new List<int>();
            allOccupiedPosEnemy = new List<int>();
            allUnOccupiedPosAlly = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            allUnOccupiedPosEnemy = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

            occupiedByChar = new List<int>();
            allTargetedDyna = new List<DynamicPosData>();
        }

        public void ShuffleCharSet(List<DynamicPosData> shuffleChar)
        {
            CharMode charMode = shuffleChar[0].charMode;

            if (charMode == CharMode.Ally)
            {
                allOccupiedPosAlly.ForEach(t => allUnOccupiedPosAlly.Remove(t));

                foreach (var charDyna in shuffleChar)
                {
                    int ran = UnityEngine.Random.Range(0, allUnOccupiedPosAlly.Count);
                    gridController.Move2Pos(charDyna, allUnOccupiedPosAlly[ran]);

                }
            }
            else if (charMode == CharMode.Enemy)
            {
                allOccupiedPosEnemy.ForEach(t => allUnOccupiedPosEnemy.Remove(t));

                foreach (var charDyna in shuffleChar)
                {
                    int ran = UnityEngine.Random.Range(0, allUnOccupiedPosEnemy.Count);
                    gridController.Move2Pos(charDyna, allUnOccupiedPosEnemy[ran]);
                }
            }

        }

        public void ShuffleParty(CharMode charMode)
        {
            if (charMode == CharMode.Ally)
            {               
                allOccupiedPosAlly.ForEach(t => allUnOccupiedPosAlly.Remove(t));

                foreach (var charDyna in GetAllOccupiedbyCharMode(charMode))
                {
                    int ran = UnityEngine.Random.Range(0, allUnOccupiedPosAlly.Count);
                    gridController.Move2Pos(charDyna, allUnOccupiedPosAlly[ran]);                  
                }
            }
            else if (charMode == CharMode.Enemy)
            {
                allOccupiedPosEnemy.ForEach(t => allUnOccupiedPosEnemy.Remove(t));

                foreach (var charDyna in GetAllOccupiedbyCharMode(charMode))
                {
                    int ran = UnityEngine.Random.Range(0, allUnOccupiedPosEnemy.Count);
                    gridController.Move2Pos(charDyna, allUnOccupiedPosEnemy[ran]);
                }
            }            
        }

        public void RestoreState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
              + "/Grid/DynaModels.txt";

            if (File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("File found!");
                string str = File.ReadAllText(Application.dataPath + mydataPath);

                allDynaStr = str.Split('|').ToList();
                allCurrPosOccupiedByDyna.Clear();
                foreach (string modelStr in allDynaStr)
                {
                    Debug.Log($"Grid: {modelStr}");
                    if (String.IsNullOrEmpty(modelStr)) continue; // eliminate blank string
                    DynaModel dynaModel = JsonUtility.FromJson<DynaModel>(modelStr);
                    DynamicPosData dyna = new DynamicPosData(dynaModel);
                    allCurrPosOccupiedByDyna.Add(dyna);
                    Debug.Log(dynaModel);
                }
                PlaceCharAsPerSaveGrid();
            }
            else
            {
                Debug.Log("File Does not Exist");
            }

        }

        public void SaveState()
        {
            if (allCurrPosOccupiedByDyna.Count <= 0)
            {
                Debug.Log("no chars in play"); return;
            }
            ClearState();
            foreach (DynamicPosData dyna in allCurrPosOccupiedByDyna)
            {
                dyna.SaveModel();
            }
        }
        public void ClearState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
             + "/Grid/DynaModels.txt";
            File.WriteAllText(Application.dataPath + mydataPath, "");

        }

        public void SetCharOnPosAfterAmbushed()
        {
            List<CharController> charCtrlInCombat = CharService.Instance.charsInPlayControllers;
            foreach (CharController charCtrl in charCtrlInCombat)
            {
                int pos = -1;
                do
                {
                    pos = UnityEngine.Random.Range(1, 8);

                } while (allOccupiedPosAlly.Any(t => t == pos));
                allOccupiedPosAlly.Add(pos);
                CharModel charModel = charCtrl.charModel;
                DynamicPosData dyna = new DynamicPosData(new DynaModel(charModel.charMode, charModel.charID
                                                                        , charModel.charName, pos));

                Add2CharDynaList(dyna);
                // PlaceAllyOnSpot(charCtrl,pos);
                PlaceCharOnGrid(dyna);
            }
        }

        public void SetEnemy(EnemyPacksSO enemySO)
        {

            foreach (EnemyDataInPack eData in enemySO.allEnemyDataInPack)
            {
                // CharController charCtrl = CharService.Instance.SpawnCompanions(eData.enemyName);

                CharController charCtrl = BestiaryService.Instance.SpawnBestiary(eData.enemyName); 
                    //,eData.charID);
                CharOccupies charOccupies = charCtrl.charModel._charOccupies;

                foreach (int pos in eData.preferredPos)
                {
                    CharModel charModel = charCtrl.charModel;

                    if (gridController.IsValidSpace(charModel, pos))// chks if cell is empty
                    {
                        DynamicPosData dyna = new DynamicPosData(new DynaModel(charModel.charMode, charModel.charID
                                                                                , charModel.charName, pos));

                        Add2CharDynaList(dyna);
                        PlaceCharOnGrid(dyna);
                        // PlaceEnemyOnSpot(charCtrl,pos);
                        break;
                    }
                    else
                    {
                          Debug.LogError("Cell no is filled" + pos);
                        // place on an empty spot

                    }
                }
            }

        }


        public void PlaceAllyOnSpot(CharController charCtrl, int pos)
        {
            GameObject charGO = charCtrl.gameObject;
            CharOccupies charOccupies = charCtrl.charModel._charOccupies;
            CharMode charMode = charCtrl.charModel.charMode;
            Vector3Int _tilePos =  gridMovement.GetTilePos4Pos(charMode, pos);
            List<int> occupiedPosAlly = new List<int>();

             occupiedByChar = new List<int>();
            switch (charOccupies)
            {
                case CharOccupies.Single:
                    occupiedPosAlly.Add(pos); occupiedByChar.Add(pos);                  
                    charGO.transform.position = gridMovement.GetWorldPosSingle(_tilePos);
                    break;
                case CharOccupies.Lane:
                    occupiedPosAlly.Add(pos);     occupiedByChar.Add(pos);
                    occupiedPosAlly.Add(pos + 3); occupiedByChar.Add(pos + 3);

                    charGO.transform.position = gridMovement.GetWorldPos4Long(CharMode.Ally, pos, pos + 3);
                    break;
                default:
                    Debug.Log("Unintentional default action triggered"); 
                    break;
            }           
            allOccupiedPosAlly.AddRange(occupiedPosAlly);
            occupiedPosAlly.ForEach(t => allUnOccupiedPosAlly.Remove(t));
           // DynamicPosData dyna = new DynamicPosData(charMode, charGO, pos, occupiedPosAlly);
            //if (!allCurrPosOccupiedByDyna.Any(t=> t == dyna))
            //    allCurrPosOccupiedByDyna.Add(dyna);          
        }
        
        public void PlaceEnemyOnSpot(CharController charCtrl,  int cellNo)
        {
            GameObject charGO = charCtrl.gameObject;
            List<int> occupiedPosEnemy = new List<int>();
            CharOccupies charOccupies = charCtrl.charModel._charOccupies;
             Vector3Int _cellPos = gridMovement.GetTilePos4Pos(CharMode.Enemy, cellNo);
            switch (charOccupies)
            {
                case CharOccupies.Single:
                    occupiedPosEnemy.Add(cellNo); occupiedByChar.Add(cellNo);
                    charGO.transform.position = gridMovement.GetWorldPosSingle(_cellPos);
                    break;
                case CharOccupies.Lane:
                    occupiedPosEnemy.Add(cellNo);       occupiedByChar.Add(cellNo);
                    occupiedPosEnemy.Add(cellNo + 3);   occupiedByChar.Add(cellNo+3);
                    charGO.transform.position = gridMovement.GetWorldPos4Long(CharMode.Enemy, cellNo, cellNo + 3);
                    break;
                case CharOccupies.Tris:   // split tris ........place object on tris position
                    List<int> trisSplit = cellNo.SplitTris();
                    foreach (int i in trisSplit)
                    {
                      occupiedPosEnemy.Add(i); occupiedByChar.Add(i);
                    }
                    charGO.transform.position = gridMovement.GetWorldPos4Triad(CharMode.Enemy, 
                                                trisSplit[0], trisSplit[1], trisSplit[3]);  
                    break;
                case CharOccupies.Diamond:
                    occupiedPosEnemy.Add(cellNo);    occupiedByChar.Add(cellNo);
                    occupiedPosEnemy.Add(cellNo+1);  occupiedByChar.Add(cellNo+1);
                    occupiedPosEnemy.Add(cellNo+2);  occupiedByChar.Add(cellNo+2);
                    occupiedPosEnemy.Add(cellNo+3);  occupiedByChar.Add(cellNo+3);
                    charGO.transform.position = gridMovement.GetWorldPos4Diamond(CharMode.Enemy, cellNo, cellNo + 3); 
                    break;
                case CharOccupies.FullHex:
                    for (int i = 0; i < 8; i++)
                    {
                        occupiedPosEnemy.Add(i);
                        occupiedByChar.Add(i);
                    }
                    charGO.transform.position = gridMovement.GetWorldPos4Hex(CharMode.Enemy); 
                    break;
                default:
                    break;
            }
            allOccupiedPosEnemy.AddRange(occupiedPosEnemy);
            occupiedPosEnemy.ForEach(t => allUnOccupiedPosEnemy.Remove(t));
            //DynamicPosData dyna = new DynamicPosData(CharMode.Enemy, charGO, cellNo, occupiedPosEnemy);
            //if (!allCurrPosOccupiedByDyna.Any(t => t == dyna))
            //    allCurrPosOccupiedByDyna.Add(dyna);
          
        }

        public void ClearOldTargets()
        {
            allTargetedDyna.Clear();
            gridView.ClearAllTileState();
        }
        public bool IsTileHLOne(Vector3Int _tilePos)
        {

            bool isHL =  gridView.currentSelectedTiles.Any(t => t == _tilePos);
            return isHL;  
        }
        public void HLTargetTiles(List<CellPosData> allPos)  // cellData mode
        {
            ClearOldTargets(); 

            foreach (CellPosData cellPosData in allPos)
            {
                DynamicPosData dyna = gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                   allTargetedDyna.Add(dyna); 
                else
                {
                    unOccupiedTargetPos.Add(cellPosData.pos);
                    gridView.ToggleTileState(cellPosData.pos, cellPosData.charMode, TileState.TargetMarked);
                }         
                               
            }
            allTargetedDyna.Distinct().ToList();

            foreach (var dyna in allTargetedDyna)
            {
                gridView.ToggleDynaState(dyna,  TileState.TargetMarked); 
            }      
        }

        public void On_CellPosRightClicked(CellPosData currCellPosClicked)
        {
            OnCellPosClicked?.Invoke(currCellPosClicked); 
        }

        #endregion

        #region Getters

        public Vector3 GetCenterPos4CurrHLTargets()
        {
            Vector3 centerPos  = new Vector3(0, 0, 0);  

            foreach (DynamicPosData dyna in allTargetedDyna)
            {
                centerPos += dyna.FwdtilePos; 
            }
            return centerPos/allTargetedDyna.Count; 
        }
        
        public int GetDynaRow(DynamicPosData dyna)
        {
            if (dyna.FwdtilePos.y == -1) return -1;
            if (dyna.FwdtilePos.y == 0) return 0;
            if (dyna.FwdtilePos.y == 1) return 1;
            return 0; 
        } 
        public List<DynamicPosData> GetAllTargets()
        {
            return allTargetedDyna; 
        }

    public void GridUpdateOnTargetSelected(DynamicPosData _targetDyna)
        {
            Debug.Log("EVENT AT GRID " + _targetDyna);
            currSelectionState = SelectionState.Target;
            gridView.ChangeSelectionState(_targetDyna.FwdtilePos, _targetDyna);  // tile selection state to be updated
            targetSelected = _targetDyna;
        }
        
        public List<DynamicPosData> GetAllByCharMode(CharMode charMode)
        {
            return allCurrPosOccupiedByDyna.FindAll(t => t.charMode == charMode).ToList(); 
        }


        public void UpdateDynaListOnDeath(DynamicPosData dyna)
        {
            allCurrPosOccupiedByDyna.Remove(dyna);
        }
        public DynamicPosData GetDyna4GO(GameObject _charGO)
        {
            return allCurrPosOccupiedByDyna.Find(d => d.charGO == _charGO); 
        }

        public DynamicPosData GetDynaAtCellPos(CharMode charMode, int pos)
        {

            return allCurrPosOccupiedByDyna.Find(t => t.currentPos == pos && t.charMode == charMode);
        }

        public Vector3 GetDynaWorldPos(DynamicPosData dyna)
        {
           
                return GridService.Instance.gridMovement.GetWorldPosSingle(dyna.FwdtilePos);            
        }
        public void SetAllyPreTactics()
        {
            // select one player at a time 
            // get pos priority for char Model 
            // if unoccupied give it him else go to next 
            List<CharController> charCtrlInPlay = CharService.Instance.charsInPlayControllers;
            foreach (CharController charCtrl in charCtrlInPlay)
            {
                CharOccupies charOccupies = charCtrl.charModel._charOccupies;
                bool charfoundPlace = false; 
                foreach (int pos in charCtrl.charModel._posPriority)
                {
                    CharModel charModel = charCtrl.charModel;
                    //if (gridController.IsValidPos(CharMode.Ally, charOccupies, pos))// chekc whether its empty or not
                    if (gridController.IsValidSpace(charModel, pos))
                    {
                       
                        DynamicPosData dyna = new DynamicPosData(new DynaModel(charModel.charMode, charModel.charID
                                                                                , charModel.charName, pos));
                        Add2CharDynaList(dyna);
                        //PlaceAllyOnSpot(charCtrl, pos);
                        PlaceCharOnGrid(dyna);
                        charfoundPlace = true; 
                        break;
                    }
                }
                if (!charfoundPlace)
                {
                    Debug.Log(" get all unoccupies place and find a place"); 
                }
            }
        }
        public List<int> GetAllUnOccupied(CharMode charMode)
        {
            //List<int> allTiles = new List<int>() { 1, 2, 3, 4, 5, 6, 7 }; 
            //if (charMode == CharMode.Ally)
            //{
            //    List<int> UnOccupiedAlly = allTiles.Where(t => !allOccupiedPosAlly.Any(x => x == t)).ToList();
            //    UnOccupiedAlly.
            //}
            //if(charMode == CharMode.Enemy)
            //{


            //}


            return null; 
        }
        public List<DynamicPosData> GetCharINDiamond(CharMode charMode, bool isFront)
        {
            List<DynamicPosData> InDiamond = new List<DynamicPosData>();
            foreach (DynamicPosData dyna in allCurrPosOccupiedByDyna)
            {
                if (dyna.charMode == charMode)
                {
                    if (isFront)
                    {
                        if (dyna.currentPos == 1 || dyna.currentPos == 2 || dyna.currentPos == 3 || dyna.currentPos == 4)
                        {
                            InDiamond.Add(dyna);
                        }
                    }
                    else
                    {
                        if (dyna.currentPos == 4 || dyna.currentPos == 5 || dyna.currentPos == 6 || dyna.currentPos == 7)
                        {
                            InDiamond.Add(dyna);
                        }
                    }
                }          
            }           

            return InDiamond;
        }

        public List<DynamicPosData> GetFirstDiamond(CharMode charMode)
        {

            if(allCurrPosOccupiedByDyna.Any(t=> (t.charMode == charMode) && (t.currentPos ==1 
            || t.currentPos == 2 || t.currentPos == 3|| t.currentPos == 4))){

                return allCurrPosOccupiedByDyna.Where(t => (t.charMode == charMode) && (t.currentPos == 1
              || t.currentPos == 2 || t.currentPos == 3 || t.currentPos == 4)).ToList();
            }else
            {
                return allCurrPosOccupiedByDyna.Where(t => (t.charMode == charMode) && (t.currentPos == 4
                  || t.currentPos == 5 || t.currentPos == 6 || t.currentPos == 7)).ToList();
            }
        }


        public bool IsFrontDiamond(DynamicPosData dyna)
        {
            if (dyna.currentPos == 1 || dyna.currentPos == 2 || dyna.currentPos == 3 || dyna.currentPos == 4)
                return true; 
            else
                return false; 
        }
        public List<DynamicPosData> GetFirstRowChar(CharMode charMode)
        {
            List<DynamicPosData> allOccupied = GetAllOccupiedbyCharMode(charMode);

            List<DynamicPosData> firstRowChar = new List<DynamicPosData>(); 
            if (allOccupied.Any(t => t.currentPos == 1 || t.currentPos == 2 || t.currentPos == 3))
            {
                firstRowChar = allOccupied.Where(t => t.currentPos == 1 || t.currentPos == 2 || t.currentPos == 3).ToList();
                //return allOccupied.Where(t => t.currentPos == 1 || t.currentPos == 2 || t.currentPos == 3).ToList();
            }
            else if (allOccupied.Any(t => t.currentPos == 4 || t.currentPos == 5 || t.currentPos == 6))
            {
                firstRowChar = allOccupied.Where(t => t.currentPos == 4 || t.currentPos == 5 || t.currentPos == 6).ToList();
            }
            else if (allOccupied.Any(t => t.currentPos == 7))
            {
                firstRowChar = allOccupied.Where(t => t.currentPos == 7).ToList();
            }
            return firstRowChar;
        }

        public List<DynamicPosData> GetAllInFrontINSameParty(DynamicPosData dyna)
        {
            List<DynamicPosData> allDynaInFront = new List<DynamicPosData>();

            List<DynamicPosData> allCharInSameMode = GetAllByCharMode(dyna.charMode);

            if (dyna.currentPos == 1)
            {
                return null; 

            }
            else if (dyna.currentPos == 3 || dyna.currentPos == 2)
            {
                DynamicPosData dynaInFront = allCharInSameMode.Find(t => t.currentPos == 1);
                if (dynaInFront != null)
                {
                    allDynaInFront.Add(dynaInFront);
                }                  
                
            }else if (dyna.currentPos == 5 || dyna.currentPos == 6)
            {
                List<DynamicPosData> dynasInFront = allCharInSameMode.FindAll(t => t.currentPos == 1 || t.currentPos == 2 
                                                                                     || t.currentPos ==3 || t.currentPos ==4).ToList();
                allDynaInFront.AddRange(dynasInFront); 
            }else if (dyna.currentPos == 4)
            {
                List<DynamicPosData> dynasInFront = allCharInSameMode.FindAll(t => t.currentPos == 1 || t.currentPos == 2
                                                                                   || t.currentPos == 3).ToList();
                allDynaInFront.AddRange(dynasInFront);


            }else if (dyna.currentPos == 7)
            {
                List<DynamicPosData> dynasInFront = allCharInSameMode.FindAll(t => t.currentPos == 1 || t.currentPos == 2 || t.currentPos == 3
                                                                                    ||t.currentPos == 4 || t.currentPos == 5 || t.currentPos == 6
                                                                                    ).ToList();
                allDynaInFront.AddRange(dynasInFront);
            }
            return allDynaInFront.Where(t =>t.charMode == dyna.charMode).ToList();
        }

        public List<DynamicPosData> GetAllOccupiedbyCharMode(CharMode _charMode)
        {
            return allCurrPosOccupiedByDyna.FindAll(x => x.charMode == _charMode).ToList();
        }

        public List<DynamicPosData> GetInSameLaneOppParty(CellPosData cellPosData)
        {
            List<int> sameLanePos = gridController.GetSameLanePos(cellPosData.pos);
            List<DynamicPosData> occupiedInSameLane = new List<DynamicPosData>();
            foreach (int lane in sameLanePos)
            {
                DynamicPosData dyna = allCurrPosOccupiedByDyna.Find(t => t.charMode
                            == cellPosData.charMode.FlipCharMode() && t.currentPos == lane);
                if (dyna != null)
                    occupiedInSameLane.Add(dyna);
            }

            if (occupiedInSameLane.Count > 1)
                occupiedInSameLane.OrderByDescending(t => t.currentPos).ToList();
            return occupiedInSameLane;
        }

     
        public bool IsTargetInBackRow(DynamicPosData Dyna)
        {
            if(Dyna.currentPos ==5 || Dyna.currentPos ==6|| Dyna.currentPos == 7)
            {
                return true; 
            }
            return false; 
        }

        #endregion
    }




}

