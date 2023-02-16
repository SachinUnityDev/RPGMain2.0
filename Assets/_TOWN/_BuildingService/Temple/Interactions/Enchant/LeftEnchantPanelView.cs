using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{

    public class LeftEnchantPanelView : MonoBehaviour
    {
        [SerializeField] Image portImg;
        [SerializeField] CharNames charName;
        [SerializeField] TextMeshProUGUI descTxt;
//        Unidentified: Identify first
//Identified: There is enough denari to Enchant   / Not enough denari to Enchant
//Enchanted: Enchanted with Ruri
//Rechargeable : There is enough denari to Recharge / Not enough denari to Recharge

        void Awake()
        {
                      
        }
        public void InitLeftPanel(EnchantView enchantView)
        {

        }
    }
}