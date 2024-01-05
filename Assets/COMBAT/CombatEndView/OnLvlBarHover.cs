using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class OnLvlBarHover : MonoBehaviour
    {
        [SerializeField] LvlNExpSO lvlNExpSO;
        CharModel charModel;
        [SerializeField] TextMeshProUGUI onHoverTxt; 
        public void InitOnLvlBarHover(CharModel charModel)
        {
            this.charModel = charModel;
            LvlBarTxtUpdate(); 
            onHoverTxt.gameObject.SetActive(true); 
        }
        void LvlBarTxtUpdate()
        {
            int deltaExp = lvlNExpSO.GetdeltaExpPts4Lvl(charModel.charLvl);
            int thresholdExp = lvlNExpSO.GetThresholdExpPts4Lvl(charModel.charLvl+1);
            //int expDiff = charModel.mainExp - thresholdExp;
            onHoverTxt.text = $"{charModel.mainExp}/{thresholdExp}";             
        }
    }
}