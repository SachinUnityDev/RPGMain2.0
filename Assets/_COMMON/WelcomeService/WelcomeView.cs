using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using DG.Tweening;
using TMPro;

namespace Town
{
    public class WelcomeView : MonoBehaviour
    {
        GameObject canvas;
        [SerializeField] Button continueBtn;
        [SerializeField] Transform welcomeTxt; 

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
            canvas = GameObject.FindGameObjectWithTag("TownCanvas"); 
        }

        public void InitWelcomeView()
        {
            gameObject.SetActive(true);
          //  transform.GetChild(0).gameObject.SetActive(true);
        }
        void OnContinueBtnPressed()
        {
            gameObject.SetActive(false);
           // transform.GetChild(0).gameObject.SetActive(false);
            BarkService.Instance.seqBarkController.ShowSeqbark(SeqBarkNames.KhalidHouse); 
              
        }
        public void RevealWelcomeTxt(string str)
        {
            welcomeTxt.GetComponent<TextMeshProUGUI>().text = str;           
            welcomeTxt.gameObject.SetActive(true);         
            UnRevealWelcomeTxt(); 
        }
        public void UnRevealWelcomeTxt()
        {
            Sequence seq = DOTween.Sequence();
            seq
                .AppendInterval(6f)
                .AppendCallback(()=> {welcomeTxt.GetComponentInChildren<TextRevealer>().Unreveal();})
                .AppendInterval(0.4f)
                .AppendCallback(() => welcomeTxt.gameObject.SetActive(false))
               ;
            seq.Play();
        }

 

    }
}