using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using DG.Tweening; 


namespace Combat
{
    public class HPBarView : MonoBehaviour
    {
       [SerializeField] float prevHPVal; 
        [SerializeField] float prevStaminaVal;

       [SerializeField] CharController charController; 
        //private void Start()
        //{
        //    CombatEventService.Instance.OnCharOnTurnSet += FillHPBar;
        //    CombatEventService.Instance.OnSOR1 += OnSOR; 
        //}
        //void OnSOR(int rd)
        //{
        //    charController = transform.parent.gameObject.GetComponent<CharController>();

        //    foreach (CharController charCtrl in CharService.Instance.charsInPlayControllers)
        //    {
        //        if (charCtrl.charModel.charID == charController.charModel.charID)
        //            charCtrl.OnStatChg += OnStatChg;
        //    }
        //}

        //private void OnDisable()
        //{
        //    CombatEventService.Instance.OnCharOnTurnSet -= FillHPBar;
        //  //  CombatEventService.Instance.OnSOC1 -= OnSOR;
        //}


        //void OnStatChg(StatModData statModData)
        //{
        //    CharController charController1 = CharService.Instance.GetCharCtrlWithCharID(statModData.effectedCharNameID);
        //    if (charController1.charModel.charID == charController.charModel.charID)
        //        FillHPBar(charController1);
        //}

        public void FillHPBar(CharController charController)
        {
            Transform HPBarImgTrans = transform.GetChild(0).GetChild(1);
            Transform StaminaBarImgTrans = transform.GetChild(1).GetChild(1);

            Transform BarImgOrange = transform.GetChild(0).GetChild(0);
            Transform StaminaBarImgOrange = transform.GetChild(1).GetChild(0);
            StatData statDataHP = charController.GetStat(StatName.health);
            StatData statDataStm = charController.GetStat(StatName.stamina);
            AttribData willPowerSD = charController.GetAttrib(AttribName.willpower);
            AttribData vigorSD = charController.GetAttrib(AttribName.vigor);
            
            float barValHP = (float)statDataHP.currValue / (float)(statDataHP.maxLimit);
            barValHP = (barValHP > 1) ? 1 : barValHP;
            if (statDataHP.currValue != prevHPVal)
            {
                Vector3 barImgScale = new Vector3(barValHP, HPBarImgTrans.localScale.y, HPBarImgTrans.localScale.z);
                HPBarImgTrans.localScale = barImgScale;
                OrangeBarScaleAnim(BarImgOrange, barImgScale.x);
            }

            float barValStm = (float)statDataStm.currValue / (float)(statDataStm.maxLimit);
            barValStm = (barValStm > 1) ? 1 : barValStm;
            if (statDataStm.currValue != prevStaminaVal)
            {
                Vector3 staminaScale = new Vector3(barValStm, StaminaBarImgTrans.localScale.y, StaminaBarImgTrans.localScale.z);
                StaminaBarImgTrans.localScale = staminaScale;
                OrangeBarScaleAnim(StaminaBarImgOrange, staminaScale.x);
            }
        }


       
        void OrangeBarScaleAnim(Transform barTrans, float scale)
        {
            barTrans.gameObject.SetActive(true);
            Sequence barSeq = DOTween.Sequence();

            barSeq
                .AppendInterval(0.4f)
                .Append(barTrans.DOScaleX(scale, 1f))
                ;

            barSeq.Play().OnComplete(() => barTrans.gameObject.SetActive(false));
        }
    }
}