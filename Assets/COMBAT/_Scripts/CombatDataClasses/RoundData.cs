using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RoundData
    {
        public List<TurnData> turnData;

        public RoundData(List<TurnData> _turnData)
        {
            _turnData = turnData;
        }
    }

}
