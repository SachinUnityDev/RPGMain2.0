using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class RosterModel 
    {
        // Bottom BP
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

