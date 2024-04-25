using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Common
{
    public class LevelAutoDsplyView : MonoBehaviour
    {
        [SerializeField] LevelModel lvlModel;
        [SerializeField] LevelView levelView; 

        CharModel charModel; 
        public void Init(LevelView levelView)
        {
            this.levelView = levelView;
            InvService.Instance.OnCharSelectInvPanel -= OnCharSelectInInv;
            InvService.Instance.OnCharSelectInvPanel += OnCharSelectInInv;
            levelView.OnLevelDsplyChg -= ToggleDsply;
            levelView.OnLevelDsplyChg += ToggleDsply; 
        }

        void ToggleDsply(LvlDspyType lvlDspyType)
        {
            if (lvlDspyType == LvlDspyType.SelectPanel)
            {
                CharModel charModel = InvService.Instance.charSelectController.charModel; 
                OnCharSelectInInv(charModel);
                gameObject.SetActive(true);
            }                
            else
                gameObject.SetActive(false);
        }

        void OnCharSelectInInv(CharModel charModel)
        {
            this.charModel = charModel;
            FillLvlData();
        }

        void FillLvlData()
        {
            lvlModel = LevelService.Instance.lvlModel;

            LvlStackData lvlStackData = lvlModel.GetLvlStackData(charModel.charName);
            if(lvlStackData == null )
            {
                // empty up cards...       
                return; 
            }
            
            int j = 0;
            int i = 0; 
            for (i = 0; i < lvlStackData.allAutoDataAdded.Count; i++)
            {
                LvlData lvlData = lvlStackData.allAutoDataAdded[i]; 
                if (lvlData.val != 0)
                {
                    transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                   string sign = (lvlData.val < 0) ? "-" : "+";
                    string str = sign + $"{lvlData.val} {lvlData.attribName}";
                    transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                                    = str;
                    j++; 
                }
            }
            for (j = i; j < transform.GetChild(0).childCount; j++)
            {
                transform.GetChild(0).GetChild(j).gameObject.SetActive(false);
            }
        }


    }
}