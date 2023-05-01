using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

namespace Common
{
    public class GuideViewController : MonoBehaviour
    {
        GuideMarkView[] getAllMarks;
        private void Start()
        {
            getAllMarks = transform.GetComponentsInChildren<GuideMarkView>();
            InitGuideView();
        }

        public void InitGuideView()
        {
            foreach (GuideMarkView mark in getAllMarks)
            {
                mark.InitGuideView(this);
            }
            DisableAllGuideMarks();
        }

        public void ShowGuideMark(GuideMarkLoc guideMarkLoc)
        {
            DisableAllGuideMarks();
            GetGuideMark(guideMarkLoc).Load();
        }

        GuideMarkView GetGuideMark(GuideMarkLoc guideMarkLoc)
        {
            foreach (GuideMarkView mark in getAllMarks)
            {
                if (mark.guideMarkLoc == guideMarkLoc)
                    return mark; 
            }
            Debug.Log("Mark not found" + guideMarkLoc);
            return null;

        }

        public void ShowGuideMarkInSeq(GuideMarkLoc guideMarkLoc1, GuideMarkLoc guideMarkLoc2)
        {
          
            ShowGuideMark(guideMarkLoc1);
            GuideMarkView mark = GetGuideMark(guideMarkLoc1);
            StartCoroutine(Wait(mark, guideMarkLoc2)); 
        }

        IEnumerator Wait(GuideMarkView mark, GuideMarkLoc guideMarkLoc2)
        {
            yield return new WaitUntil(() => mark.isHovered);
            ShowGuideMark(guideMarkLoc2);
        }


        public void DisableAllGuideMarks()
        {
            foreach (var mark in getAllMarks)
            {
                mark.UnLoad();
            }
        }


    }
}