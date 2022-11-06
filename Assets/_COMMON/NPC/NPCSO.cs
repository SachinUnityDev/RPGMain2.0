using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [CreateAssetMenu(fileName = "NPCSO", menuName = "Character Service/NPCSO")]

    public class NPCSO : ScriptableObject
    {
        public int npcID;
        public NPCNames npcName;
        public GameObject npcPrefab;  // change to prefab 
        public Sprite npcSprite;
        public Sprite npcHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;


        [Header("Interaction related")]
        public string InteractHeading; 
        public List<string> InteractionOptions = new List<string>();




        //[Header("DEFAULT PROVISION")]
        //public List<ItemData> provisionItems = new List<ItemData>();

        //[Header("Gift")]
        //// money and Item 
        //public List<ItemData> giftItems = new List<ItemData>();
        //public Currency giftCurrencyShare = new Currency();

        //[Header("Companion PreReq")]
        //public List<ItemData> CompanionPreReq = new List<ItemData>();


    }
}

