using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RemoteView : MonoBehaviour
    {
        [SerializeField] SkillModel skillModel;         
        public CellPosData cellPosData;
        [SerializeField]CharController charController;
        SkillBase skillbase;
        SkillPerkFXData skillPerkFXData; 
        public void InitRemoteView(SkillBase skillBase, CellPosData cellPosData, CharController charController, 
            SkillPerkFXData skillPerkFXData)
        {  
            
            this.cellPosData= cellPosData;
            this.charController= charController;
            this.skillbase= skillBase;
            this.skillPerkFXData= skillPerkFXData;  
            skillModel = skillBase.skillModel;
        }

        public void OnTriggerEnter(Collider collision)
        {
            Debug.Log("remote Skill here TRIGGER");
            CharController targetController = collision.transform.parent?.GetComponent<CharController>(); 
            if (targetController != null)
            {
                DynamicPosData dyna = GridService.Instance.GetDyna4GO(targetController.gameObject);
               
                if (dyna != null)
                {
                    skillbase.targetGO = targetController.gameObject; 
                    skillbase.targetController = targetController;
                    skillbase.myDyna= dyna;
                    skillbase.PreApplyFX();
                   // skillbase.BaseApply(); // not to be used in remote
                    skillbase.ApplyFX1();
                    skillbase.ApplyFX2();
                    skillbase.ApplyFX3();
                    skillbase.ApplyMoveFx();
                    skillbase.ApplyVFx();
                   
                    GameObject ImpactFX = Instantiate(skillPerkFXData.impactFX, transform.position, Quaternion.identity).gameObject;
                    Sequence seq = DOTween.Sequence();
                    seq
                        .AppendCallback(() => PlayParticleSystem(ImpactFX))
                        .AppendInterval(1.0f)
                        .AppendCallback(()=>Destroy(ImpactFX))
                        .AppendCallback(() => Destroy(this.gameObject, 0.2f))              
                        ; 
                    seq.Play();
                }
            }
            void PlayParticleSystem(GameObject FxGO)
            {   
                foreach (ParticleSystem ps in FxGO.GetComponentsInChildren<ParticleSystem>())
                {
                    ps.Play();
                }
            }
        }
    }
}