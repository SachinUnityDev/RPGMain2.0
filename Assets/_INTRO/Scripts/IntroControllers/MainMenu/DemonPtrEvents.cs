using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace Intro
{
    public class DemonPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject iconPanel;
        [SerializeField] Image img;

        [SerializeField] Button twitterBtn;
        [SerializeField] Button youtubeBtn;
        [SerializeField] Button discordBtn;
        void Start()
        {
           // img = gameObject.GetComponent<Image>();
            iconPanel.transform.DOScale(0, 0.25f);
           
            twitterBtn.onClick.AddListener(OnTwitterBtnPressed);
            youtubeBtn.onClick.AddListener(OnYoutubeBtnPressed);

            discordBtn.onClick.AddListener(OnDiscordBtnPressed);

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


        void OnTwitterBtnPressed()
        {
            Application.OpenURL("https://twitter.com/ShargadS");
        }
        void OnYoutubeBtnPressed()
        {
            Application.OpenURL("https://www.youtube.com/channel/UC5S1Kydzr8bcIby0G1AI5cw");
        }

        void OnDiscordBtnPressed()
        {
            Application.OpenURL("https://discord.com/invite/zFaHW4ExGY");
        }
    }



}

