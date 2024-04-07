using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Common;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

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
            string str = allCultTypeStr[dropDown.value];
            CultureType cultType = (CultureType)Enum.Parse(typeof(CultureType), str);
            bestiaryViewController.Move2Index(cultType);
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
                    if (!allCultTypeStr.Any(t => t == str))
                        allCultTypeStr.Add(str);
                }
            }
            dropdown.AddOptions(allCultTypeStr);
        }
        public void UpdateDropDownVal(CultureType cultType)
        {
            int indexF = allCultTypeStr.FindIndex(t=>t == cultType.ToString());
            if(indexF != -1)
            {
                dropdown.value = indexF; 
            }
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

