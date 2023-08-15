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

        CharModel charModel;
        public void Init()
        {
            InvService.Instance.OnCharSelectInvPanel -= OnCharSelectInInv;
            InvService.Instance.OnCharSelectInvPanel += OnCharSelectInInv;
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
                        transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                        string sign = (lvldata.val < 0) ? "-" : "+";
                        string str = sign + $"{lvldata.val} {lvldata.attribName}";
                        transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>().text
                                                                        = str;
                        j++;
                    }
                }
            }
            for (j = i; j < transform.GetChild(0).childCount; j++)
            {
                transform.GetChild(0).GetChild(j).gameObject.SetActive(false);
            }
        }


    }
}