using Combat;
using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class TraitCardView : MonoBehaviour
    {

        [SerializeField] const float descTransSize = 55f;
        [SerializeField] const float containerSize = 125f;


        [Header("Transform TBR")]
        [SerializeField] Transform header;        
        [SerializeField] Transform desc;

        [Header(" NTBR")]
        [SerializeField] Image headerIconImg;
        [SerializeField] TextMeshProUGUI traitName;
        [SerializeField] Image traitBehaviorImg; 

        [Header("Temp Trait")]        
        [SerializeField] TempTraitSO tempTraitSO;
        [SerializeField] AllTempTraitSO allTempTraitSO;
        [SerializeField] TempTraitModel tempTraitModel;

        [Header("Perma Trait")]
        [SerializeField] PermaTraitSO permaTraitSO; 
        [SerializeField] AllPermaTraitSO allPermaTraitSO;
        [SerializeField] PermaTraitModel permaTraitModel;

        [Header("Global var")]
        CharController charController;
        [SerializeField] List<string> allDesc = new List<string>();
        int incr;
        [SerializeField] int incrVal = 0;


        public void ShowTempTraitCard(TempTraitModel tempTraitModel)
        {
            this.tempTraitModel = tempTraitModel;
            charController = InvService.Instance.charSelectController;
            allTempTraitSO = TempTraitService.Instance.allTempTraitSO;

            TempTraitSO tempTraitSO = allTempTraitSO.GetTempTraitSO(tempTraitModel.tempTraitName); 
            allDesc.Clear();
            allDesc.AddRange(tempTraitSO.allLines); 
            FillTempTraits();
            ExpandNFillDescTrans();
        }
        public void ShowPermaTraitCard(PermaTraitModel permaTraitModel)
        {
            this.permaTraitModel = permaTraitModel;
            charController = InvService.Instance.charSelectController;
            allPermaTraitSO = PermaTraitsService.Instance.allPermaTraitSO;
            PermaTraitSO permaTraitSO = allPermaTraitSO.GetPermaTraitSO(permaTraitModel.permaTraitName);

            allDesc.Clear();
            allDesc.AddRange(permaTraitSO.allLines);
            ExpandNFillDescTrans();
            FillPermaTraits();
            
        }

        void FillTempTraits()
        {
            tempTraitSO = allTempTraitSO.GetTempTraitSO(tempTraitModel.tempTraitName);
            // get BG     
            TempTraitType tempTraitType = tempTraitModel.tempTraitType; 
            headerIconImg.sprite = allTempTraitSO.GetTraitTypeSpriteData(tempTraitType).iconSprite; 
            if(tempTraitType != TempTraitType.None)
                traitName.text = tempTraitType.ToString();

            FillPosNeg(tempTraitModel.temptraitBehavior); 
        }
        void FillPermaTraits()
        {
            allTempTraitSO = TempTraitService.Instance.allTempTraitSO;
            permaTraitSO = allPermaTraitSO.GetPermaTraitSO(permaTraitModel.permaTraitName);
           
            if (permaTraitSO.classType != ClassType.None)
            {
                headerIconImg.sprite = allPermaTraitSO.classIcon;
                traitName.text = "CLASS"; 
            }                
            else if (permaTraitModel.raceType != RaceType.None)
            {
                headerIconImg.sprite = allPermaTraitSO.racialIcon;
                traitName.text = "RACIAL"; 
            }                
            else if (permaTraitModel.cultureType != CultureType.None)
            {
                headerIconImg.sprite = allPermaTraitSO.cultureIcon;
                traitName.text = "CULTURAL"; 
            }
          
            traitBehaviorImg.sprite = allTempTraitSO.GetTempTraitBehaviourData(permaTraitModel.traitBehaviour).iconSprite;
            FillPosNeg(permaTraitModel.traitBehaviour); 
        }

        void FillPosNeg(TraitBehaviour traitBehaviour)
        {            
            allTempTraitSO = TempTraitService.Instance.allTempTraitSO;

            transform.GetChild(0).GetComponent<Image>().sprite 
                = allTempTraitSO.GetTempTraitBehaviourData(traitBehaviour).cardBG;
            traitBehaviorImg.sprite = allTempTraitSO.GetTempTraitBehaviourData(traitBehaviour).iconSprite;
            AnchorImg(traitBehaviorImg, headerIconImg, traitBehaviour); 
      

        }

        void AnchorImg(Image img1, Image img2, TraitBehaviour traitBehaviour)
        {
            if (traitBehaviour == TraitBehaviour.Negative)
            {
                img1.GetComponent<RectTransform>().DOAnchorMin(new Vector2(0, 0), 0.1f);
                img1.GetComponent<RectTransform>().DOAnchorMax(new Vector2(0, 0), 0.1f);
                img1.GetComponent<RectTransform>().DOPivot(new Vector2(0, 0), 0.1f);

                img2.GetComponent<RectTransform>().DOAnchorMin(new Vector2(1, 0), 0.1f);
                img2.GetComponent<RectTransform>().DOAnchorMax(new Vector2(1, 0), 0.1f);
                img2.GetComponent<RectTransform>().DOPivot(new Vector2(1, 0), 0.1f);

            }
            else if (traitBehaviour == TraitBehaviour.Positive)
            {
                img1.GetComponent<RectTransform>().DOAnchorMin(new Vector2(1, 0), 0.1f);
                img1.GetComponent<RectTransform>().DOAnchorMax(new Vector2(1, 0), 0.1f);
                img1.GetComponent<RectTransform>().DOPivot(new Vector2(1, 0), 0.1f);

                img2.GetComponent<RectTransform>().DOAnchorMin(new Vector2(0, 0), 0.1f);
                img2.GetComponent<RectTransform>().DOAnchorMax(new Vector2(0, 0), 0.1f);
                img2.GetComponent<RectTransform>().DOPivot(new Vector2(0, 0), 0.1f);
            }
        }

        void ResetSize()
        {
            RectTransform descTransRect = desc.GetComponent<RectTransform>();
            RectTransform descParentRect = transform.GetChild(0).GetComponent<RectTransform>();
            //descTransRect.sizeDelta
            //        = new Vector2(descTransRect.sizeDelta.x, descTransSize);
            descParentRect.sizeDelta
                    = new Vector2(descParentRect.sizeDelta.x, containerSize);
        }
        void ExpandNFillDescTrans()
        {
            int lines = allDesc.Count; 
            
            RectTransform descTransRect = desc.GetComponent<RectTransform>();
            RectTransform descParentRect = transform.GetChild(0).GetComponent<RectTransform>();
         
            incrVal = 0;
            int j = 0;
            incr = 0;
            ResetSize();
            for (int i = desc.childCount-1; i >= 0; i--)
            {
                if(j <lines) 
                {
                    desc.GetChild(i).gameObject.SetActive(true);
                    TextMeshProUGUI textM = desc.GetChild(i).GetComponent<TextMeshProUGUI>();
                    textM.text = "";
                    textM.text = allDesc[j];
                    if (textM != null || textM.text != "")
                    {
                        UpdateTextHeight(textM);
                    }
                    j++;
                }
                else
                {
                    desc.GetChild(i).gameObject.SetActive(false);
                    incrVal = 0;
                    incr = 0;
                }
            }
            //foreach (Transform child in desc)
            //{
            //    if (j < lines)
            //    {
            //        child.gameObject.SetActive(true);
            //        TextMeshProUGUI textM = child.GetComponent<TextMeshProUGUI>();
            //        textM.text = ""; 
            //        textM.text = allDesc[j];
            //        if (textM != null || textM.text != "")
            //        {
            //            UpdateTextHeight(textM);
            //            Debug.Log(" j val" + j);
            //        }
            //        else
            //        {
            //            Debug.LogError(" j val" + j);
            //        }
            //    }
            //    else
            //    {
            //        child.gameObject.SetActive(false);
            //        incrVal = 0;
            //        incr = 0;
            //    }
            //    j++;
            //}
            if (lines > 1)
            {
                //incrVal = 0;
                incr = 0;
                // increase size 
                incr += (lines - 1);// also updated in update Txt Ht

                RectTransform txtRect = desc.GetChild(0).GetComponent<RectTransform>();// text ht 
                float txtHt = txtRect.sizeDelta.y;
                //Debug.Log("TXT HT" + txtHt);
                incrVal += incr * ((int)(txtHt)+15);// correction factor
                //descTransRect.sizeDelta
                //        = new Vector2(descTransRect.sizeDelta.x, descTransSize + incrVal);
                descParentRect.sizeDelta
                        = new Vector2(descParentRect.sizeDelta.x, containerSize + incrVal);
            }
            else
            {
                // reduce to org size 
                incrVal = 0; incr = 0;
                //descTransRect.sizeDelta
                //        = new Vector2(descTransRect.sizeDelta.x, descTransSize);
                descParentRect.sizeDelta
                        = new Vector2(descParentRect.sizeDelta.x, containerSize);
            }
        }
        void UpdateTextHeight(TextMeshProUGUI textM)
        {
            // Get the current text from the TextMeshPro component
            string text = textM.text;
            incr = 0; incrVal = 0;
            // Check if the text length exceeds the maximum length
            if (text.Length > 20)
            {
                // Calculate the new height based on the number of lines required               
                int numberOfLines = Mathf.RoundToInt((float)text.Length / 20);
                Debug.Log("The Desc" + textM.text +"LINES"+ numberOfLines);
                float newHeight = textM.fontSize * (numberOfLines+1);

                // Adjust the text component's rect transform height
                textM.rectTransform.sizeDelta = new Vector2(textM.rectTransform.sizeDelta.x, newHeight);
                incrVal += (int)(textM.rectTransform.sizeDelta.y + 15f);

            }
            else
            {
                textM.rectTransform.sizeDelta = new Vector2(textM.rectTransform.sizeDelta.x, 40);
            }
        }
    }
}