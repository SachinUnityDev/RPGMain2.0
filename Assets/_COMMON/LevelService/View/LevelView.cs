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
using System.Security.Policy;

namespace Common
{
    public enum LvlDspyType
    {
        None, 
        ManSelect, 
        ManDsply, 
        AutoDsply,
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

   

        public void LevelViewInit()
        {
            levelBtn.InitLvlBtn(this, lvlManSelectView, lvlAutoDsply);
            lvlManSelectView.LevelManSelectInit();
            lvlManDsply.Init();
            lvlAutoDsply.Init(); 
          
        }


        public void On_LvlDsplyChg(LvlDspyType lvlDsplytype)
        {
            OnLevelDsplyChg?.Invoke(lvlDsplytype);

        }
    }


}
