using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
 {
    [System.Serializable]
    public class Spread 
    {
        public int min;
        public int max; 

        public Spread(int _min, int _max)
        {
            min = _min;
            max = _max; 
        }
    }
 }
