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

        bool isRevealing = false; 

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
            canvas = GameObject.FindGameObjectWithTag("TownCanvas");
        }

        public void InitWelcomeView()
        {   
            gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        void OnContinueBtnPressed()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            BarkService.Instance.seqBarkController.ShowSeqbark(SeqBarkNames.KhalidHouse); 
              
        }
        public void RevealWelcomeTxt(string str)
        {   
            if(!isRevealing) 
            {
                Sequence seqreveal = DOTween.Sequence();
                seqreveal
                         .AppendCallback(() => { isRevealing = true;  })   
                         .AppendCallback(() => { welcomeTxt.GetComponent<TextMeshProUGUI>().text = str; })
                         .AppendCallback(() => { welcomeTxt.gameObject.SetActive(true); })                         
                         ;
                seqreveal.Play(); 
                UnrevealWelcometxt();
            }
            else
            {
                StartCoroutine(WaitForSec(str)); 
            }
        }

        IEnumerator WaitForSec(string str)
        {
            yield return new WaitForSeconds(1.5f);
            RevealWelcomeTxt(str); 
        }

       void OnRevealComplete()
        {
            Debug.Log("Reveal completed ");
            isRevealing = false; 
            welcomeTxt.gameObject.SetActive(false);
        }

        public void UnrevealWelcometxt()
        {
            Sequence seq = DOTween.Sequence();
            seq
                .AppendInterval(3.5f) 
                .AppendCallback(() => { welcomeTxt.GetComponentInChildren<TextRevealer>().Unreveal(); })                              
                ;
            seq.Play().OnComplete(OnRevealComplete);
        }
    }
}