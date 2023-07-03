using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    public class HelpDay : MonoBehaviour, iHelp
    {
        [SerializeField] HelpName helpName;
        public HelpName GetHelpName()
        {
            return helpName;
        }
    }
}