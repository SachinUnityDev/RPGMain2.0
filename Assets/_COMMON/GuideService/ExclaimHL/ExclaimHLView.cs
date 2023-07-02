using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ExclaimHLView : MonoBehaviour
    {
        ExclaimMarkHLView[] getAllMarks;
        private void Start()
        {
            getAllMarks = transform.GetComponentsInChildren<ExclaimMarkHLView>();
            InitGuideView();
        }

        public void InitGuideView()
        {
            foreach (ExclaimMarkHLView mark in getAllMarks)
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

        ExclaimMarkHLView GetGuideMark(GuideMarkLoc guideMarkLoc)
        {
            foreach (ExclaimMarkHLView mark in getAllMarks)
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
            ExclaimMarkHLView mark = GetGuideMark(guideMarkLoc1);
            StartCoroutine(Wait(mark, guideMarkLoc2)); 
        }

        IEnumerator Wait(ExclaimMarkHLView mark, GuideMarkLoc guideMarkLoc2)
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