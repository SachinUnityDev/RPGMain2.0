using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
using Intro;
using TMPro;
using DG.Tweening;
using System.Linq; 

namespace Town
{

    public class ClearMindController : MonoBehaviour, IPanel
    {

        const int LEVEL_MULTIPLIER = 1; 
        [Header("CLEAR MIND BTN")]
        [SerializeField] Button clearMindBtn;

        [Header("Price GO")]
        [SerializeField] GameObject moneyNeededGO;
        [SerializeField] GameObject playerMoneyGO; 
        [SerializeField] GameObject minamiMoneyGO;

        [Header("Top Panel related")]
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] Button leftButton;
        [SerializeField] Button rightButton; 

        [SerializeField] GameObject charSelectPanel;
        [SerializeField] Transform charContent;

        [Header("Global variables")]

        [SerializeField] int index; 
        
        public CharModel selectChar;

        [SerializeField] Currency minamiMoney;
        [SerializeField] Currency playerMoney;
        [SerializeField] List<CharModel> charForClearMind = new List<CharModel>();


        void Start()
        {
            index =0; 
            leftButton.onClick.AddListener(OnLeftBtnPressed);
            rightButton.onClick.AddListener(OnRightBtnPressed);
            clearMindBtn.onClick.AddListener(OnClearMindPressed);
        }
        void OnLeftBtnPressed()
        {
            index--;
            if (index < 0)
            {
                index = 0; return;
            }
            else
            {
                PopulateCharData();
            }
        }

        void OnRightBtnPressed()
        {
            index++;
            if (index > (charForClearMind.Count - 1))
            {
                index = charForClearMind.Count - 1; return;
            }
            else
            {
                PopulateCharData();
            }
        }


        void OnClearMindPressed()
        {
            TransactMoney();
            charForClearMind.Remove(selectChar);
            charForClearMind.ToList();
            // lvl fx 
            // Temp traits clearing
            Chk4CMListCount();
        }


        void TransactMoney()
        {
            int level = selectChar.charLvl;
            Currency moneyNeeded = new Currency(LEVEL_MULTIPLIER * level, 0);
            //EcoServices.Instance.PayMoney2NPC(moneyNeeded, NPCNames.MinamiTheSoothsayer);
            EcoService.Instance.DebitPlayerStash(moneyNeeded);

            //display updates
            moneyNeededGO.FillCurrencyUI(new Currency(0, 0));

            DisplayMinamiMoney();
            DisplayPlayerMoney(); 
        }
        void DisplayMoneyNeeded()
        {
            int level = selectChar.charLvl;
            Currency moneyNeeded = new Currency(LEVEL_MULTIPLIER*level, 0);
            moneyNeededGO.FillCurrencyUI(moneyNeeded); 
        }
        
        void DisplayMinamiMoney()
        {
            //minamiMoney = EcoServices.Instance.GetMoneyInAct(NPCNames.MinamiTheSoothsayer);
            //minamiMoneyGO.FillCurrencyUI(minamiMoney);
        }

        void DisplayPlayerMoney()
        {
            playerMoney = EcoService.Instance.GetMoneyAmtInPlayerStash();
            playerMoneyGO.FillCurrencyUI(playerMoney);
        }
        void PopulateCharData()
        {    // get name 
            charSelectPanel.SetActive(true); 
            selectChar = charForClearMind[index]; 

            CharacterSO charSO = CharService.Instance.allCharSO.GetCharSO(selectChar.charName);

            nameTxt.text = charSO.charNameStr;
            charContent.GetChild(0).GetComponent<Image>().sprite = charSO.charSprite;
            DisplayMoneyNeeded(); // curr char Selected has Value
        }
      

        public void Init()
        {
            DisplayMinamiMoney(); 
            DisplayPlayerMoney();
            GetCharList4CM();
            Chk4CMListCount();


        }

        void Chk4CMListCount()
        {
            if (charForClearMind.Count < 1)
                NoAvailableCharForCM();
            else
            {
                clearMindBtn.GetComponent<Image>().DOFade(1f, 0.1f);
                clearMindBtn.GetComponent<Button>().enabled = true;
                PopulateCharData(); // will be in load
            }
        }

        void NoAvailableCharForCM()
        {
            nameTxt.text = "NO AVAILABLE CHARACTERS";
            // setactive false remaining
            charSelectPanel.SetActive(false);
            clearMindBtn.GetComponent<Image>().DOFade(0.5f, 0.1f);
            clearMindBtn.GetComponent<Button>().enabled = false; 
        }
        void GetCharList4CM()
        {
            charForClearMind.Clear(); 
            foreach (CharModel c in TownService.Instance.townController.allCharInTown)
            {
                if (c.charLvl > 1)
                {
                    charForClearMind.Add(c); 
                }
            }
            
        }
        public void Load()
        {

        }

        public void UnLoad()
        {
         
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    Debug.Log("Clear MIND INIT TEST");
            //    Init(); 
            //}
        }

    }



}
