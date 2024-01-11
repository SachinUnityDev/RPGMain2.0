using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common;
using DG.Tweening; 
namespace Combat
{

    public class CombatPortView : MonoBehaviour
    {
        [Header("Port View")]
        [SerializeField] Transform portView;
        [SerializeField] Image charImg; 

        [SerializeField] TextMeshProUGUI charNameTxt;
        [SerializeField] Image HPBarImg;
        [SerializeField] Image StaminaBarImg;
        [SerializeField] FortCircleView fortCircleView;
        [SerializeField] CharOnTurnBtn charOnTurnBtn;

        [SerializeField] HpRegenView hpRegenView;
        [SerializeField] StmRegenView stmRegenView;

        [Header("On Hover Scripts")]
        [SerializeField] OnHoverHpBar onHoverHpBar;

        [Header("Global Var")]
        CharController charController; 
        private void OnEnable()
        {
            CombatEventService.Instance.OnCharClicked += FillPort; 
        }

        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= FillPort;
        }

        void FillPort(CharController charController)
        {
            this.charController = charController;
            RectTransform portAlly = portView.GetComponent<RectTransform>();

            if (charController.charModel.charMode == CharMode.Ally)
            {
                portView.transform.DOScaleY(1.0f, 0.4f);
            }
            else
            {
                portView.transform.DOScaleY(0.0f, 0.4f);
            }
            InitOnHover(); 
            FillNameNTxt();
            FillBars();
            FillHpNStmRegen(); 
        }
        void InitOnHover()
        {
            onHoverHpBar.InitOnHoverTxt(charController);
            //onHoverStmBar.InitOnHoverTxt(charController);
        }
        void FillNameNTxt()
        {
            CharNames charName = charController.charModel.charName;
            CharacterSO charSO = CharService.Instance.GetCharSO(charName);
            charImg.sprite = charController.charModel.charSprite;
            charNameTxt.text = charController.charModel.charNameStr;
        }
        void FillBars()
        {
            StatData HPData = charController.GetStat(StatName.health);
            StatData StaminaData = charController.GetStat(StatName.stamina);
            AttribData vigorData = charController.GetAttrib(AttribName.vigor);
            AttribData wpData = charController.GetAttrib(AttribName.willpower);
            
            float HPbarValue = (float)HPData.currValue / (vigorData.currValue*4);
            float staminaBarVal = (float)StaminaData.currValue / (wpData.currValue *3);
             
            HPBarImg.fillAmount = HPbarValue;
            StaminaBarImg.fillAmount = staminaBarVal;
        }
        void FillHpNStmRegen()
        {
            hpRegenView.InitNFillHPView(charController); 
            stmRegenView.InitNFillHPView(charController); 
        }

    }
}