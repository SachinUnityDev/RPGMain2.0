using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    [Serializable]
    public class RosterModel 
    {
        //build from the char Model .. temp no need to load
        public List<CharNames> charInParty = new List<CharNames>();

        public bool IsPartyFull()
        {
            return charInParty.Count == 4;
        }
        public bool IsCharInParty(CharNames charName)
        {
            return charInParty.Contains(charName);
        }   
    }

}

