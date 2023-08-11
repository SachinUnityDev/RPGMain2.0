using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Common;

namespace Town
{   
    public class WoodGameController1 : MonoBehaviour
    {

        [Header(" Wood game Save Params")]
        public WoodGameRank currWoodGameRank;
        public int currentGameSeq;
        public int netGameExp;

        [Header("CAN PLAY THE GAME")]
        public bool isLocked; 

        [Header(" wood game Data Loaded ref")]
        public WoodGameData currWoodGameData;
       
        [Header("Next game Sequence")]
        WoodGameRank nextWoodGameRank;
        int nextGameSeq;

        public WoodGameSO woodGameSO;

        [SerializeField] GameObject startPanel;
        [Header("PANEL")]
        //[SerializeField] Button StartBtn;
        [SerializeField] Button ContinueBtn;
        [SerializeField] Button Back2TownBtn;

        [Header("Wood Game Model")]
        public WoodGameModel woodGameModel;

        [SerializeField] bool isGameInitDone = false;

        [Header(" Wood Game Prefab Ref")]
        [SerializeField] GameObject woodGamePreFab;
        [SerializeField] GameObject woodGameGO;
        WoodGameView1 woodGameView;
        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay -= (int day) => UnLockOnDayChg();
            CalendarService.Instance.OnStartOfCalDay += (int day) => UnLockOnDayChg();
            isLocked = false;
        }
        public void StartGame()
        {
            if (isGameInitDone) return; // return multiple clicks
            if(isLocked) return;

            Transform parent = GameObject.FindGameObjectWithTag("TownCanvas").transform; 
            woodGameGO = Instantiate(woodGamePreFab);

            woodGameView = woodGameGO.GetComponent<WoodGameView1>();
            woodGameGO.transform.SetParent(parent);

            //UIControlServiceGeneral.Instance.SetMaxSiblingIndex(diaGO);
            int index = woodGameGO.transform.parent.childCount - 2;
            woodGameGO.transform.SetSiblingIndex(index);
            RectTransform woodRect = woodGameGO.GetComponent<RectTransform>();

            woodRect.anchorMin = new Vector2(0, 0);
            woodRect.anchorMax = new Vector2(1, 1);
            woodRect.pivot = new Vector2(0.5f, 0.5f);
            woodRect.localScale = Vector3.one;
            woodRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            woodRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
            isGameInitDone = true;
            GetLoadGameData(); // curr wood game Data populated for controller 
           
            woodGameGO.GetComponent<WoodGameView1>().NewGameInit(currWoodGameData, this); 
        }
        public void ExitGame(WoodGameData woodGameData)
        {
            this.currWoodGameData= woodGameData;
            currentGameSeq = woodGameData.gameSeq;
            currWoodGameRank = woodGameData.woodGameRank; 
            netGameExp = woodGameData.netGameExp;

            SaveGameData();
            Destroy(woodGameGO, 0.4f);

            // reset params
            isGameInitDone= false;  
            isLocked= true;
            CalendarService.Instance.On_EndDayClick(); 
            // destroy prefab 
            // block the game for one day 
        }
        void UnLockOnDayChg()
        {
            isLocked= false;
        }


        void GetLoadGameData()
        {
            WoodGameModel woodGameModel = RestoreState();
            currWoodGameData = new WoodGameData();

            if (woodGameModel == null)
            {
                currentGameSeq = 1;
                netGameExp = 0;
                currWoodGameRank = WoodGameRank.Apprentice;
                netGameExp = 0;
            }
            else
            {
                currentGameSeq = 1;
                netGameExp = woodGameModel.currGameJobExp;
                currWoodGameRank = woodGameModel.currWoodGameRank;
            }
            foreach (WoodGameData wooddata in woodGameSO.allWoodData)
            {
                if (wooddata.woodGameRank == currWoodGameRank)
                {
                    if (wooddata.gameSeq == currentGameSeq)
                    {
                        currWoodGameData = wooddata.DeepClone();
                        currWoodGameData.netGameExp = netGameExp; // getting data from the save model .. by default its 0 
                    }
                }
            }
        }

        WoodGameModel RestoreState()
        {
 
            WoodGameModel woodGameModel = null;
            string mydataPath = "/_SaveService/savedFiles/WoodGameModel.txt";

            if (File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("File found!");
                string str = File.ReadAllText(Application.dataPath + mydataPath);

                if (string.IsNullOrEmpty(str))
                {
                    Debug.Log("string empty!");
                }
                else
                {
                    woodGameModel = JsonUtility.FromJson<WoodGameModel>(str);
                }
            }
            else
            {
                Debug.Log("File Does not Exist");
                woodGameModel= new WoodGameModel(woodGameSO);
            }
            return woodGameModel;
        }

        void SaveGameData()
        {
            woodGameModel = new WoodGameModel(currWoodGameRank, currentGameSeq, netGameExp);
            woodGameModel.SaveModel(woodGameModel); 
        }
    }
}