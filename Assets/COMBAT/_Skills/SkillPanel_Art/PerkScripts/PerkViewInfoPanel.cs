using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Interactables
{

    public class PerkViewInfoPanel : MonoBehaviour
    {
        [SerializeField] Transform headerTrans;
        [SerializeField] Transform descTrans;
        [SerializeField] TextMeshProUGUI perkTypeTxt;

        [Header("Info")]
        [SerializeField] PerkData perkData;
        PerkBase perkBase;       
        SkillController1 skillController;
        private void Awake()
        { 
            headerTrans = transform.GetChild(0); 
            descTrans = transform.GetChild(1);
            perkTypeTxt = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }
        public void Init(PerkData perkData, PerkBase perkBase, SkillController1 _skillController)
        {
            this.skillController = _skillController;
            this.perkData = perkData;
            this.perkBase = perkBase;
            Fillheader();
            FillMidDesc();
            FillBtmPerkType();

        }

        void Fillheader()
        {
            headerTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = perkData.perkName.ToString().CreateSpace();

            List<PerkNames> preReqName = perkBase.preReqList;
            List<PerkData> allPerkData = skillController.allSkillPerkData;

            string prereqStr = "";
            foreach (PerkNames perk in preReqName)
            {
                if (perk == PerkNames.None) break;
                int index = allPerkData.FindIndex(t => t.perkName == perk);
                if (index != -1)
                    prereqStr = prereqStr + allPerkData[index].perkType.ToString() + ", ";
            }
            string strFinal = ""; 
           if(prereqStr.Length > 0) 
             strFinal = prereqStr.Substring(0, prereqStr.Length - 2);

            headerTrans.GetChild(1).GetComponent<TextMeshProUGUI>().text
               = strFinal;
        }

        void FillMidDesc()
        {
            
        }
        void FillBtmPerkType()
        {
            // perkTypeTxt
            perkTypeTxt.text = perkData.perkType.ToString();

        }

    }
}