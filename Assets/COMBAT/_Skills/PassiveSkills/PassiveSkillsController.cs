using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class PassiveSkillsController : MonoBehaviour
    {
        // Start is called before the first frame update

        CharController charController;
        CharNames charName;
        public List<PassiveSkillBase> allPassiveSkillbase = new List<PassiveSkillBase>();

        PassiveSkillFactory passiveSkillFactory;
        public void InitPassive(CharController charController)
        {
            // get so .. get pskill name .. get base ... apply fx on chars ....
            if (allPassiveSkillbase.Count > 0) return; 
            this.charController = charController;
            this.charName = charController.charModel.charName;
            SkillDataSO skillSO = SkillService.Instance.GetSkillSO(charName);
            passiveSkillFactory = SkillService.Instance.passiveSkillFactory; 
            foreach (PassiveSkillData pSkillData in skillSO.passiveSkills)
            {
                PassiveSkillBase pSkillbase =
                        passiveSkillFactory.GetPassiveSkills(pSkillData.passiveSkillName);

                pSkillbase.ApplyFX(); 

                allPassiveSkillbase.Add(pSkillbase);
            }
        }

        //public void ApplyPassiveSkills(CharController targetController)
        //{
        //    foreach (PassiveSkillBase pSkillBase in allPassiveSkillbase)
        //    {
        //        pSkillBase.BaseFX(targetController);
        //        pSkillBase.ApplyFX();
        //    }
        //}

    }
}