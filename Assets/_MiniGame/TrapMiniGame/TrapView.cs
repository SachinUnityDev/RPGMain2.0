using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Common
{
    public class TrapView : MonoBehaviour
    {
        [Header("Result Txt")]
        [SerializeField] TextMeshProUGUI resultTxt; 

        [SerializeField]int currHL =-1;
        [SerializeField] int failedHit = 0;
        [SerializeField] int successHit = 0; 
        [SerializeField] float startTime = 0; 

        Sequence seq;
        private void Start()
        {
        }
        public void StartSeq()
        {
            resultTxt.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
            failedHit = 0;
            startTime = Time.time;
            seq = DOTween.Sequence();
            seq.AppendCallback(HLTile);
            seq.AppendInterval(0.2f);

            seq.Play().SetLoops(100); 
        }



        void HLTile()
        {
            currHL = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if(i != currHL)
                    transform.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowNLTile();
                else
                    transform.GetChild(i).GetComponent<TrapBtnPtrEvents>().ShowHLTile();
            }
        }

        void OnLetterPressed(int letter)
        {
            if(letter== currHL)            
                OnSuccess();
            else
                OnFailed();            
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            if (Time.time >= (startTime + 16f))
                EndGame(false);
            else
                seq.Play();            
        }     

        public void OnSuccess()
        {
            seq.Pause();
            successHit++;
            if (successHit >= 2)
            {
                EndGame(false);
                return;
            }
            Debug.Log("success hit");
            StartCoroutine(Wait());
        }
        public void OnFailed()
        {
            seq.Pause();
            failedHit++;
            if(failedHit >= 3)
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