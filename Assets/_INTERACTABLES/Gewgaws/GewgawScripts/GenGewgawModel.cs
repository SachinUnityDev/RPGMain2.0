using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class GenGewgawModel
    {
        [Header("Fundamentals")]
        public GenGewgawNames genGewgawName;
        public PrefixNames prefixName;
        public GenGewgawMidNames gewgawName;
        public SuffixNames suffixName;

        public GenGewgawState genGewgawState = GenGewgawState.None;

        [Header("during runtime")]
        public GenGewgawQ genGewgawQ;   // gewgaw quality is defined when instance is created
        public GewgawSlotType gewgawSlotType;
        public CharNames charName;
        public SlotType invType;

        [Header("RESTRICTIONS")]
        public List<ClassType> classRestrictions = new List<ClassType>();
        public List<CultureType> cultureRestrictions = new List<CultureType>();
        public List<RaceType> raceRestrictions = new List<RaceType>();

        [Header("COST")]
        public Currency cost;
        public float fluctuation;
        public BronzifiedRange currPrchPrice;

        [Header("Max Instance")]
        public int maxWorldInstance;


        [Header("Desc")]
        public string desc = "";

       
        // to be filled using code // factory for the prefix and suffix will get acces to it and fill it 
       // public List<string> allLines = new List<string>();

        public GenGewgawModel(GenGewgawSO gewgawSO, GenGewgawQ gewgawQ)
        {
            genGewgawQ = gewgawQ;
            SetMaxInstance(gewgawQ);
            this.genGewgawName = gewgawSO.genGewgawName;
            this.prefixName = gewgawSO.prefixName;
            this.gewgawName = gewgawSO.gewgawName;
            this.suffixName = gewgawSO.suffixName;

            this.gewgawSlotType = gewgawSO.gewgawSlotType;

            this.classRestrictions = gewgawSO.classRestrictions.DeepClone();
            this.cultureRestrictions = gewgawSO.cultureRestrictions.DeepClone();
            this.raceRestrictions = gewgawSO.raceRestrictions.DeepClone();
            this.cost = gewgawSO.cost;
            this.fluctuation = 20f;
           
            this.desc = gewgawSO.desc;
            //this.allLines = gewgawSO..DeepClone();
        }

    
        void SetMaxInstance(GenGewgawQ genGewgawQ)
        {
            // sagaic its 1, epic 3 , folklyric =6 , lyric =6 , poetic = 3, 
            switch (genGewgawQ)
            {
                case GenGewgawQ.Lyric:
                    maxWorldInstance = 6; 
                    break;
                case GenGewgawQ.Folkloric:
                    maxWorldInstance = 6;
                    break;
                case GenGewgawQ.Epic:
                    maxWorldInstance = 3; 
                    break;
                default:
                    break;
            }

        }
    }
}

