using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq;
using Common;

namespace Interactables
{
    public class BestiarySkillView : MonoBehaviour
    {
        [SerializeField] Transform skillIconsContainer;

        [SerializeField] AllSkillSO allSkillSO; 
        void Start()
        {

        }

        public void InitSkillIcons(CharModel charModel)
        {
            SkillService.Instance.
            // get skills fro SKillSO 
            // fill up one by one the either the icons or darta 


        }

        SkillDataSO GetSkillDataSO(CharNames _charName)
        {
            SkillDataSO skillDataSO = allSkillSO.allSkillDataSO.Find(x => x.charName == _charName);
            if (skillDataSO != null)
            {
                return skillDataSO;
            }
            else
            {
                Debug.Log("skill Data SO  Not FOUND");
                return null;
            }
        }

    }

}
