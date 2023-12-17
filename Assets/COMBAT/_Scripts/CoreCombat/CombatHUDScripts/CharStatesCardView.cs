using Common;
using DG.Tweening;
using Spine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{

    public class CharStatesCardView : MonoBehaviour
    {
        [SerializeField] const float STATES_CARD_HT = 165;
        [SerializeField] const int STANDARD_NO_OF_LINES = 4;

        CharStateSO1 charStateSO;
        CharStatesBase charStateBase;
        [SerializeField] Transform txtContainer;

       
        [SerializeField] List<string> lines = new List<string>();
        [SerializeField] int lvl;

        [SerializeField] Vector3 initPos; 

        private void Start()
        {
            //   gameObject.SetActive(false);
            initPos = transform.localPosition; 
        }
        public void InitStatesCardView(CharStateSO1 charStatesSO, CharStatesBase charStateBase, int lvl)
        {
            this.charStateSO = charStatesSO;
            this.charStateBase = charStateBase;
            lines = charStateBase.allStateFxStrs; 
            this.lvl= lvl;
            FillCard();
        }

        void ChgCardSizeNFillStr()
        {
            // get number of lines
            int count = lines.Count;
            // get skill card height             
            RectTransform statesCardRect = transform.GetComponent<RectTransform>();
            RectTransform containerRect = transform.GetComponent<RectTransform>();
            if (lines.Count > STANDARD_NO_OF_LINES)
            {  // increase size 
                int incr = lines.Count - STANDARD_NO_OF_LINES;
                statesCardRect.sizeDelta
                        = new Vector2(statesCardRect.sizeDelta.x, STATES_CARD_HT + incr * 20f);
                containerRect.sizeDelta
                       = new Vector2(containerRect.sizeDelta.x, STATES_CARD_HT + incr * 20f);


            }
            else
            {
                // reduce to org size               
                statesCardRect.sizeDelta
                        = new Vector2(statesCardRect.sizeDelta.x, STATES_CARD_HT);
            }

            // Fill card
       

        }
        public void FillCard() // Max 5 lines of strings 
        {

            transform.DOLocalMoveY(initPos.y + lvl * 60, 0.1f); 

            transform.GetChild(0).GetComponent<Image>().sprite
                                            = charStateSO.iconSprite;

            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                       = charStateSO.CharStateNameStr;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                        = charStateBase.castTime.ToString();
          
            int j = 0;
            foreach (Transform child in txtContainer)
            {
                if (j < lines.Count)
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<TextMeshProUGUI>().text = lines[j];
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
                j++;
            }
        }
    }
}