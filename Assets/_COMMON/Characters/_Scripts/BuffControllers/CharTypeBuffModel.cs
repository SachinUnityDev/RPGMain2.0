using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CharTypeBuffModel
    {
        public List<RaceCultClassBuffData> allBuffData = new List<RaceCultClassBuffData>();
        public int charID = -1; 

        public CharTypeBuffModel(int charID)
        {
            this.charID = charID;
        }
    }
}