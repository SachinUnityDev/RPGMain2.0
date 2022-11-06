using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Common;
namespace Interactables
{
    public class CultDropDownEvents : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        [SerializeField] List<string> allOptions = new List<string>();
        [SerializeField] List<string> allCultTypeStr = new List<string>();
        [SerializeField] List<CharModel> cultList = new List<CharModel>();
        [SerializeField] BestiaryViewController bestiaryViewController;

        void Start()
        {
            dropdown =GetComponentInChildren<TMP_Dropdown>();
            dropdown.onValueChanged.AddListener(delegate
            {
                DropdownValueChanged(dropdown);
            });
            AddOptions();
        }

        void DropdownValueChanged(TMP_Dropdown dropDown)
        {
           // Debug.Log("drop down + " + cultList[dropdown.value].cultType);

            CultureType cultTyp = cultList[dropdown.value].cultType;
            bestiaryViewController.Move2Index(cultTyp);
        }

        public void PopulateOptions(List<CharModel> cultList, BestiaryViewController bestiaryViewController)
        {
            this.bestiaryViewController = bestiaryViewController;
            this.cultList = cultList;
            dropdown.ClearOptions();
            allCultTypeStr.Clear();
            if (cultList.Count < 1)
            {
                allCultTypeStr.Add("None");
            }
            else
            {
                foreach (CharModel c in cultList)
                {
                    string str = c.cultType.ToString();
                    allCultTypeStr.Add(str);
                }
            }
            dropdown.AddOptions(allCultTypeStr);
        }
        void AddOptions()
        {
            for (int i = 1; i < Enum.GetNames(typeof(CultureType)).Length; i++)
            {
                CultureType cult= (CultureType)i;
                string str = cult.ToString().CreateSpace();
                allOptions.Add(str); 
            }
            dropdown.AddOptions(allOptions);
        }
    }
}

