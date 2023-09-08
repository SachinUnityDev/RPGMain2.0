using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
namespace Common
{
    [System.Serializable]
    public class lineData
    {
        public string barkLineTxt = "";
        public AudioClip audioClip; 

    }
    public class charNLineData
    {
        public CharNames charName;
        public NPCNames npcName; 
        public lineData lineData;
    }

    [CreateAssetMenu(fileName = "Bark", menuName = "BarksService/BarkLines")]
    public class BarkSO : ScriptableObject
    {
        //public BarkLines[] barkLines;   

        //BarkData 
        public BarkType barkTrigger; 
        public int barkID; 
        public float timeDelay;// time delay if nothing is clicked 
        public CharNames charInBark;
        public NPCNames npcInBark;
        
        public List<lineData> BarkLine = new List<lineData>();
        public List<charNLineData> allCharNLineData = new List<charNLineData>();





    }
    [System.Serializable]
    public class BarkLines
    {
        public string txt;
        public GameObject barkCollider;
        public float timer;

    }




}





//* barktab 
// owned by collider of building, nPC , char, Location , game state (quest prep)
// multi line plural 
// singular multi Options -----Minami 
// multichar and multiLines



// call to barkService not controller.. will contain the bark ID n charNames..  
//bark ID .. bark ID will find in


// StartBark (BarkID)  central repo 
// StartBark(BarkID, buildings) // .. this will mark the location for search in barkService
// StartBark(BarkID, List<CharNames> charNames)...// this will mark searches 
// StartBark(BarkID, location); 
// StartBark(BarkID, Landscape); 


