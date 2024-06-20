using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Common;
using Quest;
using DG.Tweening;
using System.Threading.Tasks;

namespace Town
{   
    public class WoodGameController1 : MonoBehaviour
    {

        [Header(" Wood game Save Params")]
        public JobRank currWoodGameRank;
        public int currentGameSeq;
        public int netGameExp;

        [Header("CAN PLAY THE GAME")]
        public bool isLocked; 

        [Header(" wood game Data Loaded ref")]
        public WoodGameData currWoodGameData;
       
        [Header("Next game Sequence")]
        JobRank nextWoodGameRank;
        int nextGameSeq;

        public WoodGameSO woodGameSO;

        [SerializeField] GameObject startPanel;
        [Header("PANEL")]
        //[SerializeField] Button StartBtn;
        [SerializeField] Button ContinueBtn;
        [SerializeField] Button Back2TownBtn;

        [Header("Wood Game Model")]
        public JobModel jobModel;

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

            Transform parent = GameObject.FindGameObjectWithTag("Canvas").transform; 
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

            GetLoadGameData();
            woodGameGO.GetComponent<WoodGameView1>().NewGameInit(currWoodGameData, this); 

        }
        public void ExitGame(WoodGameData woodGameData)
        {
            currWoodGameData= woodGameData;
            currentGameSeq = woodGameData.gameSeq;
            woodGameData.netGameExp += woodGameData.lastGameExp;
            netGameExp = woodGameData.netGameExp;
            woodGameData.lastGameExp = 0;

            if ((woodGameData.netGameExp) >= currWoodGameData.maxJobExpR)
            {
                if (woodGameData.woodGameRank != JobRank.Master)
                {
                    woodGameData.woodGameRank++;                   
                }
            }
            currWoodGameRank = woodGameData.woodGameRank;
            SaveGameData();
            Destroy(woodGameGO, 0.4f);

            if(WelcomeService.Instance.isWelcomeRun)
            {
                BuildingIntService.Instance.UnLockDiaInBuildNPC(BuildingNames.House, NPCNames.Khalid, DialogueNames.AttendJob, true);
                WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Report back to Khalid");
            }

            // reset params
            isGameInitDone= false;  
            isLocked= true;
            CalendarService.Instance.On_EndDayClick(BuildingNames.None);
            
        }
        void UnLockOnDayChg()
        {
            isLocked= false;
        }


        public JobModel GetLoadGameData()
        {
            JobModel woodGameModel = RestoreState();
            currWoodGameData = new WoodGameData();

            if (woodGameModel == null)
            {
                currentGameSeq = 1;
                netGameExp = 0;
                currWoodGameRank = JobRank.Apprentice;               
            }
            else
            {
                currentGameSeq = 1;
                netGameExp = woodGameModel.currGameJobExp;
                currWoodGameRank = woodGameModel.currJobRank;
            }
            foreach (WoodGameData wooddata in woodGameSO.allWoodData)
            {
                if (wooddata.woodGameRank == currWoodGameRank)
                {
                    if (wooddata.gameSeq == currentGameSeq)
                    {
                        currWoodGameData = wooddata.DeepClone();
                        if(woodGameModel != null)
                        {
                            currWoodGameData.netGameExp = netGameExp;
                        }
                    }
                }
            }
            currWoodGameData.isPlayedOnce = woodGameModel.isPlayedOnce;
            return woodGameModel; 
        }

        JobModel  RestoreState()
        {

            string mydataPath = "/_SaveService/savedFiles/WoodGameModel.txt";
            if (File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("File found!");
                string str  = File.ReadAllText(Application.dataPath + mydataPath);             
                if (string.IsNullOrEmpty(str))
                {
                    Debug.Log("string empty!");
                }
                else
                {
                    jobModel = JsonUtility.FromJson<JobModel>(str);
                }
            }
            else
            {
                Debug.Log("File Does not Exist");
                jobModel = new JobModel(woodGameSO);
            }
            return jobModel;         
        }

        void SaveGameData()
        {
            
               
            jobModel = new JobModel(currWoodGameRank, currentGameSeq, netGameExp
                                          , CalendarService.Instance.calendarModel.dayInYear
                                          , CalendarService.Instance.calendarModel.currentMonth);
            jobModel.SaveModel(jobModel); 
        }
    }
}