using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
using TMPro;
using System;


namespace Combat
{
    [CreateAssetMenu(fileName = "TransitionSO", menuName = "Combat/TransitionSO")]
    public class TransitionSO : ScriptableObject
    {
        [Header("Main Sprites")]
        [SerializeField] Sprite SOCDayMidSprite;
        [SerializeField] Sprite SOCNightMidSprite;
        [SerializeField] Sprite[] SOCDayTopSprites = new Sprite[3];
        [SerializeField] Sprite[] SOCNightTopSprites = new Sprite[3];


        [Header("Params")]
        [SerializeField] float mainAnimTime = 2f; 

         GameObject CenterPrefab;
        Transform centerTrans;
        Transform crossTxtTrans;
        Transform topTrans;
        Transform BottomTrans;

        [SerializeField] Vector2 orgPos; 
        float centerTransWidth;
        RectTransform rectCenter; 
        GameObject centerPanel; 

        void Awake()
        {
           
        }
        public void PlayAnims(string Centertxt, GameObject _centerPanel)
        {
            if(_centerPanel == null)
             {
                _centerPanel = FindObjectOfType<AnimPanelView>().gameObject;
            }

            TimeState _timeState = TimeState.Day; 
            if (_timeState == TimeState.Day)
            {               
             //   Debug.Log("position" + orgPos);
                centerPanel = _centerPanel;
                //centerPanel.SetActive(true); 
                centerTrans = centerPanel.transform.GetChild(0);
                crossTxtTrans = centerTrans.GetChild(1);

                orgPos = new Vector2(Screen.width/2,Screen.height/2);
                orgPos.x = orgPos.x - Screen.width/6;

                rectCenter = centerTrans.GetComponent<RectTransform>();
                centerTransWidth = rectCenter.sizeDelta.x;
                topTrans = centerPanel.transform.GetChild(1);
                BottomTrans = centerPanel.transform.GetChild(2);
                centerTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text = Centertxt;
                centerTrans.GetChild(1).GetComponent<TextMeshProUGUI>().text = Centertxt;
               
                // sprite flip anim to start...
               
                for (int i = 0; i < 3; i++)
                {
                    centerPanel.transform.GetChild(1).GetChild(i).GetComponent<Image>().sprite = SOCDayTopSprites[i];
                }

                Sequence mainSeq = DOTween.Sequence();
                //mainSeq.PrependCallback(() => centerPanel.SetActive(true));
                //mainSeq.PrependInterval(0.5f);
                mainSeq.AppendCallback(()=> PositionCrossTxt());
                mainSeq.Append(crossTxtTrans.GetComponent<TextMeshProUGUI>().DOFade(0.35f, 0.1f));
                mainSeq.AppendCallback(() => ToggleTrans(true));

                mainSeq.AppendCallback(() => MoveLeft2Right()); 
                mainSeq.AppendCallback(() => FadeImage(1f));
                mainSeq.AppendCallback(() => ScaleTopNBottomPanel(1f)); 
                mainSeq.AppendCallback(TopBottomFX);

                mainSeq.AppendInterval(mainAnimTime);

                mainSeq.AppendCallback(() => ScaleTopNBottomPanel(0f));               
                
                mainSeq.AppendCallback(() => FadeImage(0f));
                mainSeq.Append(crossTxtTrans.GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.65f));

                mainSeq.Play();
               // mainSeq.OnComplete(() => centerPanel.SetActive(false)); 
            }
        }
        void PositionCrossTxt()
        {
            crossTxtTrans.GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.001f);

          
            crossTxtTrans.transform.position = orgPos;
        }
        void MoveLeft2Right()
        {            
            crossTxtTrans.DOLocalMoveX(centerTransWidth*1/3 , 1.0f);         
          //  Debug.Log("WIDTH" + centerTransWidth); 
        }
        void ScaleTopNBottomPanel(float fadeVal)
        {
            topTrans.DOScale(fadeVal, 0.2f);
            BottomTrans.DOScale(fadeVal, 0.2f);

        }

        void ToggleTrans(bool status)
        {
            centerTrans.gameObject.SetActive(status);            
            topTrans.gameObject.SetActive(status);
            BottomTrans.gameObject.SetActive(status);

        }
     
        void FadeImage(float fadeVal)
        {
            Image[] images = centerPanel.transform.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.DOFade(fadeVal, 0.2f); 
            }
            centerTrans.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(1, 1, 1, fadeVal), 0.2f);
            centerPanel.transform.GetChild(0).GetChild(0).DOScale(fadeVal, 0.4f);
        }

        void TopBottomFX()
        {
            ToggleSprites(0); int k = 0; float interval = 0.3f;
            Sequence animSeq = DOTween.Sequence();
            animSeq.AppendCallback(() => ToggleSprites(k));
            animSeq.AppendInterval(interval);
            animSeq.AppendCallback(() => { if (k < 3) k++; else k = 0; });
            animSeq.SetLoops(-1);
                animSeq.Play();
           
        }

        void ToggleSprites(int j)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == j)
                {
                    topTrans.GetChild(i).GetComponent<Image>().DOFade(1f, 0.1f);
                    BottomTrans.GetChild(i).GetComponent<Image>().DOFade(1f, 0.1f);
                }
                else
                {
                    topTrans.GetChild(i).GetComponent<Image>().DOFade(0f, 0.1f);
                    BottomTrans.GetChild(i).GetComponent<Image>().DOFade(0f, 0.1f);
                }

            }

        }


    }
}


public enum TransitionName
{
    None,
    CombatDay, 
    CombatNight,

}

