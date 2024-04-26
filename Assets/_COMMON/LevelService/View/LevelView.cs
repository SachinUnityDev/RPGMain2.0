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
using System;

namespace Common
{
    public enum LvlDspyType
    {
        None, 
        ManSelect, 
        SelectPanel,         
    }

    public class LevelView : MonoBehaviour
    {
        public Action<LvlDspyType> OnLevelDsplyChg;

        [Header("TBR")]
        public LevelBtnPtrEvents levelBtn;
        public LevelManSelectView lvlManSelectView;
        public LevelManDsplyView lvlManDsply;
        public LevelAutoDsplyView lvlAutoDsply;

        
        [SerializeField] LvlDspyType lvlDspyType;
        public InvMainViewController invMainViewController; 
   

        public void LevelViewInit(InvMainViewController invMainViewController)
        {
            this.invMainViewController = invMainViewController;
            levelBtn.InitLvlBtn(this, lvlManSelectView, lvlAutoDsply);
            lvlManSelectView.LevelManSelectInit(this);
            lvlManDsply.Init(this);
            lvlAutoDsply.Init(this);          
        }       
  
        public bool ChkIfManSelectPending()
        {
            CharModel charModel = InvService.Instance.charSelectController.charModel;

            LvlStackData lvlStackData =
                    LevelService.Instance.lvlModel.GetLvlStackData(charModel.charName);
            if (lvlStackData == null) return false;
            if (lvlStackData.allOptionsPending.Count == 0)            
                return false; 
            
            foreach (LvlDataComp lvlDataComp in lvlStackData.allOptionsPending)
            {
                if (lvlDataComp.allStatDataOption1.Count > 0
                    || lvlDataComp.allStatDataOption2.Count > 0)
                {
                    return true; 
                }
            }
            return false;
        }
        public void On_LvlDsplyChg(LvlDspyType lvlDsplytype)
        {
            OnLevelDsplyChg?.Invoke(lvlDsplytype);

        }
    }


}
