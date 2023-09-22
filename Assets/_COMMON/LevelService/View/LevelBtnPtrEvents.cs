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
                    if(levelManSelectView.allPendingOptions.Count > 0)
                        lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.lvlPlusSprite; 
                    else
                        lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.spriteN;
                    break;
                case LvlDspyType.ManSelect:
                    spritePlus =
                        LevelService.Instance.lvlUpCompSO.lvlMinusSprite;
                    break;
                case LvlDspyType.ManDsply:
                    lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.spriteN;
                    break;
                case LvlDspyType.AutoDsply:
                    lvlBtnImg.sprite = LevelService.Instance.lvlUpCompSO.spriteN;
                    break;
                default:
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (levelManSelectView.allPendingOptions.Count > 0)
            {
                if(lvlDspyType != LvlDspyType.ManDsply)
                    levelView.On_LvlDsplyChg(LvlDspyType.ManDsply);
                else
                    levelView.On_LvlDsplyChg(LvlDspyType.None); // lvl Dsply already Open
            }
            else
            {
                // show auto dsply
                if (lvlDspyType != LvlDspyType.AutoDsply)
                    levelView.On_LvlDsplyChg(LvlDspyType.AutoDsply);
                else
                    levelView.On_LvlDsplyChg(LvlDspyType.None); // Auto Dsply already Open                
            }
        }


    }
}