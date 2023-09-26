using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Quest;
using Combat;
using UnityEngine.UIElements;

namespace Common
{
    public class TrapView : MonoBehaviour
    {

        [SerializeField] const float TIME_BET_HITS = 0.6f;
        [SerializeField] const float TIME_BET_LETTER_CHG = 0.45f;
        [SerializeField] const float TIME_DELTA = 0.4f; // to prevent double hits

        [Header("TBR")]
      
        [SerializeField] Transform keyContainer;
        [SerializeField] MistakeBarView mistakeBarView;
        [SerializeField] TextMeshProUGUI timerTxt;
        [SerializeField] TextMeshProUGUI resultTxt; 


        Sequence seq;
        [Header(" trap Model")]
        [SerializeField]TrapMGModel trapMGModel;
        [SerializeField] AllTrapMGSO allTrapMGSO;
        [SerializeField] TrapMGController trapMGController; 


        [Header(" Letter Pressed In Number")]
      
        [SerializeField] int currHL = -1;
        [SerializeField] int wrongHit = 0;
        [SerializeField] int correctHits = 0;
        [SerializeField] float startTime = 0;

        [Header(" Chk double hit")]
        [SerializeField]float prevHitTime;
        [SerializeField] bool onKeyRegWaitTime = false;

        [Header("Trap Anim")]
        public TransitionSO transitionSO;
        [SerializeField] GameObject animPanel;

        [Header("Timer")]
        [SerializeField] float prevTime;
        float timerVal;

        private void OnEnable()
        {
            wrongHit = 0;
            correctHits = 0;
            timerVal = 0;
            startTime = Time.time;
            prevTime = 0;
        }


        public void StartSeq(TrapMGModel trapMGModel, AllTrapMGSO allTrapSO, TrapMGController trapMGController)
        {
            this.trapMGModel = trapMGModel; 
            this.allTrapMGSO= allTrapSO;    
            this.trapMGController= trapMGController;

            foreach (Transform child in keyContainer)
            {
                child.GetComponent<TrapBtnPtrEvents>().InitTiles(this, allTrapMGSO); 
            }

            SetElementsInActive();  
           
            mistakeBarView.FillMistakeHearts(trapMGModel.mistakesAllowed, 0);

            Sequence startSeq = DOTween.Sequence();
            startSeq
                .AppendCallback(() => transitionSO.PlayAnims("TRAP!", animPanel))
                .AppendInterval(2f)
                .AppendCallback(SetElementsActive)
                ;

            startSeq.Play().OnComplete(GameSeq); 
            
        }

        void SetElementsActive()
        {
            keyContainer.gameObject.SetActive(true);
            mistakeBarView.gameObject.SetActive(true);
            timerTxt.gameObject.SetActive(true);
            resultTxt.gameObject.SetActive(false);
            resultTxt.DOFade(0f, 0.1f);
        }

        void SetElementsInActive()
        {
            keyContainer.gameObject.SetActive(false);
            mistakeBarView.gameObject.SetActive(false);
            timerTxt.gameObject.SetActive(false);
            resultTxt.gameObject.SetActive(true);
            resultTxt.DOFade(0f, 0.1f);
        }
        void GameSeq()
        {
            FillTimer();
            seq = DOTween.Sequence();
            seq
                .AppendCallback(HLTile)
                .AppendInterval(TIME_BET_LETTER_CHG)                
                ;
            seq.Play().SetLoops(100);
        }
        void FillTimer()
        {
            timerVal = Time.time - startTime;
            if (timerVal < trapMGModel.netTime)
                timerTxt.text = ((int)(trapMGModel.netTime - timerVal)).ToString();
            else
            {
                Debug.Log("TIME OUT" + timerVal);
                EndGame(false); 
            }
        }
        void HLTile()
        {
            currHL = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if(i != currHL)
                    keyContainer.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowGreyTile();
                else
                    keyContainer.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowDefaultTile();
            }
        }

        void OnLetterPressed(int letter)
        {
            if (Time.time <= prevHitTime + TIME_DELTA)
                return;
            if (onKeyRegWaitTime)
                return; 
            prevHitTime = Time.time;    
            if(letter== currHL)            
                OnSuccess();
            else
                OnFailed();            
        }
        IEnumerator Wait()
        {
            onKeyRegWaitTime = true;
            yield return new WaitForSeconds(TIME_BET_HITS);
            onKeyRegWaitTime = false;          
            seq.Play();            
        }     

        public void OnSuccess()
        {
            seq.Pause();
            if(currHL != -1)
            keyContainer.GetChild(currHL).GetComponent<TrapBtnPtrEvents>().OnCorrectHit();
            correctHits++;
            if (correctHits >= trapMGModel.correctHitsNeeded)
            {
                EndGame(true);
                return;
            }
            Debug.Log("success hit");
            StartCoroutine(Wait());
        }
        public void OnFailed()
        {
            seq.Pause();
            if (currHL != -1)
                keyContainer.GetChild(currHL).GetComponent<TrapBtnPtrEvents>().OnWrongHit();
            wrongHit++;
            mistakeBarView.FillMistakeHearts(trapMGModel.mistakesAllowed, wrongHit); 
            if(wrongHit >= trapMGModel.mistakesAllowed)
            {                
                EndGame(false);
                return;
            }
            Debug.Log("Failed hit");
            StartCoroutine(Wait());
        }
        void EndGame(bool result)
        {
            MGService.Instance.trapMGController.trapGameState = MGGameState.End; 
            if (!result)
            {              
                resultTxt.text = "TRAPPED";             
            }
            else
            {
                resultTxt.text = "TRAP EVADED"; 
            }
            Sequence endGameSeq = DOTween.Sequence();
            endGameSeq
                .AppendInterval(0.5f)
                .AppendCallback(SetElementsInActive)
                .Append(resultTxt.DOFade(1.0f, 0.5f))
                .AppendInterval(1.0f)
                .AppendCallback(EndGameFinal);
            endGameSeq.Play();
        }

        void EndGameFinal()
        {
            StopAllCoroutines();
            seq.Pause();            
            gameObject.SetActive(false);
            trapMGController.OnEndGame();
        }
        public void Update()
        {
            if(MGService.Instance.trapMGController.trapGameState == MGGameState.Start)
            {   
                if (Input.GetKeyDown(KeyCode.W))
                {
                    OnLetterPressed(0);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    OnLetterPressed(1);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    OnLetterPressed(2);

                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    OnLetterPressed(3);
                }

                if((Time.time - prevTime) > 1f)
                {
                    FillTimer(); 
                }
            }
        }

    }
}