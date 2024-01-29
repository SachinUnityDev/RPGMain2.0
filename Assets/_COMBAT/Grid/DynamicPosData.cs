using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common; 

namespace Combat
{
    [System.Serializable]
    public class DynaModel
    {
        public CharMode charMode;
        public int charID;
        public CharNames charName;
        public int currPos;

        public DynaModel(CharMode charMode, int charID, CharNames charName, int currPos)
        {
            this.charMode = charMode;
            this.charID = charID;
            this.charName = charName;
            this.currPos = currPos;
        }
    }

    [System.Serializable]
    public class DynamicPosData
    {

        public CharMode charMode;
        public GameObject charGO;
        public int currentPos;
        public List<int> cellsOccupied = new List<int>();  // also defines charOccupies(Data)
        public Vector3Int FwdtilePos;
        public DynaModel dynaModel; 

        public void SetCurrPos(int pos)
        {
            currentPos= pos;
            dynaModel.currPos= pos;
        }
        public DynamicPosData()
        {

        }

        public DynamicPosData(DynaModel _dynaModel)
        {
            charMode = _dynaModel.charMode;
            currentPos = _dynaModel.currPos;
            charGO = CharService.Instance.GetCharCtrlWithCharID(_dynaModel.charID).gameObject;
            FwdtilePos = GridService.Instance.gridMovement.GetTilePos4Pos(charMode, currentPos);
            // find game object with charName and ID and Assign 
            // get TilePos from cellPos Data ... 

            dynaModel = _dynaModel;
        }

        public DynamicPosData(CharMode _CharMode, GameObject _charGO, int _currentPos,
            List<int> _cellOccupied)
        {
            charMode = _CharMode;
            charGO = _charGO;
            currentPos = _currentPos;
            cellsOccupied.AddRange(_cellOccupied);
            FwdtilePos = GridService.Instance.gridMovement.GetTilePos4Pos(_CharMode, _currentPos);

            CharModel charModel = charGO.GetComponent<CharController>().charModel; 
            int charID = charModel.charID;
            CharNames charName = charModel.charName;


            dynaModel = new DynaModel(charMode, charID,  charName, currentPos); 
        }

    


        public void SaveModel()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
            + "/Grid/DynaModels.txt";
            Debug.Log(" INSIDE DYna save MODEL ");
            if (!File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("does not exist");
                File.CreateText(Application.dataPath + mydataPath);
            }

            string dynaData = JsonUtility.ToJson(dynaModel);
            // string dynaData = "hello world";
            string saveStr = dynaData + "|";

            File.AppendAllText(Application.dataPath + mydataPath, saveStr);

        }

    }

}
