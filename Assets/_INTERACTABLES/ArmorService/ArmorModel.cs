using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CharSocketData
    {
        public CharNames charName;
        public GemNames gemDivineSocket1;
        public GemNames gemDivineSocket2;
        public GemNames gemSupportSocket3;

        public CharSocketData(CharNames charName)
        {
            this.charName = charName;
            this.gemDivineSocket1 = GemNames.None;
            this.gemDivineSocket2 = GemNames.None;
            this.gemSupportSocket3 = GemNames.None;
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

