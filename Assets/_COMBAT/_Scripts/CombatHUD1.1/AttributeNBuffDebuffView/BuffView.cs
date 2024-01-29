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

        [SerializeField] float WIDTH_N = 350f; 

        [SerializeField] Transform container;
        [SerializeField] List<BuffData> allBuffData = new List<BuffData>();
        [SerializeField] List<string> allBuffStrs = new List<string>();

        BuffBtnView buffbtnView; 
        [SerializeField] bool isBuffView = false; 

        [Header(" log prefab")]
        [SerializeField] GameObject logPanelPrefab;
        [Header(" Buff log GO")]
        [SerializeField] GameObject logPanelGO;
        [SerializeField] string strPrev = "";

        
        public bool InitBuffView(BuffBtnView buffBtnView, CharController charController, bool isBuffView)
        {
          container = transform.GetChild(0);    
            this.buffbtnView = buffBtnView;
          
            this.isBuffView= isBuffView;
            bool hasBuff = OnCharClicked(charController);
            return hasBuff;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(allBuffStrs.Count > 0)
            {
                gameObject.SetActive(true);
                PrintBuffList();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }


        bool OnCharClicked(CharController charController)
        {
            BuffController buffController = charController.buffController; 
            allBuffData.Clear();
            allBuffData = buffController.GetBuffDebuffData();
            allBuffStrs.Clear();
            string charNameStr = charController.charModel.charNameStr; 
            foreach (BuffData buffData in allBuffData)
            {
                //+1 Morale from Skills, 2 rds
                if(buffData.isBuff != isBuffView) continue; 
                if (buffData.attribModData.chgVal == 0) continue;
                if(buffData.timeFrame == TimeFrame.Infinity) continue;                

                string sign = buffData.attribModData.chgVal > 0 ? "+" : "-";
                string str2 = buffData.attribModData.chgVal > 0 ? "gains" : "suffers";

                string str = sign + Mathf.Abs(buffData.attribModData.chgVal) + " " + buffData.attribModData.attribModified.ToString() + " " +
                        buffData.attribModData.causeType.ToString() + ", " + buffData.buffedNetTime.ToString() + " "
                        + GetTimeFrameStr(buffData.timeFrame); 
                          
                allBuffStrs.Add(str);
                
            }
            PrintBuffList();
            if(allBuffStrs.Count > 0)
            {
                return true; 
            }
            return false; 

        }

        void IncrHt()
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
            foreach (Transform child in container)
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
        void IncrWidth()
        {
            int maxlength = 0; 
            foreach (string str in allBuffStrs)
            {
                if(str.Length > 30)
                {
                    if(str.Length > maxlength)
                        maxlength = str.Length;                    
                }
            }
            RectTransform rect = transform.GetComponent<RectTransform>();
            if (maxlength == 0)
            {
                rect.sizeDelta = new Vector2(WIDTH_N, rect.sizeDelta.y);
            }
            else
            {
                float preferredWidth = (maxlength - 30) * 10.0f;
                rect.sizeDelta = new Vector2(preferredWidth, rect.sizeDelta.y);
            } 
        }
        void PrintBuffList()
        {
            IncrHt();
            IncrWidth();
            int k = allBuffStrs.Count; 
            if (container.childCount < k)
            {
                for (int j = 0; j < k; j++)
                {
                    Vector3 pos = Vector3.zero;
                    logPanelGO = Instantiate(logPanelPrefab, pos, Quaternion.identity);
                    logPanelGO.transform.SetParent(container);
                    logPanelGO.transform.localScale = Vector3.one;
                }
            }
            int i = 0;
            foreach (Transform child in container)
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