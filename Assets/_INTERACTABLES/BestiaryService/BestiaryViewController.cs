using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI; 

namespace Interactables
{
    public class BestiaryViewController : MonoBehaviour, IPanel, iHelp
    {
        [Header("help")]
        [SerializeField] HelpName helpName;

        [SerializeField] Transform raceBar;
        [SerializeField] Transform scrollNameGO;
        [SerializeField] Transform dropDown;
        [SerializeField] Transform attributePanel;

        [Header("Drop Down related")]

        [Header("Scroll Related")]
        [SerializeField] List<CharModel> selectBestiary = new List<CharModel>();
        [SerializeField] List<CharModel> cultList = new List<CharModel>();
        [SerializeField] List<string> cultOptions = new List<String>(); 
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] Button rightBtn;
        [SerializeField] Button leftBtn;
        public CharModel currBeastOnDisplay; 


        [Header("Global variables")]
        [SerializeField] int index = 0;
        [SerializeField] bool hasInitiated = false; 
        void Start()
        {
            nameTxt = scrollNameGO.GetChild(0).GetComponent<TextMeshProUGUI>(); 
            rightBtn = scrollNameGO.GetChild(1).GetComponent<Button>();
            leftBtn  = scrollNameGO.GetChild(2).GetComponent<Button>();
            rightBtn.onClick.AddListener(OnRightBtnPressed);
            leftBtn.onClick.AddListener(OnLeftBtnPressed);


        }
        public void PopulateOnRaceSelect(RaceType raceType)
        {          
            PopulateBestiaryScroll();
        }                
        void PopulateBestiaryScroll()
        {
            RaceType raceType = BestiaryService.Instance.currSelectRace; 
            //selectBestiary.Clear();           
            if (BestiaryService.Instance.allModel4BestiaryInGame
                    .Any(t => t.raceType == raceType))
            {
                selectBestiary = BestiaryService.Instance.allModel4BestiaryInGame
                    .Where(t => t.raceType == raceType).ToList();
                index = 0;
               
            }
            else if (raceType == RaceType.None)
            {
                selectBestiary = BestiaryService.Instance.allModel4BestiaryInGame;
                index = -1;
               
            }
            else
            {
                nameTxt.text = "No registered chars of the race";
                index = -1;               
            }
            PopulateScollName();
            PopulateCultListWrtRace(raceType); 
        }
        void PopulateCultListWrtRace(RaceType raceType)
        {      
            if (selectBestiary.Count < 1)
                return;
            cultList.Clear();
            foreach (CharModel charModel in selectBestiary)
            {
                if(charModel.raceType == raceType)
                {
                    cultList.Add(charModel); 
                }
            }
            dropDown.GetComponent<CultDropDownEvents>().
                                         PopulateOptions(cultList, this); 
        }      
        void PopulateScollName()
        {
            if(index != -1)
            {
                nameTxt.text = selectBestiary[index].charName.ToString();
                currBeastOnDisplay = selectBestiary[index];
            }                  
            if (index == -1)
                currBeastOnDisplay = null;

            attributePanel.GetComponent<AttributeViewController>().PopulateAttribPanel();

        }
        public void Move2Index(CultureType cultType)
        {
            int i = selectBestiary.FindIndex(t => t.cultType == cultType);
            index = i;
            PopulateScollName();
        }
        void OnRightBtnPressed()
        {
            if (index == -1) return;
            if (index >= selectBestiary.Count - 1)
            {
                index = selectBestiary.Count - 1;
            }
            else
            {
                index++; 
                PopulateScollName();
            }
        }
        void OnLeftBtnPressed()
        {
            if (index == -1) return;

            if (index <= 0)
            {
                index = 0;
            }
            else
            {
                index--; PopulateScollName();
            }
        }

        void PopulateRaceBar()
        {
            int i = 0;

            foreach (RaceSpriteData raceData in CharService.Instance.charComplimentarySO.allRaceSprites)
            {
                if(i < raceBar.childCount)
                {
                    raceBar.GetChild(i).GetComponent<RaceBtnBeastPtrEvents>().Init(raceData);
                    i++;
                }
                else
                {
                    Debug.Log("Low Child count vs Racetype");
                }
            }
        }

        #region IPANEL RELATED 
        public void Load()
        {
           // transform.parent.gameObject.SetActive(true);
          
        }

        public void UnLoad()
        {
           // transform.parent.gameObject.SetActive(false); 
        }

        public void Init()
        {          
             transform.GetChild(1).
                 GetComponent<CompanionViewController>().Init();  // Acts as parent to comp 
            PopulateRaceBar();
            PopulateBestiaryScroll();
        }

        #endregion
        public HelpName GetHelpName()
        {
            return helpName;
        }

    }
}

