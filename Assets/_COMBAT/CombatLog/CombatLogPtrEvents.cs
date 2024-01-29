using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Combat
{
    public class CombatLogPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        ScrollRect scrollRect; 
        Scrollbar scrollBar;
        RectTransform combatLogRect;
        Vector2 orgRectSize ;
        public List<string> combatLogTxt = new List<string>(); 
        void Start()
        {
            scrollRect = gameObject.GetComponent<ScrollRect>();
            scrollBar = gameObject.GetComponentInChildren<Scrollbar>();
            combatLogRect = GetComponent<RectTransform>();
            orgRectSize = combatLogRect.sizeDelta;
            CombatEventService.Instance.OnSOC += CombatStartInit;
            DisplayChg(false);
            combatLogRect.sizeDelta = orgRectSize;
        }
        void CombatStartInit()
        {
            // Setactive false all the txt panels

        }
  
        void DisplayChg(bool value)
        {
            scrollRect.enabled = value;
            scrollBar.gameObject.SetActive(value);
        }

        public void PopulateCombatLog(List<string> combatLogUpdated)
        {
            int maxCount=  combatLogUpdated.Count-1;
            combatLogTxt.Clear();
            if (maxCount <= 0) return;
            for (int i = maxCount; (i > maxCount-20 && i>0) ; i--)
            {               
                combatLogTxt.Add(combatLogUpdated[i]); 
            }
            
            Transform content=   gameObject.transform.GetChild(1);
            int k = 0; 
            for( k =0; k < content.childCount && k < combatLogTxt.Count; k++)
            {
                content.GetChild(k).gameObject.SetActive(true);
                content.GetChild(k).GetComponent<TextMeshProUGUI>().text = combatLogTxt[k];     

            }
            for(int j = k; j < content.childCount; j++)
            {
                content.GetChild(j).gameObject.SetActive(false);  
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DisplayChg(true);
            combatLogRect.DOSizeDelta(orgRectSize + new Vector2(0f, 100f), 0.4f, false); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisplayChg(false);
            combatLogRect.DOSizeDelta(orgRectSize, 0.4f, false);
        }
      
    }


}

