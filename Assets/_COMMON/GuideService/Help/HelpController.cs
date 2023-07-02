using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class HelpController : MonoBehaviour
    {

        public AllHelpSO allHelpSO;
        public HelpView helpView;
        void Start()
        {

        }

        public void ShowHelpView(HelpName helpName)
        {
            HelpSO helpSO = allHelpSO.GetHelpSO(helpName);  

            helpView.InitHelp(helpName, helpSO);
        }
        
    }
}