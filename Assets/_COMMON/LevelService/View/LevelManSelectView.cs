using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using Interactables;


namespace Common
{
    public class LevelManSelectView : MonoBehaviour, IPanel
    {
     //   [SerializeField] Button lvlUpBtn;

        [SerializeField] Button opt1Btn;
        [SerializeField] Button opt2Btn;


        [SerializeField] BtmCharViewController btmCharViewController;

        [Header("Global Variables")]
        [SerializeField] int index = 0;
        public List<LvlDataComp> allPendingOptions = new List<LvlDataComp>();


        [Header("Lvl Button Related")]
        [SerializeField] Sprite spritePlus;
        [SerializeField] Sprite spriteMinus;
        [SerializeField] Sprite spriteN;
        [SerializeField] Image lvlBtnImg;
        bool isPanelOpen = false;
        [SerializeField] bool isButtonActive = true;
        [SerializeField] int pendingCount = 0;

        [Header("CURR CHAR")]
        [SerializeField] CharNames charName;
        [SerializeField] Levels currLvl = Levels.Level0;
        [SerializeField] CharModel charModel;

   
        void Start()
        {
            InvService.Instance.OnCharSelectInvPanel += FillOptionPendingList;

            opt1Btn.onClick.AddListener(OnOptBtn1Pressed);
            opt2Btn.onClick.AddListener(OnOptBtn2Pressed);
            UnLoad();
        }

        public void LevelManSelectInit()
        {
            // get abbas charModel on inv open
            // make it work only for allies first
            CharModel charModel =
             CharService.Instance.GetAbbasController(CharNames.Abbas).charModel;
            FillOptionPendingList(charModel);
        }
        void FillOptionPendingList(CharModel charModel)
        {
            if (charModel == null) return;
            charName = charModel.charName;
            this.charModel = charModel;
            LvlStackData lvlStackData =
                    LevelService.Instance.lvlModel.GetLvlStackData(charModel.charName);
            if (lvlStackData == null) return;

            if (lvlStackData.allOptionsPending.Count <= 0)
            {
                Debug.Log("No options are available");
                isButtonActive = false;
                lvlBtnImg.sprite = spriteMinus;
                UnLoad();
                return;
            }
            else
            {
                isButtonActive = true;
                lvlBtnImg.sprite = spritePlus;
            }
            allPendingOptions.Clear();
            foreach (LvlDataComp lvlDataComp in lvlStackData.allOptionsPending)
            {
                if (lvlDataComp.allStatDataOption1.Count > 0
                    || lvlDataComp.allStatDataOption2.Count > 0)
                {
                    allPendingOptions.Add(lvlDataComp);
                }
            }
            index = 0;
            FillOptionsPanel();
        }

        void FillOptionsPanel()
        {
            if (index >= allPendingOptions.Count)
            {
                return;
            }
            string str1 = "";
            foreach (LvlData stat in allPendingOptions[index].allStatDataOption1)
            {
                str1 += stat.val + " " + stat.attribName;

            }
            opt1Btn.GetComponentInChildren<TextMeshProUGUI>().text = str1;
            string str2 = "";
            foreach (LvlData stat in allPendingOptions[index].allStatDataOption2)
            {
                str2 += stat.val + " " + stat.attribName;
            }
            opt2Btn.GetComponentInChildren<TextMeshProUGUI>().text = str2;
        }

        void OnOptBtn1Pressed()
        {
            currLvl = allPendingOptions[index].level;
            LevelService.Instance.lvlModel.RemoveOptions2PendingStack(charName, currLvl, 1);
            LevelService.Instance.lvlModel.AddOptions2ChosenStack(charName
                            , allPendingOptions[index].allStatDataOption1, currLvl);
            index++;
            FillOptionsPanel();
        }
        void OnOptBtn2Pressed()
        {
            currLvl = allPendingOptions[index].level;
            LevelService.Instance.lvlModel.RemoveOptions2PendingStack(charName, currLvl, 2);
            LevelService.Instance.lvlModel.AddOptions2ChosenStack(charName
                            , allPendingOptions[index].allStatDataOption2, currLvl);
            index++;
            FillOptionsPanel();
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            isPanelOpen = true;
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            isPanelOpen = false;
        }

        public void Init()
        {           
            UnLoad();
        }

    }
}