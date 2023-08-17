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


        [Header(" TBR")]
        [SerializeField] Button continueBtn;
        [SerializeField] Transform welcomeTxt;
        [SerializeField] TextMeshProUGUI headingTxt; 
        [SerializeField] TextMeshProUGUI welcomeDesc; 
        bool isRevealing = false; 

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
            canvas = GameObject.FindGameObjectWithTag("TownCanvas");
        }

        public void InitWelcomeView()
        {   
            gameObject.SetActive(true);
            // text desc
            if (WelcomeService.Instance.isWelcomeRun)
            {
                headingTxt.text = "Welcome"; 
                welcomeDesc.text = "At last, you arrived in the city of Nekkisari. After the long journey, fatigue surges through your body and you�re looking forward to finding a tavern and resting. But first, you need to find Khalid, the man your father told you to find in his dying words."; 
            }
            else
            {
                headingTxt.text = "End of Tutorial";
                welcomeDesc.text = "Now that the guided section of the game is complete, you are free to make your own choices. You can access the quest journal through the City Screen and select the order of quests you want to take on according to your preference. Press F1 if you need assistance on any specific screen or panel."; 
            }
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        void OnContinueBtnPressed()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            if (WelcomeService.Instance.isWelcomeRun)
            {
                BarkService.Instance.seqBarkController.ShowSeqbark(SeqBarkNames.KhalidHouse);
            }   
            else
            {
                gameObject.SetActive(false);                  
            }
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
                seqreveal.Play().OnComplete(UnrevealWelcometxt);
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
            welcomeTxt.GetComponent<TextMeshProUGUI>().text = ""; 
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