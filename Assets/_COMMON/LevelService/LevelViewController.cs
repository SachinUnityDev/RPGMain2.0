using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common;
using System.Linq;
using UnityEngine.UI;
using Interactables;
namespace Common
{
    public class LevelViewController : MonoBehaviour, IPanel
    {
       [SerializeField] Button lvlUpBtn; 
       [SerializeField] GameObject lvlUpPanel;

        [SerializeField] Button opt1Btn;
        [SerializeField] Button opt2Btn; 


        [SerializeField] BtmCharViewController btmCharViewController; 

        [Header("Global Variables")]
        [SerializeField] int index = 0; 
        [SerializeField] List<LvlDataComp> allPendingOptions = new List<LvlDataComp>();


        [Header("Lvl Button Related")]
        Sprite spritePlus;
        Sprite spriteMinus;
        Image img; 
        bool isPanelOpen = false;
        [SerializeField] bool isButtonActive = true;
        [SerializeField] int pendingCount = 0;

        [Header("CURR CHAR")]
        [SerializeField] CharNames charName;
        [SerializeField] Levels currLvl = Levels.Level0;
        [SerializeField] CharModel charModel; 

        void Start()
        {
            spriteMinus =
           LevelService.Instance.lvlUpCompSO.lvlMinusSprite;
            spritePlus =
                LevelService.Instance.lvlUpCompSO.lvlPlusSprite;


            btmCharViewController = 
            transform.parent.GetChild(2).GetComponent<BtmCharViewController>(); 
           
            InvService.Instance.OnCharSelectInvPanel += PopulateOptionPendingList;

            lvlUpBtn.onClick.AddListener(OnLevelUpBtnPressed);
            opt1Btn.onClick.AddListener(OnOptBtn1Pressed);
            opt2Btn.onClick.AddListener(OnOptBtn2Pressed);
            UnLoad();
        }

        void PopulateOptionPendingList(CharModel charModel)
        {
            if (charModel == null) return;
            charName = charModel.charName;
            this.charModel = charModel;
            LvlStackData lvlStackData = 
                    LevelService.Instance.lvlModel.GetLvlStackData(charModel.charName);
            if(lvlStackData == null) return;

            if (lvlStackData.allOptionsPending.Count <= 0)
            {
                Debug.Log("No options are available");
                isButtonActive = false;
                img.sprite = spriteMinus; 
                UnLoad();
                return;
            }
            else
            {
                isButtonActive = true;
                img.sprite = spritePlus;
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
            PopulateOptionsPanel(); 
        }

        void PopulateOptionsPanel()
        {    
            if (index >= allPendingOptions.Count)
            {
                img.sprite = spriteMinus;
                isButtonActive = false;
                UnLoad();
                return;
            }
            string str1 = "";
            foreach (AttribData stat in allPendingOptions[index].allStatDataOption1)
            {

                if(stat.AttribName == AttribName.dmgMin)
                {
                    AttribData statMax = allPendingOptions[index]
                        .allStatDataOption1.Find(t => t.AttribName == AttribName.dmgMax); 
                    str1 = stat.currValue +"-"+statMax.currValue;

                }
                else if (stat.AttribName == AttribName.armorMin)
                {
                    AttribData statMax = allPendingOptions[index]
                                    .allStatDataOption1.Find(t => t.AttribName == AttribName.armorMax);
                    str1 = stat.currValue + "-" + statMax.currValue;
                }
                else if(!stat.AttribName.IsAttribDamage() || !stat.AttribName.IsAttribArmor())
                {
                    str1 += stat.currValue + " "+ stat.AttribName; 
                }
                
            }
            opt1Btn.GetComponentInChildren<TextMeshProUGUI>().text = str1;
            string str2 = "";
            foreach (AttribData stat in allPendingOptions[index].allStatDataOption2)
            {

                if (stat.AttribName == AttribName.dmgMin)
                {
                    AttribData statMax = allPendingOptions[index]
                        .allStatDataOption1.Find(t => t.AttribName == AttribName.dmgMax);
                    str1 = stat.currValue + "-" + statMax.currValue;
                }
                else if (stat.AttribName == AttribName.armorMin)
                {
                    AttribData statMax = allPendingOptions[index]
                                    .allStatDataOption1.Find(t => t.AttribName == AttribName.armorMax);
                    str1 = stat.currValue + "-" + statMax.currValue;
                }
                else
                {
                    str2 += stat.currValue + " " + stat.AttribName;
                }                
            }
            opt2Btn.GetComponentInChildren<TextMeshProUGUI>().text = str2;
        }

        void OnOptBtn1Pressed()
        {
           
            currLvl = allPendingOptions[index].level;
            LevelService.Instance.lvlModel.RemoveOptions2PendingStack(charName, currLvl);
            LevelService.Instance.lvlModel.AddOptions2ChosenStack(charName
                            , allPendingOptions[index].allStatDataOption1, currLvl);
            index++;
            PopulateOptionsPanel();
        
        }

       
        void OnOptBtn2Pressed()
        {
           
            currLvl = allPendingOptions[index].level;
            LevelService.Instance.lvlModel.RemoveOptions2PendingStack(charName, currLvl);
            LevelService.Instance.lvlModel.AddOptions2ChosenStack(charName
                            , allPendingOptions[index].allStatDataOption2, currLvl);
            index++;
            PopulateOptionsPanel();
        }
        void OnLevelUpBtnPressed()
        {
            if (!isButtonActive) return;
            if (!isPanelOpen)
            {
                img.sprite = spriteMinus;
                Load();
            }
            else
            {
                img.sprite = spritePlus;
                UnLoad();
            }
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(lvlUpPanel, true);
            isPanelOpen = true;            
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(lvlUpPanel, false);
            isPanelOpen = false;     
        }

        public void Init()
        {
       
            img = lvlUpBtn.GetComponent<Image>();
            img.sprite = spritePlus;
            UnLoad();

        }



    }


}
