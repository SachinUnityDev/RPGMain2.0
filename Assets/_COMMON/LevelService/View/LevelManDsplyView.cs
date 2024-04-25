using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Common
{
    public class LevelManDsplyView : MonoBehaviour
    {
        [SerializeField] LevelModel lvlModel;
        [SerializeField]LevelView levelView;
        CharModel charModel;

        [SerializeField] Transform container; 
        public void Init(LevelView levelView)
        {
            this.levelView = levelView;
            container = transform.GetChild(0);
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
            if (lvlStackData == null)
            {
                // empty up cards...       
                return;
            }

            int j = 0;
            int i = 0; 
            for (i = 0; i < lvlStackData.allOptionsChosen.Count; i++)
            {
                ManualOptData manOpts = lvlStackData.allOptionsChosen[i];
                for (int k = 0; k < manOpts.allOptions.Count; k++)
                {
                    LvlData lvldata = manOpts.allOptions[k];    
                    if (lvldata.val != 0)
                    {
                        container.GetChild(i).gameObject.SetActive(true);
                        string sign = (lvldata.val < 0) ? "-" : "+";
                        string str = sign + $"{lvldata.val} {lvldata.attribName}";
                        container.GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                                        = str;
                       // j++;
                    }
                }
            }
            for (j = i; j < container.childCount; j++)
            {
                container.GetChild(j).gameObject.SetActive(false);
            }
        }


    }
}