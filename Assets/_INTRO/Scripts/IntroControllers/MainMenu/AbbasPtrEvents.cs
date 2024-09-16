using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Intro
{
    public class AbbasPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject iconPanel;
        [SerializeField] Image img;

        [SerializeField] Button steamBtn;
        void Start()
        {
            img = gameObject.GetComponent<Image>();
            steamBtn.onClick.AddListener(OnSteamBtnPressed);
            iconPanel.transform.DOScale(0, 0.25f);

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowBtn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideBtn();
        }
   
        public void ShowBtn()
        {
            DOTween.KillAll();

            iconPanel.transform.DOScale(1, 0.25f);
            img.DOFade(1, 0.25f);
        }
        public void HideBtn()
        {
            DOTween.KillAll();

            Sequence waitSeq = DOTween.Sequence();
            waitSeq
                .AppendInterval(1f)
                .Append(img.DOFade(0.4f, 0.25f))
                .Append(iconPanel.transform.DOScale(0, 0.25f))
                ;
            waitSeq.Play();

        }

        void OnSteamBtnPressed()
        {

            Application.OpenURL("https://store.steampowered.com/app/1553870/Shargad_First_Blood/"); 

        }

    
    }




}

