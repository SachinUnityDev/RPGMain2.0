using Combat;
using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class CurioBarkController : MonoBehaviour
    {
        public QbarkView qbarkView;
        void Start()
        {

        }

        public void ShowCurioBark(CurioNames curioName, CurioColEvents curioColEvents)
        {
            CurioBarkData curioBarkData = BarkService.Instance.allBarkSO.GetCurioBarkData(curioName);
            
            BarkCharData barkCharData = new BarkCharData(CharNames.Abbas_Skirmisher
                                            , curioBarkData.barkline, curioBarkData.VOaudioClip);

            qbarkView.InitCurioBark(barkCharData, curioColEvents, curioBarkData.UIaudioClip);            
        }
    }
}