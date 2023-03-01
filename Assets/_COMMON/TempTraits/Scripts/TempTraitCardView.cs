using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class TempTraitCardView : MonoBehaviour
    {
        [SerializeField] TempTraitController tempTraitController; 
        [SerializeField] CharController charController;
        [SerializeField] TempTraitModel tempTraitModel; 


        [Header("Transform ref NTBR")]
        [SerializeField] Transform topTrans;        
        [SerializeField] Transform btmTrans;

        [Header("SkillData SO")]        
        [SerializeField] TempTraitSO tempTraitSO;
        [SerializeField] AllTempTraitSO allTempTraitSO; 

        [Header("Global Variables")]
        [SerializeField] CharNames charName;
        [SerializeField] TempTraitName tempTraitName;

        private void Awake()
        {
            gameObject.SetActive(false);
            TempTraitService.Instance.OnTempTraitHovered += OnTempTraitlHovered; 
        }

        void OnTempTraitlHovered(CharNames charName, TempTraitName tempTraitName)
        {
            this.charName = charName;   
            this.tempTraitName= tempTraitName;  

            charController = CharService.Instance.GetCharCtrlWithName(charName);
            tempTraitController = charController.tempTraitController;

            FillBtmTrans();   
            FillTopTrans();
            
        }
        void FillBtmTrans()
        {
            allTempTraitSO = TempTraitService.Instance.allTempTraitSO; 
            tempTraitSO = allTempTraitSO.GetTempTraitSO(tempTraitName);
            // type
            TempTraitTypeSpriteData typeData
                            = allTempTraitSO.GetTypeSpriteData(tempTraitSO.tempTraitType);
            btmTrans.GetComponent<Image>().sprite = typeData.cardBGBottom; 
            btmTrans.GetChild(0).GetComponent<TempTraitTypePtrEvents>().InitPtrEvents(tempTraitSO, typeData); 

            // behavior
            TempTBehaviourSpriteData behaviourSprites 
                            = allTempTraitSO.GetTempTraitBehaviourData(tempTraitSO.temptraitBehavior);
            btmTrans.GetChild(1).GetComponent<TempTBehaviourPtrEvents>().InitPtrEvents(tempTraitSO, behaviourSprites);
        }

        void FillTopTrans()
        {
            TempTraitTypeSpriteData typeData
                         = allTempTraitSO.GetTypeSpriteData(tempTraitSO.tempTraitType);
            topTrans.GetComponent<Image>().sprite = typeData.cardBGTop;
            int t = 2;
            // threshold t = 2 lines 
            // expansionConst = 25
            float topHt = topTrans.GetComponent<RectTransform>().rect.height;
            float topWidth = topTrans.GetComponent<RectTransform>().rect.width;
            for (int i = 0; i < tempTraitSO.allLines.Count; i++)
            {
                if (i > t)
                {                  
                    topTrans.GetComponent<RectTransform>().sizeDelta
                        = new Vector2(topWidth, topHt + (i-t) * 25f);
                }



                // set active true; 
            }

        }
    }
}