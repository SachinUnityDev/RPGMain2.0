using Common;
using Interactables;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
//    value attribiute - attribiute name - "from" - Cause Type - cast Time
//example:
//+1 Morale from Skills, 2 rds
//Dont record attribute changes with Infinity cast time or permanent change(changeAttribute)
//and you will update Cast time each round.
//so next round it will be : +1 Morale from Skills,  1 rd

//your buff controller will be like this:
//-3 Morale from Potions, 6 days
//-3 Vigor from Potions, 6 days
//but u dont write the first change  (permanent change)

    public class BuffView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float MAX_HT = 500f;
        [SerializeField] float MIN_HT = 100f;

        //[SerializeField] Transform container;
        [SerializeField] List<BuffData> allBuffData = new List<BuffData>();
        [SerializeField] List<string> allBuffStrs = new List<string>();

        BuffBtnView buffbtnView; 
        [SerializeField] bool isBuffView = false; 

        [Header(" log prefab")]
        [SerializeField] GameObject logPanelPrefab;
        [Header(" Buff log GO")]
        [SerializeField] GameObject logPanelGO;
        [SerializeField] string strPrev = "";

        
        public void InitBuffView(BuffBtnView buffBtnView, CharController charController, bool isBuffView)
        {
            this.buffbtnView = buffBtnView;
            OnCharClicked(charController);
            this.isBuffView= isBuffView;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           gameObject.SetActive(true);
            PrintBuffList();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }


        void OnCharClicked(CharController charController)
        {
            BuffController buffController = charController.buffController; 
            allBuffData.Clear();
            allBuffData = buffController.GetBuffDebuffData();

            string charNameStr = charController.charModel.charNameStr; 
            foreach (BuffData buffData in allBuffData)
            {
                //+1 Morale from Skills, 2 rds

                if (buffData.attribModData.chgVal == 0) return;

                string sign = buffData.attribModData.chgVal > 0 ? "+" : "-";
                string str2 = buffData.attribModData.chgVal > 0 ? "gains" : "suffers";

                string str = sign + Mathf.Abs(buffData.attribModData.chgVal) + " " + buffData.attribModData.attribModified.ToString() + " " +
                        buffData.attribModData.causeType.ToString() + ", " + buffData.buffedNetTime.ToString() + " "
                        + GetTimeFrameStr(buffData.timeFrame); 
                          
                allBuffStrs.Add(str);
                
            }
            PrintBuffList();

        }

        void IncrSize()
        {
            int lines = allBuffStrs.Count;
            // get skill card height             
            RectTransform buffRect = transform.GetComponent<RectTransform>();            
            if (lines > 2)
            {
                // increase size 
                int incr = lines - 2;
               float incrVal = incr * 40f;
                
                buffRect.sizeDelta
                        = new Vector2(buffRect.sizeDelta.x, MIN_HT + incrVal);
            }
            else
            {
                // reduce to org size 
                     
                buffRect.sizeDelta
                        = new Vector2(buffRect.sizeDelta.x, MIN_HT);
            }
            int j = 0;
            foreach (Transform child in transform)
            {
                if (j < lines)
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<TextMeshProUGUI>().text
                                                   = allBuffStrs[j];
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
                j++;
            }
        }
  
        void PrintBuffList()
        {
            IncrSize();
            int k = allBuffStrs.Count; 
            if (transform.childCount < k)
            {
                for (int j = 0; j < k; j++)
                {
                    Vector3 pos = Vector3.zero;
                    logPanelGO = Instantiate(logPanelPrefab, pos, Quaternion.identity);
                    logPanelGO.transform.SetParent(transform);
                    logPanelGO.transform.localScale = Vector3.one;
                }
            }
            int i = 0;
            foreach (Transform child in transform)
            {
                if (i < allBuffStrs.Count)
                {
                    child.gameObject.SetActive(true);
                    if (strPrev != allBuffStrs[i])
                        child.GetComponentInChildren<TextMeshProUGUI>().text = allBuffStrs[i];
                  
                    strPrev = allBuffStrs[i];
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
                i++;
            }
        }

        string GetTimeFrameStr(TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.None:
                    return "";                    
                case TimeFrame.EndOfRound:
                    return "rds";                     
                case TimeFrame.EndOfCombat:
                    return "eoc";                     
                case TimeFrame.EndOfDay:
                    return "days";                     
                case TimeFrame.EndOfNight:
                    return "days"; 
                case TimeFrame.EndOfQuest:
                    return "eoq";
                case TimeFrame.Infinity:
                    return "";                     
            }
            return "";


        }


    }
}