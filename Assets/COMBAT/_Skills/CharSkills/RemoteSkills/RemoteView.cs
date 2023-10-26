using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RemoteView : MonoBehaviour
    {
        [SerializeField] SkillModel skillModel;         
        [SerializeField]CellPosData cellPosData;
        [SerializeField]CharController charController; 
        public void InitRemoteView(SkillModel skillModel, CellPosData cellPosData, CharController charController)
        {  
            this.skillModel= skillModel;
            this.cellPosData= cellPosData;
            this.charController= charController;
        }
        public void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("remote Skill here trigger");

            if (collision.gameObject != null)
            {
                CharController charController = collision.gameObject.GetComponent<CharController>();
                DynamicPosData dyna =
                GridService.Instance.GetDyna4GO(charController.gameObject);
                if (dyna != null)
                {
                    SkillService.Instance.On_SkillSelected(skillModel.charName, skillModel.skillName);
                    CombatEventService.Instance.On_targetClicked(dyna, cellPosData);
                }
            }
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("remote Skill here collider");

            if (collision.gameObject.GetComponent<CharController>() != null)
            {
                CharController targetController = collision.gameObject.GetComponent<CharController>();
                DynamicPosData dyna =
                            GridService.Instance.GetDyna4GO(targetController.gameObject);
                SkillBase skillbase = charController.skillController.GetSkillBase(skillModel.skillName); 

                if (dyna != null)
                {
                    skillbase.targetGO = targetController.gameObject; 
                    skillbase.targetController = targetController;
                    skillbase.myDyna= dyna;
                    skillbase.PreApplyFX();
                    skillbase.ApplyFX1();
                    skillbase.ApplyFX2();
                    skillbase.ApplyFX3();
                    skillbase.ApplyMoveFx();
                    skillbase.ApplyVFx();
                    skillbase.PostApplyFX();
                    Destroy(this.gameObject, 0.2f); 
                    //SkillService.Instance.On_SkillSelected(skillModel.charName, skillModel.skillName);
                    //CombatEventService.Instance.On_targetClicked(dyna, cellPosData);
                    //SkillService.Instance.DeSelectSkill(); 
                }
            }
        }
    }
}