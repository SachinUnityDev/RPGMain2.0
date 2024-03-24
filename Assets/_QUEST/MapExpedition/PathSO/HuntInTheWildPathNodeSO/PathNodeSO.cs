using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public class PathNodeSO : ScriptableObject
    {
        public int nodeSeq;
        public bool isChecked;
        public bool isSuccess; 
       

        [Header(" TimeChg On Node Enter")]
        public int noOfHalfDaysChgOnEnter;

        [Header(" TimeChg On Node Exit")]
        public int noOfHalfDaysChgOnExit;


    }
}