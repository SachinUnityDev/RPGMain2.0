using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


namespace Town
{   
    public class WoodGameController1 : MonoBehaviour
    {

        public WoodGameRank currWoodGameRank;
        public int currentGameSeq;
        public int currGameJobExp;
        public WoodGameData currWoodGameData;
        [Header("Next game Sequence")]
        WoodGameRank nextWoodGameRank;
        int nextGameSeq;

        [SerializeField] WoodGameSO woodGameSO;
        [SerializeField] WoodGameController1 woodGameController;


        [SerializeField] GameObject startPanel;
        [Header("PANEL")]
        //[SerializeField] Button StartBtn;
        [SerializeField] Button ContinueBtn;
        [SerializeField] Button Back2TownBtn;

        [Header("Wood Game Model")]
        public WoodGameModel woodGameModel;

        [SerializeField] bool isGameInitDone = false;        
        [SerializeField] GameObject woodGamePreFab;


        [SerializeField] GameObject woodGameGO;
        WoodGameView1 woodGameView;
        public void StartGame()
        {
            Debug.Log(" wood game Started");
            // Init Dialogue
            if (isGameInitDone) return; // return multiple clicks
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
            GetLoadGameData();
            woodGameGO.GetComponent<WoodGameView1>().NewGameInit(currWoodGameData); 
        }

        void Start()
        {
        //    ContinueBtn.gameObject.SetActive(false);
        //    Back2TownBtn.gameObject.SetActive(false);
            //StartBtn.onClick.AddListener(OnStartNewGamePressed);
            //ContinueBtn.onClick.AddListener(OnContinueGamePressed);
            //Back2TownBtn.onClick.AddListener(OnBack2TownPressed);

        }

        public void OnBack2TownPressed()
        {
            ExitGame();
        }

        public void ChgGameParam2Next()
        {
            if (currentGameSeq < 3)
            {
                currentGameSeq = currentGameSeq + 1;
                currWoodGameData.gameSeq = currentGameSeq;
                // woodGameController.StartGame();
            }
            else if (currentGameSeq >= 3 && currWoodGameRank != WoodGameRank.Master)
            {
                currentGameSeq = 1;
                currWoodGameRank = currWoodGameRank + 1;
            
                Debug.Log("you are DONE FOR THE DAY !");
                //   woodGameController.ControlGameState(WoodGameState.ExitGame);
            }
            else if (currentGameSeq >= 3 && currWoodGameRank == WoodGameRank.Master)
            {
                Debug.Log("Game play to be continued at master");
                currentGameSeq = 1;
            }
            // get data from SO
            currWoodGameData = woodGameSO.GetWoodGameData(currentGameSeq, currWoodGameRank).DeepClone(); 
            woodGameView.NewSeqInit(currWoodGameData);  
            woodGameView.OnContinuePressed();
        }

        public void GameViewInit()
        {
           
            ContinueBtn.gameObject.SetActive(true);
            Back2TownBtn.gameObject.SetActive(true);            
           
        }
        public void ExitGame()
        {
            ContinueBtn.gameObject.SetActive(false);
            SaveGameData();
 
        }
        void GetLoadGameData()
        {
            WoodGameModel woodGameModel = RestoreState();
            currWoodGameData = new WoodGameData();

            if (woodGameModel == null)
            {
                currentGameSeq = 1;
                currGameJobExp = 0;
                currWoodGameRank = WoodGameRank.Apprentice;
                currGameJobExp = 0;
            }
            else
            {
                currentGameSeq = woodGameModel.currentGameSeq;
                currGameJobExp = woodGameModel.currGameJobExp;
                currWoodGameRank = woodGameModel.currWoodGameRank;
            }
            foreach (WoodGameData wooddata in woodGameSO.allWoodData)
            {
                if (wooddata.woodGameRank == currWoodGameRank)
                {
                    if (wooddata.gameSeq == currentGameSeq)
                    {
                        currWoodGameData = wooddata.DeepClone();
                    }
                }
            }
        }

        WoodGameModel RestoreState()
        {
            // string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.currSlotSelected.ToString()
            // + "/Grid/DynaModels.txt";
            WoodGameModel woodGameModel = null;
            string mydataPath = "/SAVE_SYSTEM/savedFiles/WoodGameModel.txt";

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
            woodGameModel = new WoodGameModel(currWoodGameRank, currentGameSeq, currGameJobExp);
            woodGameModel.SaveModel(woodGameModel); 
        }
    }
}