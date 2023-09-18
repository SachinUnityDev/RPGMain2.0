using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Quest;

namespace Common
{
    public class TrapView : MonoBehaviour
    {
        [Header("Result Txt")]
        [SerializeField] TextMeshProUGUI resultTxt; 
        Sequence seq;
        [Header(" trap Model")]
        [SerializeField]TrapMGModel trapMGModel;
        [SerializeField] AllTrapMGSO allTrapMGSO; 
        [Header(" Letter Pressed In Number")]
      
        [SerializeField] int currHL = -1;
        [SerializeField] int wrongHit = 0;
        [SerializeField] int correctHits = 0;
        [SerializeField] float startTime = 0;

        [Header(" Chk double hit")]
        [SerializeField]float prevHitTime;
        [SerializeField] float timeDelta = 0.6f;
        [SerializeField] bool onKeyRegWaitTime = false; 

        public void StartSeq(TrapMGModel trapMGModel, AllTrapMGSO allTrapSO)
        {
            this.trapMGModel = trapMGModel; 
            this.allTrapMGSO= allTrapSO;    

            foreach(Transform child in transform)
            {
                child.GetComponent<TrapBtnPtrEvents>().InitTiles(this, allTrapMGSO); 
            }

            resultTxt.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
            wrongHit = 0;
            startTime = Time.time;

            seq = DOTween.Sequence();
                seq
                .AppendCallback(HLTile)
                .AppendInterval(0.6f);

            seq.Play().SetLoops(100); 
        }



        void HLTile()
        {
            currHL = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if(i != currHL)
                    transform.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowGreyTile();
                else
                    transform.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowDefaultTile();
            }
        }

        void OnLetterPressed(int letter)
        {
            if (Time.time <= prevHitTime + timeDelta)
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
            yield return new WaitForSeconds(1f);
            onKeyRegWaitTime = false;
            if (Time.time >= (startTime + trapMGModel.netTime))
                EndGame(false);
            else
                seq.Play();            
        }     

        public void OnSuccess()
        {
            seq.Pause();
            if(currHL != -1)
            transform.GetChild(currHL).GetComponent<TrapBtnPtrEvents>().OnCorrectHit();
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
                transform.GetChild(currHL).GetComponent<TrapBtnPtrEvents>().OnWrongHit();
            wrongHit++;
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
            Debug.Log("End Game");
            if (result)
                resultTxt.text = "UNLOCKED"; 
            else
                resultTxt.text = "TRAPPED"; 
            StopAllCoroutines();
            seq.Pause();
            resultTxt.gameObject.SetActive(true);
            gameObject.SetActive(false);
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
               

            }
        }

    }
}