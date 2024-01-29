using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    [System.Serializable]
    public class TurnData
    {
        public int turnID;
        public StrikeData strikeData;
        public StrikeData doubleStrikeData;
        public List<CharacterController> allCharInCombat; 
        public List<DmgData> allDmgData;
        public List<PosData> allPosdata;

        public TurnData(int turnID, StrikeData strikeData, StrikeData doubleStrikeData,
            List<CharacterController> allCharInCombat, List<DmgData> allDmgData, List<PosData> allPosdata)
        {
            this.turnID = turnID;
            this.strikeData = strikeData;
            this.doubleStrikeData = doubleStrikeData;
            this.allCharInCombat = allCharInCombat;
            this.allDmgData = allDmgData;
            this.allPosdata = allPosdata;
        }
    }

    public class PosData
    {
        public CharController charController;
        public int position;

        public PosData(CharController _charController, int _position)
        {
            charController = _charController;
            position = _position;
        }
    }


}

