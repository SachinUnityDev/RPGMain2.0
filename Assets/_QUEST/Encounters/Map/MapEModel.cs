using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class MapEModel
    {
        public MapENames mapEName;
        public string mapENameStr = "";

        [TextArea(5, 10)]
        public string descTxt;
        public string choiceAStr;
        public string choiceBStr;

        public MapEModel(MapESO mapESO)
        {
            mapEName =mapESO.mapEName;
            mapENameStr = mapESO.mapENameStr;

            descTxt = mapESO.descTxt;   
            choiceAStr= mapESO.choiceAStr;
            choiceBStr= mapESO.choiceBStr;
        }

    }
}