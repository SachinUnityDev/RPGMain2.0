using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CharSocketData
    {
        public CharNames charName;
        public GemName gemDivineSocket1;
        public GemName gemDivineSocket2;
        public GemName gemSupportSocket3;

        public CharSocketData(CharNames charName)
        {
            this.charName = charName;
            this.gemDivineSocket1 = GemName.None;
            this.gemDivineSocket2 = GemName.None;
            this.gemSupportSocket3 = GemName.None;
        }
    }

    public class ArmorModel
    {
        public List<CharSocketData> allCharSocketData = new List<CharSocketData>();

        public CharSocketData GetSocketData(CharNames charName)
        {
            CharSocketData charSocketData = allCharSocketData.Find(t => t.charName == charName);
            if (charSocketData != null)
                return charSocketData;
            else
            {
                CharSocketData charSocketData1 = new CharSocketData(charName);
                return charSocketData1;
            }
                
            
        }




        public void Init()
        {

        }

    }
}

