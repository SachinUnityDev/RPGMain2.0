using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [SerializeField]
    public class CharKillsData
    {
        public int charKilledID;
        public int causeByCharID;

        public CharKillsData(int charKilledID, int causeByCharID)
        {
            this.charKilledID = charKilledID;
            this.causeByCharID = causeByCharID;
        }
    }

    [SerializeField]
    public class CharMainModel
    {
        public List<CharKillsData> allCharKills = new List<CharKillsData>();


    }
}