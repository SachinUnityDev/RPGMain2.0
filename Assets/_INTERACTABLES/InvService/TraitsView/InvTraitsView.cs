using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Interactables
{


    public class InvTraitsView : MonoBehaviour
    {
        [SerializeField] PosTraitsView posTraitView;
        [SerializeField] NegTraitsView negTraitsView;

        [SerializeField] PermaTraitBtnView permaTraitBtnView;
        [SerializeField] TempTraitBtnView tempTraitBtnView; 
        public SicknessBtnView sicknessBtnView;

        private void OnEnable()
        {
           Init();
            InvService.Instance.OnCharSelectInvPanel += (CharModel c) => Init();
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= (CharModel c) => Init();
        }

        public void Init()
        {
            posTraitView = GetComponentInChildren<PosTraitsView>();
            negTraitsView= GetComponentInChildren<NegTraitsView>();
            permaTraitBtnView = GetComponentInChildren<PermaTraitBtnView>();
            tempTraitBtnView = GetComponentInChildren<TempTraitBtnView>();
            sicknessBtnView = FindObjectOfType<SicknessBtnView>();


            tempTraitBtnView.Init(this);
            sicknessBtnView.Init(this);
            permaTraitBtnView.Init(this);     
        }

        public void ShowPermaTrait(CharModel charModel)
        {
            tempTraitBtnView.OnUnClick(); 
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charModel.charID);
            PermaTraitController permaTraitController = charController.permaTraitController;

            posTraitView.InitPosTrait(permaTraitController.allPermaModels);
            negTraitsView.InitNegTrait(permaTraitController.allPermaModels);
        }

        public void ShowTempTraitType(TempTraitType tempTraitType, CharModel charModel)
        {
            permaTraitBtnView.OnUnClick();
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charModel.charID);
            TempTraitController tempTraitController = charController.tempTraitController;

            List<TempTraitModel> allTempTraitModelsOfType = tempTraitController
                                                .allTempTraitModels.Where(t => t.tempTraitType == tempTraitType).ToList(); 

            posTraitView.InitPosTrait(allTempTraitModelsOfType);
            negTraitsView.InitNegTrait(allTempTraitModelsOfType);
        }
    }
}