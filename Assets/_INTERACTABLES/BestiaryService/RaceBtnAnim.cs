
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Common;
using System.Linq;
using UnityEngine.UI;
using System;

namespace Interactables
{
    public class RaceBtnAnim : MonoBehaviour
    {
        [SerializeField] List<RaceTypeHero> leftRaces = new List<RaceTypeHero>();
        [SerializeField] List<RaceTypeHero> rightRaces = new List<RaceTypeHero>();

        [SerializeField] List<GameObject> leftRaceGO = new List<GameObject>();
        [SerializeField] List<GameObject> rightRaceGO = new List<GameObject>();
        [SerializeField] GameObject leftPanelGO;
        [SerializeField] GameObject rightPanelGO;
        [SerializeField] GameObject centerGO;
        [SerializeField] GameObject raceBtnPrefab;

        [SerializeField] RaceTypeHero selectRaceHero;
        [SerializeField] RaceType selectRaceType;
        [SerializeField] CharModel selectCharModel;
        [Header("Global var")]
        int index = 0;
        CompanionViewController compViewController;
        public bool isSpread = false;
        [SerializeField] bool isRight;

        public Canvas canvas; 
        void Start()
        {

        }

        public void Init(CharModel charModel, CompanionViewController companionViewController)
        {
            selectRaceHero = charModel.raceTypeHero;
            selectCharModel = charModel;
            selectRaceType = charModel.raceType;
            selectRaceHero = charModel.raceTypeHero;
            this.compViewController = companionViewController; 
            SortRaces();
            PopulateRaces();
        }

        public void SetNewRaceModel(CharModel charModel)
        {
            selectCharModel = charModel;
        }
        public void RaceSelected(RaceType raceType, GameObject raceGO, bool isRight)
        {
            if (selectRaceType == raceType)
            {
                if (isSpread)
                {
                    PlayCloseAnim();
                    compViewController.OnRaceSelected(raceType);
                    isSpread = false;
                }
                else
                {
                    PlayOpenAnim();
                    isSpread = true;
                }
            }
            else
            {

                isSpread = false;
                Swap(raceType, isRight, raceGO);              
                compViewController.OnRaceSelected(raceType);
                PlayCloseAnim();
            }
        }

        void Swap( RaceType raceType, bool isRight, GameObject raceGO)
        {
            // sprite swap 
            RaceSpriteData raceData = CharService.Instance
                            .charComplimentarySO.GetRaceSpriteData(raceType);
            if (raceData == null)
                Debug.Log("RaceData NULL");
            centerGO.transform.GetChild(0).GetComponentInChildren<RaceBtnCompPtrEvents>().Init(raceData, this, isRight);

            RaceSpriteData raceData2 = CharService.Instance
                            .charComplimentarySO.GetRaceSpriteData(selectRaceType);
            if (raceData == null)
                Debug.Log("RaceData NULL");
            raceGO.transform.GetComponent<RaceBtnCompPtrEvents>().Init(raceData2,this, false);

            RaceTypeHero rth = raceType.GetRaceTypHFrmRaceType();
            // data update 
            if (isRight)
            {
                rightRaces.Remove(rth);
                rightRaces.Add(selectRaceHero);
            }
            else
            {
                leftRaces.Remove(rth);
                leftRaces.Add(selectRaceHero);
            }
            selectRaceHero = rth;
            selectRaceType = raceType; 

        }
        void SortRaces()
        {
            index = 0;
          
            int raceCount = Enum.GetNames(typeof(RaceTypeHero)).Length; 
            int midCount = (raceCount - 1) / 2;
            for (int i = 1; i < raceCount; i++)  // i =0 is none
            {
                if ((RaceTypeHero)i != selectRaceHero)
                {
                    if (i <= midCount)
                    {
                        leftRaces.Add((RaceTypeHero)i);
                    }
                    else
                    {
                        rightRaces.Add((RaceTypeHero)i);
                    }
                }
            }
        }

        void PopulateRaces()
        {
            //center panel 
            leftRaceGO.Clear(); rightRaceGO.Clear();

            if (leftRaces.Count >= 1)
            {
                foreach (RaceTypeHero raceTypeHero in leftRaces)
                {
                    GameObject raceBtnGO = Instantiate(raceBtnPrefab);

                    raceBtnGO.transform.SetParent(leftPanelGO.transform);
                    raceBtnGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    raceBtnGO.GetComponent<RectTransform>().localScale = Vector3.one;
                    // get sprite Racedata 
                    RaceType raceType = raceTypeHero.GetRaceTypeFrmRaceTypeHero();
                    RaceSpriteData raceData = CharService.Instance.charComplimentarySO.GetRaceSpriteData(raceType);
                    if (raceData == null)
                        Debug.Log("RaceData NULL");
                    raceBtnGO.GetComponent<RaceBtnCompPtrEvents>().Init(raceData, this, false);
                    leftRaceGO.Add(raceBtnGO);
                }
            }
            if (rightRaces.Count >= 1)
            {
                foreach (RaceTypeHero raceTypeHero in rightRaces)
                {
                    GameObject raceBtnGO = Instantiate(raceBtnPrefab);

                    raceBtnGO.transform.SetParent(rightPanelGO.transform);
                    raceBtnGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    raceBtnGO.GetComponent<RectTransform>().localScale = Vector3.one;

                    RaceType raceType = raceTypeHero.GetRaceTypeFrmRaceTypeHero();
                    RaceSpriteData raceData = CharService.Instance
                        .charComplimentarySO.GetRaceSpriteData(raceType);
                    if (raceData == null)
                        Debug.Log("RaceData NULL");
                    raceBtnGO.GetComponent<RaceBtnCompPtrEvents>().Init(raceData, this, true);
                  
                    rightRaceGO.Add(raceBtnGO);
                }
            }
            leftRaceGO.Reverse();
            RaceSpriteData raceDataCenter = CharService.Instance
                        .charComplimentarySO.GetRaceSpriteData(selectRaceType);
            centerGO.GetComponentInChildren<RaceBtnCompPtrEvents>().Init(raceDataCenter, this, true);
           
        }
 

        void PlayOpenAnim()
        {
            Sequence spreadSeq = DOTween.Sequence();
            float y = -50f;
          
                //rightPanelGO.GetComponent<RectTransform>().rect.y;           
            spreadSeq
                .Append(transform.DOLocalMoveY(y / canvas.scaleFactor, 0.01f))                
                .Append(rightPanelGO.transform.parent.DOLocalMoveY(y / canvas.scaleFactor, 0.01f))
                .Append(leftPanelGO.transform.parent.DOLocalMoveY(y / canvas.scaleFactor, 0.01f))                
                .AppendCallback(()=>BtnSpread())
                ;

            spreadSeq.Play();
        }

        void BtnSpread()
        {
            float wide = 100f;
            Vector3 punch = new Vector3(0, -25f, 0);
            transform.DOPunchPosition(punch, 0.5f, 4);
            for (int i = 0; i < leftRaceGO.Count; i++)
            {
                float moveto = (wide ) * (i + 1);             
                leftRaceGO[i].transform.DOLocalMoveX(-moveto, 0.15f);             
            }
            for (int i = 0; i < rightRaceGO.Count; i++)
            {
                float moveto = (wide ) * (i + 1);              
                rightRaceGO[i].transform.DOLocalMoveX(moveto, 0.15f);
            }
        }


        void PlayCloseAnim()
        {
            Sequence closeSeq = DOTween.Sequence();
            float y = -50f;
            closeSeq                              
                .AppendCallback(()=>CloseSpread())
                .AppendInterval(0.4f)
                .AppendCallback(()=>MoveUp())
                ;

            closeSeq.Play();

        }

        void MoveUp()
        {
            transform.DOLocalMoveY(0, 0.05f);
            rightPanelGO.transform.parent.DOLocalMoveY(0, 0.01f);
            leftPanelGO.transform.parent.DOLocalMoveY(0, 0.01f);
        }
        void CloseSpread()
        {
            for (int i = 0; i < leftRaceGO.Count; i++)
            {
                leftRaceGO[i].transform.DOLocalMoveX(0, 0.1f);
            }
            for (int i = 0; i < rightRaceGO.Count; i++)
            {
                rightRaceGO[i].transform.DOLocalMoveX(0, 0.1f);
            }
        }

    }

}
