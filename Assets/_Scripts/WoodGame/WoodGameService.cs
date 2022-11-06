using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class WoodGameService : MonoBehaviour
{

    private static WoodGameService instance;
    public static WoodGameService Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as WoodGameService;
        }
        else
        {
            Destroy(this);
        }
    }


    public WoodGameRank currWoodGameRank;
    public int currentGameSeq;
    public int currGameJobExp;
    public WoodGameData currWoodGameData;
    [Header("Next game Sequence")]
    WoodGameRank nextWoodGameRank;
    int nextGameSeq;

    [SerializeField] WoodGameSO woodGameSO; 
    [SerializeField] WoodCuttingController woodGameController;


    [SerializeField] GameObject startPanel;
    [Header("PANEL")]
    [SerializeField] Button StartBtn;
    [SerializeField] Button ContinueBtn;
    [SerializeField] Button Back2TownBtn; 

    [Header("Game Control")]
    public WoodGameState gameState; 

    void Start()
    {
        ContinueBtn.gameObject.SetActive(false);
        Back2TownBtn.gameObject.SetActive(false);
        StartBtn.onClick.AddListener(OnStartNewGamePressed);
        ContinueBtn.onClick.AddListener(OnContinueGamePressed);
        Back2TownBtn.onClick.AddListener(OnBack2TownPressed);

    }

    public void OnBack2TownPressed()
    {
        ExitGame();
    }

    public void OnStartNewGamePressed()
    {
        Debug.Log("START BTN PRESSED");
        woodGameController.ControlGameState(WoodGameState.Running); 
        GetLoadGameData();
      
        woodGameController.StartGame();// check the diff..        
        ContinueBtn.gameObject.SetActive(true);
        Back2TownBtn.gameObject.SetActive(true);
        StartBtn.gameObject.SetActive(false); 
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
            woodGameController.ControlGameState(WoodGameState.ExitGame);
        }
        else if (currentGameSeq >= 3 && currWoodGameRank == WoodGameRank.Master)
        {
            Debug.Log("GAME OVER CONDITION TO BE DISCUSSED");
            currentGameSeq = 1;
        }
    }

    public void OnContinueGamePressed()
    {
        Debug.Log("CONTINUE BTN PRESSED");
        woodGameController.StartGame();  // Start current game
    }
    public void ExitGame()
    { 
        ContinueBtn.gameObject.SetActive(false);
        SaveGameData();
        SceneManager.LoadScene(1);// TEMPORARY SHOULD BE MANAGED BY SCENE MANAGER  
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
            if(wooddata.woodGameRank == currWoodGameRank)
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
        }
        return woodGameModel; 
    }

    void SaveGameData()
    {
        WoodGameModel woodGameModel = new WoodGameModel(currWoodGameRank, currentGameSeq, currGameJobExp);
        woodGameModel.SaveModel(woodGameModel); 
    }





}


