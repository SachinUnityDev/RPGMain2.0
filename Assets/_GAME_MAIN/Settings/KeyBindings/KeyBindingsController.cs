using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI; 

namespace Common
{

    public class KeyBindingsController : MonoBehaviour
    {
        public KeyBindingSO keyBindingSO;

        [SerializeField] Reset2defaultPtrEvents reset2DefaultPtrEvents; 
        [Header("IS CLIKED")]
        public bool isCLICKED_STATE = false; 


        [Header("Plank references")]
        [SerializeField] GameObject plankPrefab;
        [SerializeField] GameObject townPage1;
        [SerializeField] GameObject townPage2;
        [SerializeField] GameObject combatPage1;
        [SerializeField] GameObject combatPage2;
        [SerializeField] GameObject questPage;
        [SerializeField] GameObject generalpage;

        [SerializeField] List<GameObject> Pages = new List<GameObject>() ; 

        [SerializeField] TextMeshProUGUI textTitle;
        [SerializeField] Button nextPageBtn;
        [SerializeField] Button prevPageBtn;

        [SerializeField] int currPageIndex = 0;

        [Header("INTERNAL")]
        List<KeyBindingData> townBinds = new List<KeyBindingData>();
        List<KeyBindingData> combatBinds = new List<KeyBindingData>();
        List<KeyBindingData> questBinds = new List<KeyBindingData>();
        List<KeyBindingData> generalBinds = new List<KeyBindingData>();

        [Header("Timings")]
        [SerializeField] float nextBtnLastClickTime = 0f;
        [SerializeField] float prevBtnLastClickTime = 0f;


        [SerializeField] KeyBindingData currkeyBindingData; 

        void OnNextPagePressed()
        {

            if (Time.time - nextBtnLastClickTime < 0.25f) return; 
            nextBtnLastClickTime= Time.time;

            if(currPageIndex >= Pages.Count-2)
            {
                nextPageBtn.gameObject.SetActive(false);                
            }
            else
            {
                prevPageBtn.gameObject.SetActive(true);
            }
            currPageIndex++;
            ShowPage(currPageIndex); 
        }
        void OnPrevPagePressed()
        {
            if (Time.time - prevBtnLastClickTime < 0.25f) return;
            prevBtnLastClickTime = Time.time;

            if (currPageIndex <= 1)
            {
                prevPageBtn.gameObject.SetActive(false);                
            }
            else
            {
                nextPageBtn.gameObject.SetActive(true);
            }
            currPageIndex--;
            ShowPage(currPageIndex);
        }

        public void FillCurrentKeys()
        {
           if(keyBindingSO != null)
            {
                // keyBindingModel = new KeyBindingModel(keyBindingSO);
                nextPageBtn.onClick.RemoveAllListeners();
                prevPageBtn.onClick.RemoveAllListeners();
                nextPageBtn.onClick.AddListener(OnNextPagePressed);
                prevPageBtn.onClick.AddListener(OnPrevPagePressed);
                currPageIndex = 0;
                prevPageBtn.gameObject.SetActive(false);
                PopulatePages();            
                ShowPage(0);
            }
        }
        void ChkBindings(KeyCode key)
        {           
            foreach (KeyBindingData keyData in keyBindingSO.allKeyBindingData)
            {
                if (keyData.gameState == currkeyBindingData.gameState)
                {
                    if (keyData.keyPressed == key)
                    {
                        OnKeyBindFound(keyData);
                    }
                }
            }
        }

        void OnKeyBindFound(KeyBindingData keyData)  // bind key action here
        {
            switch (keyData.keyfunc)
            {
                case KeyBindFuncs.None:
                    break;
                case KeyBindFuncs.Autointeract:
                    break;
                case KeyBindFuncs.BackToActor:
                    break;
                case KeyBindFuncs.Bestiary:
                    break;
                case KeyBindFuncs.Calendar:
                    break;
                case KeyBindFuncs.CombatOptions:
                    break;
                case KeyBindFuncs.EndDay:
                    break;
                case KeyBindFuncs.Events:
                    break;
                case KeyBindFuncs.Exit:
                    break;
                case KeyBindFuncs.Fame:
                    break;
                case KeyBindFuncs.Flee:
                    break;
                case KeyBindFuncs.GameManual:
                    break;
                case KeyBindFuncs.GateOfTheDead:
                    break;
                case KeyBindFuncs.Help:
                    break;
                case KeyBindFuncs.Interact:
                    break;
                case KeyBindFuncs.Inventory:
                    break;
                case KeyBindFuncs.Journal:
                    break;
                case KeyBindFuncs.Lore:
                    break;
                case KeyBindFuncs.Map:
                    break;
                case KeyBindFuncs.NextTrack:
                    break;
                case KeyBindFuncs.Pause:
                    break;
                case KeyBindFuncs.Profession:
                    break;
                case KeyBindFuncs.QuestMode:
                    break;
                case KeyBindFuncs.QuestOptions:
                    break;
                case KeyBindFuncs.Quickload:
                    break;
                case KeyBindFuncs.Quicksave:
                    break;
                case KeyBindFuncs.Roster:
                    break;
                case KeyBindFuncs.Skill1:
                    break;
                case KeyBindFuncs.Skill2:
                    break;
                case KeyBindFuncs.Skill3:
                    break;
                case KeyBindFuncs.Skill4:
                    break;
                case KeyBindFuncs.Skill5:
                    break;
                case KeyBindFuncs.Skill6:
                    break;
                case KeyBindFuncs.Skill7:
                    break;
                case KeyBindFuncs.Skill8:
                    break;
                case KeyBindFuncs.Skills:
                    break;
                case KeyBindFuncs.Toggle:
                    break;
                case KeyBindFuncs.ToggleAutowalk:
                    break;
                case KeyBindFuncs.ToggleHex:
                    break;
                case KeyBindFuncs.ToggleLogAttributes:
                    break;
                case KeyBindFuncs.ToggleSpeed:
                    break;
                case KeyBindFuncs.Walk:
                    break;
                case KeyBindFuncs.Potions:
                    break;
                default:
                    break;

            }
            Debug.Log("Key Bind Func action taken" + keyData.keyfunc);
        }

        void OnGUI()   // FOR Action 
        {
            if (Event.current.type == EventType.KeyDown
                && !isCLICKED_STATE)
            {
                if (Event.current.isKey)
                {
                    Debug.Log("INSIDE ON  GUI" + !isCLICKED_STATE);
                    KeyCode key = Event.current.keyCode;
                    ChkBindings(key);
                }
            }
        }
        void ShowPage(int num)
        {
            if (num == 1 || num == 0)
            {
                textTitle.text = "TOWN"; 
            }
            if (num == 2 || num == 3)
            {
                textTitle.text = "COMBAT";
            }
            if(num == 4)
            {
                textTitle.text = "QUEST";
            }
            if(num == 5)
            {
                textTitle.text = "GENERAL";
            }
            TogglePage(num); 

        }
        void TogglePage(int num)
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                if(i != num)
                {
                    Pages[i].SetActive(false); 
                }
                else
                {
                    Pages[i].SetActive(true);
                    currkeyBindingData = Pages[i].GetComponentInChildren<KeyBindPanelEvents>().GetKeyBindingData(); 
                }
            }
        }
        void PopulatePages()
        {
            PopulateTownPages();
            PopulateCombatPages();
            PopulateGeneralPages();
            PopulateQuestPages();
            Pages.Clear();            
            Pages.Add(townPage1);         
            Pages.Add(townPage2);
            Pages.Add(combatPage1);
            Pages.Add(combatPage2);
            Pages.Add(questPage);
            Pages.Add(generalpage);
            reset2DefaultPtrEvents.Init(this); 
        }
        void PopulateTownPages()
        {
            int i = 0;
            
            townBinds  = keyBindingSO.allKeyBindingData
                            .Where(t => t.gameState == GameScene.InTown).ToList(); 
            
            foreach (Transform Child in townPage1.transform)
            {
                KeyBindingData keyData = townBinds[i];
                if (keyData.gameState == GameScene.InTown)
                {
                    textTitle.text = "TOWN";
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }            
            foreach (Transform Child in townPage2.transform)
            {
                KeyBindingData keyData = townBinds[i];
                if (keyData.gameState == GameScene.InTown)
                {
                    textTitle.text = "TOWN";
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }
        }
        void PopulateCombatPages()
        {
            int i = 0;
            combatBinds = keyBindingSO.allKeyBindingData
                                .Where(t => t.gameState == GameScene.InCombat).ToList();

            foreach (Transform Child in combatPage1.transform)
            {
                KeyBindingData keyData = combatBinds[i];
                if (keyData.gameState == GameScene.InCombat)
                {
                    textTitle.text = "COMBAT"; 
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }
            foreach (Transform Child in combatPage2.transform)
            {
                KeyBindingData keyData = combatBinds[i];
                if (keyData.gameState == GameScene.InCombat)
                {
                    textTitle.text = "COMBAT";
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }
        }
        void PopulateQuestPages()
        {
            int i = 0;
            questBinds = keyBindingSO.allKeyBindingData
                                .Where(t => t.gameState == GameScene.InQuestRoom).ToList();
            foreach (Transform Child in questPage.transform)
            {
                KeyBindingData keyData = questBinds[i];
                if (keyData.gameState == GameScene.InQuestRoom)
                {
                    textTitle.text = "QUEST";
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }
        }
        void PopulateGeneralPages()
        {
            int i = 0;
            generalBinds = keyBindingSO.allKeyBindingData
                                .Where(t => t.gameState == GameScene.None).ToList();
            foreach (Transform Child in generalpage.transform)
            {
                KeyBindingData keyData = generalBinds[i];
                if (keyData.gameState == GameScene.None)
                {
                    textTitle.text = "GENERAL";
                    Child.GetComponent<KeyBindPanelEvents>().PopulateKeyBindings(keyData, this);
                    i++;
                }
            }
        }
    }
    public enum KeyBindFuncs
    {
        None,
        Autointeract,
        BackToActor,
        Bestiary,
        Calendar,
        CombatOptions,
        EndDay,
        Events,
        Exit,
        Fame,
        Flee,
        GameManual,
        GateOfTheDead,
        Help,
        Interact,
        Inventory,
        Journal,
        Lore,
        Map,
        NextTrack,
        Pause,
        Profession,
        QuestMode,
        QuestOptions,
        Quickload,
        Quicksave,
        Roster,
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
        Skill6,
        Skill7,
        Skill8,
        Skills,
        Toggle,
        ToggleAutowalk,
        ToggleHex,
        ToggleLogAttributes,
        ToggleSpeed,
        Walk,
        Potions,

    }

}
