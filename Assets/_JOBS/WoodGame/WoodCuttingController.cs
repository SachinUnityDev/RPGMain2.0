//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;

//public class WoodCuttingController : MonoBehaviour
//{
//    [SerializeField] GameObject sliderObjPrefab;
//    [SerializeField] GameObject sliderObj;

//    [Header("Cross Script ref")]
//    [SerializeField] WoodGameView woodGameView;


//    [Header("SelectionBox")]
//    [SerializeField] GameObject ClickBox;
//    [SerializeField] GameObject minClickR;
//    [SerializeField] GameObject maxClickR;

//    [Header("BaseBox")]
//    [SerializeField] GameObject BaseBox;
//    [SerializeField] GameObject minBaseR;
//    [SerializeField] GameObject maxBaseR;

//    [Header("Game Params")]
//    [SerializeField] WoodGameData currWoodGameData;

//    [Header("Game Counters")]
//    public int hits = 0;
//    public int missHits = 0;
//    public int consecutiveHits = 0;

//    [Header("Time Counters")]
//    [SerializeField] float GameTime;

//    [Header("Local Variables")]
//    [SerializeField] bool movingRight;

//    [SerializeField] WoodAnimController woodAnimController;

//    void Start()
//    {
//        woodAnimController = GetComponent<WoodAnimController>();

//    }
//    public void StartGame()
//    {
//        hits = 0;
//        missHits = 0;
//        consecutiveHits = 0;
//        GameTime = 0;

//        woodGameView.TogglePanel(false, " ");
//        ControlGameState(WoodGameState.Running);

//        currWoodGameData = WoodGameService.Instance.currWoodGameData;
//        ChangeBarScale();
//        woodGameView.GameStartView(currWoodGameData);
//        SpawnSlider();
//    }
//    #region   GameMovements
//    void SpawnSlider()
//    {
//        if (sliderObj == null)
//            sliderObj = Instantiate(sliderObjPrefab, minBaseR.transform.position, Quaternion.identity);

//    }
//    public void SliderMovementStop()
//    {
//        if (sliderObj != null)
//        {
//            sliderObj.transform.DOPause();
//        }
//    }
//    void ClickBoxMovement()
//    {
//        float clickBoxWidth = Mathf.Abs(maxClickR.transform.position.x - minClickR.transform.position.x);


//        float availDist = Mathf.Abs(maxBaseR.transform.position.x - minBaseR.transform.position.x);
//        float randPos = Random.Range(clickBoxWidth / 2, availDist - clickBoxWidth / 2);

//        Debug.Log("POS added" + randPos + "click box width " + clickBoxWidth);
//        Vector3 finalPos = minBaseR.transform.position + new Vector3(randPos, 0f, 0f);
//        if (WoodGameService.Instance.gameState == WoodGameState.Running
//            || WoodGameService.Instance.gameState == WoodGameState.HitPaused)
//        {
//            ClickBox.transform.DOMove(finalPos, 0.05f);
//        }
//    }
//    #endregion
//    public void AddJobExp()
//    {
//        WoodGameService.Instance.currGameJobExp += Random.Range(currWoodGameData.maxJobExpR, currWoodGameData.minJobExpR);
//        woodGameView.PopulateJobExp(currWoodGameData);

//    }
//    void CheckNUpdateGameParams()
//    {
//        woodGameView.PopulateGameView(currWoodGameData, hits, missHits);

//        if (missHits >= currWoodGameData.totalMistakesAllowed)
//        {
//            ReloadGameSeq();
//        }
//        else if (hits >= currWoodGameData.totalCorrectHits)
//        {
//            Debug.Log("THIS GAME OVER ...move to the next one");
//            LoadTheNextGameSeq();
//        }
//        else
//        {
//            ClickBoxMovement();
//            ControlGameState(WoodGameState.Running);
//        }

//    }
//    public void LoadTheNextGameSeq()
//    {
//        WoodGameService.Instance.ChgGameParam2Next();
//        AddJobExp();
//        woodGameView.TogglePanel(true, "YOU WON THE GAME");

//        ControlGameState(WoodGameState.LoadPaused);

//    }
//    void ChangeBarScale()
//    {
//        ClickBox.transform.localScale = new Vector3(currWoodGameData.barScale, 1, 1);
//    }
//    public void ReloadGameSeq()
//    {
//        ControlGameState(WoodGameState.LoadPaused);
//        ChangeBarScale();

//        Sequence seq = DOTween.Sequence();
//        seq
//             .AppendInterval(1f)
//             .AppendCallback(() => woodGameView.TogglePanel(true, "GAME RELOAD!"));
//        seq.Play();
//    }
//    public void HitSeq(bool isSuccess)
//    {
//        ControlGameState(WoodGameState.HitPaused);

//        if (isSuccess)
//        {
//            Sequence HitSeq = DOTween.Sequence();
//            HitSeq.AppendCallback(() => woodGameView.InGamePopUp("SUCCESS"))
//                 .AppendInterval(0.4f)
//                 .AppendCallback(() => CheckNUpdateGameParams());
//            HitSeq.Play();
//        }
//        else
//        {
//            Sequence HitSeq = DOTween.Sequence();
//            HitSeq.AppendCallback(() => woodGameView.InGamePopUp("MIS HIT"))
//                 .AppendInterval(0.4f)
//                 .AppendCallback(CheckNUpdateGameParams)
//                ;
//            HitSeq.Play();
//        }
//    }
//    public void ExitGame()
//    {
//        woodGameView.TogglePanel(true, "YOU ARE DONE FOR THE DAY ! ");
//        sliderObj.transform.position = minBaseR.transform.position;
//        SliderMovementStop();
//        WoodGameService.Instance.ExitGame();
//    }
//    public void ResetOnHit()
//    {

//        sliderObj.transform.position = minBaseR.transform.position;

//        SliderMovement();

//        Do a brief pause
//         get the slider to minBaseR
//         Start the Motion again

//    }
//    void Update()
//    {
//        if (WoodGameService.Instance.gameState == WoodGameState.Running && sliderObj != null)
//        {
//            float speed = currWoodGameData.sliderSpeed * 10f;

//            if (Vector3.Distance(maxBaseR.transform.position, sliderObj.transform.position) > 0.1f
//                && movingRight)
//            {
//                sliderObj.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;

//            }
//            else if (Vector3.Distance(minBaseR.transform.position, sliderObj.transform.position) > 0.1f
//               && !movingRight)
//            {
//                sliderObj.transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
//            }

//            if (Vector3.Distance(maxBaseR.transform.position, sliderObj.transform.position) <= 0.1f)
//            {
//                movingRight = false;
//            }
//            if (Vector3.Distance(minBaseR.transform.position, sliderObj.transform.position) <= 0.1f)
//            {
//                movingRight = true;
//            }


//            if (Input.GetKeyDown(KeyCode.Space) && (WoodGameService.Instance.gameState == WoodGameState.Running))
//            {
//                if (sliderObj.transform.position.x < maxClickR.transform.position.x
//                    && sliderObj.transform.position.x > minClickR.transform.position.x)
//                {
//                    Debug.Log("SUCCESS");
//                    hits++;
//                    consecutiveHits++;
//                    HitSeq(true);
//                }
//                else
//                {
//                    Debug.Log("MISSED HIT ");
//                    missHits++;
//                    consecutiveHits = 0;
//                    HitSeq(false);
//                }

//                //Vector3 minBaseVector = minBaseR.transform.position;
//                //sliderObj.transform.position = new Vector3(minBaseVector.x, minBaseVector.y, minBaseVector.z);
//            }
//        }

//        if (currWoodGameData.isBarRelocationON)
//        {
//            if (WoodGameService.Instance.gameState == WoodGameState.Running)
//            {
//                if (GameTime + 3 <= Time.time)
//                {
//                    ClickBoxMovement();
//                    GameTime = Time.time;
//                }
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            ClickBoxMovement();
//        }

//    }



//}

//s
//void LoadTheNextSeq()
//{
//    // ADD TOGGLE PLANE HERE

//    //  check whether 

//    //woodGameView.LoadGamePopUp("NEXT GAME SEQ ");
//    //WoodGameService.Instance.ControlGameState(WoodGameState.LoadPaused);
//    //WoodGameService.Instance.OnGameSeqComplete();

//    //  Sequence LoadNextSeq = DOTween.Sequence();
//    //LoadNextSeq.AppendCallback(() => woodGameView.LoadGamePopUp("NEXT GAME SEQ "))
//    //     .AppendInterval(1f)
//    //     .AppendCallback();
////    //LoadNextSeq.Play();
////}

//public void SliderMovement()
//{
//    //float speed = currWoodGameData.sliderSpeed;

//    //Sequence seq = DOTween.Sequence();
//    //seq.Append(sliderObj.transform.DOMove(maxBaseR.transform.position, 1 / speed))
//    //    .Append(sliderObj.transform.DOMove(minBaseR.transform.position, 1 / speed));


//    //if (WoodGameService.Instance.gameState == WoodGameState.Running)
//    //{
//    //    sliderObj.transform.position = minBaseR.transform.position;
//    //    seq.Play().SetLoops(100);
//    //}

//}
//public class WoodCuttingController : MonoBehaviour
//{

//    public GameObject sliderObjPrefab;
//    public GameObject sliderObj;

//    public GameObject selectionBox;

//    public GameObject BaseBox;

//    public GameObject minR;
//    public GameObject maxR;

//    float xMax = 0;
//    float xMin = 0;

//    float sliderDuration = 1f;
//    void Start()
//    {
//        SpawnSlider();
//    }




//    void GetXRange()
//    {

//        //Sprite sprite = BaseBox.GetComponent<SpriteRenderer>().sprite;

//        // Vector2[] vertices = sprite.vertices;
//        //RectTransform boxRT = BaseBox.GetComponent<RectTransform>();
//        // Vector3[] v = new Vector3[4];
//        //boxRT.GetLocalCorners(v);
//        //float prevX = 0;

//        //   float scaleBaseBox = BaseBox.transform.localScale.x;
//        //   for (var i = 0; i < 4; i++)
//        //   {


//        //      Debug.Log("Pos " + vertices[i].x);
//        //     if (vertices[i].x > prevX)
//        //        {
//        //          xMax = vertices[i].x*scaleBaseBox;
//        //           xMin = prevX*scaleBaseBox;
//        //      }
//        //      prevX = vertices[i].x;                        
//        //    }
//        //     Debug.Log("XMax " + xMax + "xMin" + xMin);

//        //   sliderObj = Instantiate(sliderObjPrefab, vertices[1], Quaternion.identity);

//        //sliderObj.transform.SetParent(BaseBox.transform.parent);
//        // SliderMovement(true);
//    }

//    void SpawnSlider()
//    {

//        sliderObj = Instantiate(sliderObjPrefab, minR.transform.position, Quaternion.identity);
//        SliderMovement();
//    }

//    //void SliderMovement()
//    {

//        Sequence seq = DOTween.Sequence();
//        seq.Append(sliderObj.transform.DOMove(maxR.transform.position, 1))
//            .Append(sliderObj.transform.DOMove(minR.transform.position, 1));


//        seq.Play().SetLoops(100);

//    }



//    void CheckOnTarget()
//    {



//    }

//}