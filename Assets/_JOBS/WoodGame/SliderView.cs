using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] GameObject pointer;

        [Header("Cross Script ref")]
        [SerializeField] WoodGameView1 woodGameView;

        [Header("SelectionBox")]
        [SerializeField] GameObject ClickBox;
        [SerializeField] GameObject minClickR;
        [SerializeField] GameObject maxClickR;

        [Header("BaseBox")]
        [SerializeField] GameObject BaseBox;
        [SerializeField] GameObject minBaseR;
        [SerializeField] GameObject maxBaseR;

        [Header("Game Params")]
        [SerializeField] WoodGameData woodGameData;

        [Header("Game Counters")]
        public int hits = 0;
        public int missHits = 0;
        public int consecutiveHits = 0;

        [Header("Time Counters")]
        [SerializeField] float GameTime;

        [Header("Local Variables")]
        [SerializeField] bool movingRight;
        [SerializeField] float timeDelay = 0.3f;
        [SerializeField] float prevHit; 
        void Start()
        {
         

        }
        public void Init(WoodGameView1 woodGameView, WoodGameData woodGameData)
        {
            this.woodGameView= woodGameView;
            this.woodGameData= woodGameData;
        }
        #region   GameMovements
        public void StartMovement()
        {
            hits = 0;
            missHits = 0;
            consecutiveHits = 0;
            GameTime = 0;    
            
            ChangeBarScale();        
            pointer.SetActive(true);
            woodGameView.gameState = WoodGameState.Running;
        }

        public void SliderMovementStop()
        {
            if (pointer != null)
            {
                pointer.transform.DOPause();
                pointer.transform.position = minBaseR.transform.position;
            }
        }
        public void ClickBoxMovement()
        {
            float clickBoxWidth = Mathf.Abs(maxClickR.transform.position.x - minClickR.transform.position.x);


            float availDist = Mathf.Abs(maxBaseR.transform.position.x - minBaseR.transform.position.x);
            float randPos = Random.Range(clickBoxWidth / 2, availDist - clickBoxWidth / 2);

            // Debug.Log("POS added" + randPos + "click box width " + clickBoxWidth); 
            Vector3 finalPos = minBaseR.transform.position + new Vector3(randPos, 0f, 0f);
            if (woodGameView.gameState == WoodGameState.Running
                || woodGameView.gameState == WoodGameState.HitPaused)
            {
                ClickBox.transform.DOMove(finalPos, 0.05f);
            }
        }
        #endregion
       
        public void ChangeBarScale()
        {
            ClickBox.transform.localScale = new Vector3(woodGameData.barScale, 1, 1);
        }

       public  void CheckNUpdateGameParams()
        {
            woodGameView.PopulateGameView(woodGameData, hits, missHits);

            if (missHits >= woodGameData.totalMistakesAllowed)
            {
                woodGameView.ReloadGameSeq();
            }
            else if (hits >= woodGameData.totalCorrectHits)
            {
                woodGameView.ShowSuccess(); 
            }
            else
            {
                ClickBoxMovement();
                woodGameView.ControlGameState(WoodGameState.Running);
            }
        }
        public void HitSeq(bool isSuccess)
        {
            woodGameView.ControlGameState(WoodGameState.HitPaused);

            if (isSuccess)
            {
                Sequence HitSeq = DOTween.Sequence();
                HitSeq
                     .AppendInterval(0.4f)
                     .AppendCallback(CheckNUpdateGameParams);
                HitSeq.Play();
            }
            else
            {
                Sequence HitSeq = DOTween.Sequence();
                HitSeq
                     .AppendInterval(0.4f)
                     .AppendCallback(CheckNUpdateGameParams)
                    ;
                HitSeq.Play();
            }
        }

        public void ResetOnHit()
        {
            pointer.transform.position = minBaseR.transform.position;
        }
        void Update()
        {
            if (woodGameView == null || woodGameData == null) return;
            if (woodGameView.gameState == WoodGameState.Running)
            {
                float speed = woodGameData.sliderSpeed * 400f;
                if(maxBaseR.transform.position.x > pointer.transform.position.x
                    && movingRight)
                {
                    pointer.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;

                }
                else if(minBaseR.transform.position.x < pointer.transform.position.x
                && !movingRight)
                {
                    pointer.transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                }
               if(maxBaseR.transform.position.x - pointer.transform.position.x <= 5f)
                {
                    movingRight = false;
                }
                if((pointer.transform.position.x- minBaseR.transform.position.x) <= 5f)
                {
                    movingRight = true;
                }

                if (Input.GetKeyDown(KeyCode.Space) && (Time.time -prevHit) > timeDelay  && (woodGameView.gameState == WoodGameState.Running))
                {
                    if (pointer.transform.position.x < maxClickR.transform.position.x
                        && pointer.transform.position.x > minClickR.transform.position.x)
                    {
                        Debug.Log("SUCCESS");
                        hits++;
                        consecutiveHits++;
                       HitSeq(true);
                    }
                    else
                    {
                        Debug.Log("MISSED HIT ");
                        missHits++;
                        consecutiveHits = 0;
                        HitSeq(false);
                    }
                    
                    prevHit = Time.time;
                }
            }

            if (woodGameData.isBarRelocationON)
            {
                if (woodGameView.gameState == WoodGameState.Running)
                {
                    if (GameTime + 3 <= Time.time)
                    {
                        ClickBoxMovement();
                        GameTime = Time.time;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ClickBoxMovement();
            }

        }

    
    }
}