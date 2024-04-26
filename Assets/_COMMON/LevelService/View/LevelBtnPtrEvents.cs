using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Common
{


    public class LevelBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] Image lvlBtnImg;

        Sprite spritePlus;
        Sprite spriteMinus;
        Sprite spriteN;

        [Header("Global Var")]
         LevelView levelView; 
        LevelManSelectView levelManSelectView;  
        LevelAutoDsplyView  levelAutoDsplyView;

        [SerializeField] LvlDspyType lvlDspyType; 

        public void InitLvlBtn(LevelView levelView, LevelManSelectView levelManSelectView, LevelAutoDsplyView levelAutoDsply)
        {
            this.levelView= levelView;
            this.levelManSelectView = levelManSelectView;
            this.levelAutoDsplyView = levelAutoDsply;
            levelView.OnLevelDsplyChg -= OnDsplyChg;
            levelView.OnLevelDsplyChg += OnDsplyChg;
            FillImg(); 
        }

         void OnDsplyChg(LvlDspyType lvlDspyType)
        {
            this.lvlDspyType= lvlDspyType;
            FillImg(); 
        }

        void FillImg()
        {

            switch (lvlDspyType)
            {
                case LvlDspyType.None:
                    //if(levelManSelectView.allPendingOptions.Count > 0)
                    if (levelView.ChkIfManSelectPending())
                    {
                        lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.lvlPlusSprite;                       
                    }
                    else
                    {
                        lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.spriteN;                      
                    }                        
                    break;
                case LvlDspyType.ManSelect:
                    spritePlus =
                        LevelService.Instance.lvlUpCompSO.lvlMinusSprite;
                    break;
                case LvlDspyType.SelectPanel:
                    lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.spriteN;
                    break;             
                default:
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //if (levelManSelectView.allPendingOptions.Count > 0)
            if(levelView.ChkIfManSelectPending())
            {
                if(lvlDspyType != LvlDspyType.ManSelect)
                    levelView.On_LvlDsplyChg(LvlDspyType.ManSelect);
                else
                    levelView.On_LvlDsplyChg(LvlDspyType.None); // lvl Dsply already Open
            }
            else
            {
                // show auto dsply
                if (lvlDspyType != LvlDspyType.SelectPanel)
                    levelView.On_LvlDsplyChg(LvlDspyType.SelectPanel);                
                else
                    levelView.On_LvlDsplyChg(LvlDspyType.None); // Auto Dsply already Open                
            }
            levelView.invMainViewController.ToggleViewFwd(false); 
        }


    }
}