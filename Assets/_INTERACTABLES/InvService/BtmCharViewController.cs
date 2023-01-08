using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Common;
using UnityEngine.UI;
using System;

namespace Interactables
{
    public class BtmCharViewController : MonoBehaviour, IPanel
    {
        
        [SerializeField] List<CharModel> allAvailableChars = new List<CharModel>();
        [SerializeField] List<CharModel> rightsChars = new List<CharModel>();
        [SerializeField] List<CharModel> leftChars = new List<CharModel>();

        [SerializeField] List<GameObject> leftCharsGO = new List<GameObject>();
        [SerializeField] List<GameObject> rightCharsGO = new List<GameObject>();

        [SerializeField] GameObject leftPanelGO;
        [SerializeField] GameObject rightPanelGO;
        [SerializeField] GameObject centerPanelGO;
        [SerializeField] GameObject portPrefab; 


        [Header("GLOBAL")]
        public CharModel charSelect;
        public bool isSpread = false;
        int index = 0; 
        void Start()
        {
           // charSelect = CharNames.Cahyo; 
        }

        public void Init()
        {
            // get all available characters 
           // LocationName location = LocationName.Nekkisari;
            //foreach (CharController c in RosterService.Instance.rosterController.GetCharAvailableInTown(location))
            //{              
            //    allAvailableChars.Add(c.charModel);
            //}
            allAvailableChars.Clear();
            rightsChars.Clear();leftChars.Clear();
            foreach (CharModel c in CharService.Instance.allAvailCharsModels)
            {
                allAvailableChars.Add(c);
            }
            
            charSelect = allAvailableChars[0];
            InvService.Instance.On_CharSelectInv(charSelect);
            
            PlayCloseAnim();
           
        }
        public void CharSelected(CharModel charModel, GameObject portGO, bool isRight)
        {
            if(charSelect.charName == charModel.charName)
            {
                if (isSpread)
                {
                    PlayCloseAnim();
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
                Swap(isRight, portGO);
                charSelect = charModel;
                InvService.Instance.On_CharSelectInv(charSelect);                
                PlayCloseAnim();
            }    
        }

     
        void Swap( bool isRight, GameObject portGO)
        {
            Transform panel = portGO.transform.parent;
            if (isRight)
            {
                rightCharsGO.Remove(portGO);
                rightCharsGO.Add(centerPanelGO.transform.GetChild(0).gameObject);
            }
            else
            {
                leftCharsGO.Remove(portGO);
                leftCharsGO.Add(centerPanelGO.transform.GetChild(0).gameObject);
            }
            centerPanelGO.transform.GetChild(0).GetComponent<CharPortraitPtrEvents>().SetPanel(isRight);
            centerPanelGO.transform.GetChild(0).SetParent(panel);
        
            portGO.transform.SetParent(centerPanelGO.transform);
            portGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            portGO.transform.DOLocalMoveX(0, 0.15f);
            SortChars();
           
        }
        void SortChars()
        {
            index = 0; int midCount = 0;
            if(allAvailableChars.Count%2 !=0)
                midCount = (allAvailableChars.Count-1) / 2;
            else
                midCount = (allAvailableChars.Count) / 2;
            leftChars.Clear();rightsChars.Clear();
            for (int i = 0; i < allAvailableChars.Count; i++)
            {
                if(allAvailableChars[i].charName != charSelect.charName)
                {
                    if (i < midCount)
                    {
                        leftChars.Add(allAvailableChars[i]);
                    }
                    else
                    {
                        rightsChars.Add(allAvailableChars[i]);
                    }
                }
            }
        }

        void ClearPanels()
        {
            int count = leftCharsGO.Count;
            for (int i = 0; i < count; i++)
            {
                Destroy(leftCharsGO[i]); 
            }
            count = rightCharsGO.Count;
            for (int i = 0; i < count; i++)
            {
                Destroy(rightCharsGO[i]);
            }
            leftCharsGO.Clear(); rightCharsGO.Clear();
        }
        void PopulatePortPanels( )
        {
            ClearPanels();
            SortChars();

            if (leftChars.Count > 0)
            {
                foreach (CharModel charModel in leftChars)
                {
                    GameObject portChar = Instantiate(portPrefab);
                    portChar.name = charModel.charNameStr;
                    portChar.transform.SetParent(leftPanelGO.transform);
                    portChar.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    portChar.GetComponent<RectTransform>().localScale = Vector3.one;
                    portChar.GetComponent<CharPortraitPtrEvents>().Init(charModel, this, false);
                    leftCharsGO.Add(portChar);
                }
            }
            if(rightsChars.Count > 0)
            {
                foreach (CharModel charModel in rightsChars)
                {
                    GameObject portChar = Instantiate(portPrefab);
                    portChar.name = charModel.charNameStr;
                    portChar.transform.SetParent(rightPanelGO.transform);
                    portChar.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    portChar.GetComponent<RectTransform>().localScale = Vector3.one;
                    portChar.GetComponent<CharPortraitPtrEvents>().Init(charModel, this, true);
                    rightCharsGO.Add(portChar);
                }
            }
            GameObject portCharC = Instantiate(portPrefab);
            portCharC.name = charSelect.charNameStr;
            portCharC.transform.SetParent(centerPanelGO.transform);
            portCharC.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            portCharC.GetComponent<RectTransform>().localScale = Vector3.one;
            portCharC.GetComponent<CharPortraitPtrEvents>().Init(charSelect, this, false);

            
        }

        public void PlayOpenAnim()
        {
            float wide = leftCharsGO[0].GetComponent<RectTransform>().rect.width; 
            for (int i = 0; i < leftCharsGO.Count; i++)
            {
                float moveto = (wide ) * (i+1);
             //   Debug.Log("Index value" + i + "NAME" + leftCharsGO[i].name);
                leftCharsGO[i].transform.DOLocalMoveX(-moveto, 0.15f);
                leftCharsGO[i].transform.SetSiblingIndex(0);
            }
            for (int i = 0; i < rightCharsGO.Count; i++)
            {
                float moveto = (wide ) * (i+1);
               // Debug.Log("Index value" + i + "NAME" + rightCharsGO[i].name);
                rightCharsGO[i].transform.DOLocalMoveX(moveto, 0.15f);
                rightCharsGO[i].transform.SetAsFirstSibling();
            }
        }

        public void PlayCloseAnim()
        {
            PopulatePortPanels();
            for (int i = 0; i < leftCharsGO.Count; i++)
            {
                leftCharsGO[i].transform.DOLocalMoveX(0, 0.15f);
            }
            for (int i = 0; i < rightCharsGO.Count; i++)
            {
                rightCharsGO[i].transform.DOLocalMoveX(0, 0.15f);
            }
           
        }

        public void Load()
        {
            Init();
        }

        public void UnLoad()
        {
        }
    }


}


