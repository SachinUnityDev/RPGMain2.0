using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Interactables; 


namespace Common
{
    public class SkillPtsView : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI skillPtsTxt;
        [SerializeField] TextMeshProUGUI charMottotxt;

        LeftSkillView leftSkillView; 
        public void Init(LeftSkillView leftSkillView)
        {
            this.leftSkillView = leftSkillView;
            // get char Select Controller
            CharController charController = InvService.Instance.charSelectController;
            int skillpts = charController.charModel.skillPts;
            skillPtsTxt.text = skillpts.ToString();
        }

    }
}